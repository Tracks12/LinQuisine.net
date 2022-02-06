using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQuisine.Models
{
    public class Reponse
    {
        [Required(ErrorMessage = "Success state is required")]
        public bool success { get; set; }

        public string info { get; set; }
        public string error { get; set; }
    }
}
