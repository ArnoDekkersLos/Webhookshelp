using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomRestServer.Models
{
    public class LeaderWithTeamMembersModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Tussenvoegsel { get; set; }
        public string LastName { get; set; }
        public List<EmployeeModel> TeamMembers { get; set; }

        public LeaderWithTeamMembersModel(EmployeeModel teamLeader)
        {
            Id = teamLeader.Id;
            FirstName = teamLeader.FirstName;
            Tussenvoegsel = teamLeader.Tussenvoegsel;
            LastName = teamLeader.LastName;
            TeamMembers = new List<EmployeeModel>();
        }
    }
}