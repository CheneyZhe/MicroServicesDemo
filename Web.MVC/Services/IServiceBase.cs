using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.MVC
{
    public interface IServiceBase
    {
        Task<string> GetCYOranizationAsync();

        Task<string> GetCYPlanningAsync();

        void GetServices();
    }
}
