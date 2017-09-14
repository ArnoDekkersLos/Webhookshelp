using CustomRestServer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CustomRestServer.Database
{
    public class EmployeeModifier
    {
        private static readonly log4net.ILog log
       = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static User GetUserByLogin(string userName, string password)
        {
            User foundUser = new User();
            foundUser.Id = 1;
            foundUser.Name = "Drag0nvil";
            foundUser.Password = "not important";
            foundUser.Email = "Drag0nvil@stackoverflow.com";
            return foundUser;
        }

        public static LeaderWithTeamMembersModel GetTeamLeaderAndTeam(int iD)
        {
            LeaderWithTeamMembersModel foundLeader = null;
            List<EmployeeModel> foundMembers = new List<EmployeeModel>();
            EmployeeModel currentMatch = null;
            currentMatch = new EmployeeModel(1, "Leader", "", "The great");
            foundLeader = new LeaderWithTeamMembersModel(currentMatch);
            currentMatch = new EmployeeModel(2, "first helper", "", "The second");
            foundMembers.Add(currentMatch);
            currentMatch = new EmployeeModel(3, "second member", "Te", "The Third");
            foundMembers.Add(currentMatch);
            foundLeader.TeamMembers = foundMembers;
            return foundLeader;
        }

        public static bool updateEmployee(EmployeeModel e)
        {
            //don't see how we can make this work without making the class a singleton and edditing the List<Employees> in there
            //so the only thing you can really check is wheather or not we can get in client handler when this method is called I guess
            return true;
        }
    }
}