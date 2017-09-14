using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Tussenvoegsel { get; set; }
        public string LastName { get; set; }
        public List<Employee> TeamMembers { get; set; }

        public Employee(int id, string firstName, string tussenvoegsel, string lastName)
        {
            Id = id;
            FirstName = firstName;
            Tussenvoegsel = tussenvoegsel;
            LastName = lastName;
            TeamMembers = new List<Employee>();
        }
    }
}
