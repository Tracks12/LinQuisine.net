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
        [Required(ErrorMessage = "Online statement is required")]
        public bool online { get; set; }
    }

    public class Version
    {
        [Required(ErrorMessage = "Version number is required")]
        public string version { get; set; }
    }
}
