using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQuisine.Models
{
    public class User
    {
        private int id1;

        public User(int id, string username, string mail, string password, string token)
        {
            this.id = id;
            this.username = username;
            this.mail = mail;
            this.password = password;
            this.token = token;
        }

        [Required(ErrorMessage = "Id is required")]
        public int id { get => id1; set => id1 = value; }

        [Required(ErrorMessage = "Username is required")]
        public string username { get; set; }

        [Required(ErrorMessage = "Mail address is required")]
        public string mail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }

        [Required(ErrorMessage = "Token is required")]
        public string token { get; set; }
    }

    public class Profile
    {
        private int id1;

        public Profile(int id, string username, string mail, string token)
        {
            this.id = id;
            this.username = username;
            this.mail = mail;
            this.token = token;
        }

        [Required(ErrorMessage = "Id is required")]
        public int id { get => id1; set => id1 = value; }

        [Required(ErrorMessage = "Username is required")]
        public string username { get; set; }

        [Required(ErrorMessage = "Mail address is required")]
        public string mail { get; set; }

        [Required(ErrorMessage = "Token is required")]
        public string token { get; set; }
    }

    public class Register
    {
        public Register(string username, string mail, string password)
        {
            this.username = username;
            this.mail = mail;
            this.password = password;
        }

        [Required(ErrorMessage = "Username is required")]
        public string username { get; set; }
        
        [Required(ErrorMessage = "Mail Address is required")]
        public string mail { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
    }

    public class Login
    {
        public Login(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        [Required(ErrorMessage = "Username is required")]
        public string username { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
    }
}
