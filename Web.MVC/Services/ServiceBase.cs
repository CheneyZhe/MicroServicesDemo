using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web.MVC.Services
{
    public class ServiceBase : IServiceBase
    {
        private readonly IConfiguration configuration;
        private readonly ConsulClient consulClient;
        private ConcurrentBag<string> cyoranizationServiceUrls;
        private ConcurrentBag<string> cyplanningServiceUrls;

        public ServiceBase(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.consulClient = new ConsulClient(

                c =>
                {
                    c.Address = new Uri(configuration["ConsulSetting:ConsulAddress"]);
                });
        }

        public async Task<string> GetCYOranizationAsync()
        {
            if (cyoranizationServiceUrls == null)
            {
                return await Task.FromResult("服务列表为空");
            }
            //健康的服务
            var services = consulClient.Health.Service("cyoranizationservice", null, true, null).Result.Response;

            
            var client = new HttpClient();
            var result = await client.GetAsync(cyoranizationServiceUrls.ElementAt(new Random().Next(0, cyoranizationServiceUrls.Count)) + "/cyoranization");
            
            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> GetCYPlanningAsync()
        {
            if (cyplanningServiceUrls == null)
            {
                return await Task.FromResult("服务列表为空");
            }
            //健康的服务
            var services = consulClient.Health.Service("cyplanningservice", null, true, null).Result.Response;

            var client = new HttpClient();
            //每次随机访问一个服务实例
            var result = await client.GetAsync(cyplanningServiceUrls.ElementAt(new Random().Next(0, cyplanningServiceUrls.Count)) + "/cyplanning");
            
            return await result.Content.ReadAsStringAsync();
        }

        public void GetServices()
        {
            var serviceNames = new string[] { "cyoranizationservice", "cyplanningservice" };
            Array.ForEach(serviceNames, p =>
            {
                Task.Run(() =>
                {
                    //WaitTime默认为5分钟
                    var queryOptions = new QueryOptions { WaitTime = TimeSpan.FromMinutes(10) };
                    while (true)
                    {
                        GetServices(queryOptions, p);
                    }
                });
            });
        }

        private void GetServices(QueryOptions queryOptions, string serviceName)
        {
            var res = consulClient.Health.Service(serviceName, null, true, queryOptions).Result;

            //控制台打印一下获取服务列表的响应时间等信息
            Console.WriteLine($"{DateTime.Now}获取{serviceName}：queryOptions.WaitIndex：{queryOptions.WaitIndex}  LastIndex：{res.LastIndex}");

            //版本号不一致 说明服务列表发生了变化
            if (queryOptions.WaitIndex != res.LastIndex)
            {
                queryOptions.WaitIndex = res.LastIndex;

                //服务地址列表
                var serviceUrls = res.Response.Select(p => $"http://{p.Service.Address + ":" + p.Service.Port}").ToArray();

                if (serviceName == "cyoranizationservice")
                    cyoranizationServiceUrls = new ConcurrentBag<string>(serviceUrls);
                else if (serviceName == "cyplanningservice")
                    cyplanningServiceUrls = new ConcurrentBag<string>(serviceUrls);
            }
        }
    }
}
