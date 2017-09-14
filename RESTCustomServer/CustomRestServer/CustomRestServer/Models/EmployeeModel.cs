using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomRestServer.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Tussenvoegsel { get; set; }
        public string LastName { get; set; }

        public EmployeeModel(int id, string firstName, string tussenvoegsel, string lastName)
        {
            Id = id;
            FirstName = firstName;
            Tussenvoegsel = tussenvoegsel;
            LastName = lastName;
        }
    }
}