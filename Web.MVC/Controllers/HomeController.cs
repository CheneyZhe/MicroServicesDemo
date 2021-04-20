using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        readonly IServiceBase service;

        public HomeController(IServiceBase service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string cyoranization = await service.GetCYOranizationAsync();

            string cyplanning = await service.GetCYPlanningAsync();

            return Ok(cyoranization +"------"+ cyplanning);
        }
    }
}
