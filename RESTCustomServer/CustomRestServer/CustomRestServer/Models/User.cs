using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomRestServer.Models
{
    public class User
    {
        public static object Identity { get; internal set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}