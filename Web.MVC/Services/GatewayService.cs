using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web.MVC.Services
{
    public class GatewayService : IServiceBase
    {
        private readonly IConfiguration configuration;

        public GatewayService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> GetCYOranizationAsync()
        {
            string baseUri = configuration["GatewayBaseUri"];
            var client = new HttpClient();
             
            var result = await client.GetAsync(baseUri + "/cyoranization");

            return await result.Content.ReadAsStringAsync();

        }

        public async Task<string> GetCYPlanningAsync()
        {
            string baseUri = configuration["GatewayBaseUri"];
            var client = new HttpClient();
            var result = await client.GetAsync(baseUri + "/cyplanning");

            return await result.Content.ReadAsStringAsync();
        }

        public void GetServices()
        {
            throw new NotImplementedException();
        }
    }
}
