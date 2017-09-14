using CustomRestServer.Database;
using CustomRestServer.Models;
using CustomRestServer.WebHooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CustomRestServer.Controllers
{
    [Authorize]
    public class EmployeesController : ApiController
    {
        private static readonly log4net.ILog log
       = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Route("api/Employees/GetTeamLeaderAndTeam/{id}")]
        public IHttpActionResult GetLeaderWGetTeamLeaderAndTeamithTeam(int id)
        {
            return Ok(EmployeeModifier.GetTeamLeaderAndTeam(id));
        }

        //update a employee
        public async Task<IHttpActionResult> Post(EmployeeModel e)
        {
            //#Todo if this is not null... we are overwriting something backup current datasnapshot and new data and log a warning
            //var b = Request.Headers.FirstOrDefault(x => x.Key == "Overwriting").Value?.FirstOrDefault();
            await this.NotifyAsync(CustomFilterProvider.EmployeeChanged, new { Employee = e });
            if (EmployeeModifier.updateEmployee(e))
            {
                log.Info("User: " + User.Identity.Name + " Updated an employee with the following new value " + Newtonsoft.Json.JsonConvert.SerializeObject(e));
                return Ok();
            }
            else
            {
                log.Error("User: " + User.Identity.Name + " failed to update an employee");
                return BadRequest();
            }
        }
    }
}
