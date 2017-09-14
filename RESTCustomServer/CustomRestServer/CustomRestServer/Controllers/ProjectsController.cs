using CustomRestServer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CustomRestServer.Controllers
{
    public class ProjectsController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(ProjectModifier.GetProjects());
        }
    }
}
