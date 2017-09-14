using CustomRestServer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CustomRestServer.Database
{
    public class ProjectModifier
    {
        private static readonly log4net.ILog log
       = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<ProjectModel> GetProjects()
        {
            //this has to do with the complex query method not important for webhooks
            return null;
        }
    }
}