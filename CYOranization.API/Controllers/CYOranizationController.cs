using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CYOranization.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CYOranizationController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public CYOranizationController(ILogger<CYOranizationController> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            string result = $"【组织结构服务】{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}--"
            + $"{Request.HttpContext.Connection.LocalIpAddress}:{configuration["ConsulSetting:ServicePort"]}";

            return Ok(result);
        }
    }
}
