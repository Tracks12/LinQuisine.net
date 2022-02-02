using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQuisine.Models
{
    public class User
    {
        private int id1;
        public int id { get => id1; set => id1 = value; }
        public string username { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
    }

    public class Profile
    {
        private int id1;
        public int id { get => id1; set => id1 = value; }
        public string username { get; set; }
        public string mail { get; set; }
    }

    public class Register
    {
        public string username { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
    }

    public class Login
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class Logout
    {
        public bool success { get; set; }
    }
}
