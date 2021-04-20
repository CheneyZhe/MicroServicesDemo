using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CYPlanning.API.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class CYPlanningController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public CYPlanningController(ILogger<CYPlanningController> logger,IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            string result = $"【计划制订服务】{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}--"
           + $"{Request.HttpContext.Connection.LocalIpAddress}:{configuration["ConsulSetting:ServicePort"]}";

            return Ok(result);
        }
    }
}
