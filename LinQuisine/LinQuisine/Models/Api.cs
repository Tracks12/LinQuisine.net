using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQuisine.Models
{
    public class Status
    {
        public Status(bool online)
        {
            this.online = online;
        }

        [Required(ErrorMessage = "Online statement is required")]
        public bool online { get; set; }
    }

    public class Version
    {
        public Version(string version)
        {
            this.version = version;
        }

        [Required(ErrorMessage = "Version number is required")]
        public string version { get; set; }
    }
}
