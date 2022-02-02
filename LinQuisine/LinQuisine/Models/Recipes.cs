using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQuisine.Models
{
    public class Ingredient
    {
        public string name { get; set; }
        public int quantity  { get; set; }
        public string unit { get; set; }
    }

    public class Recipe
    {
        private int id1;
        public int id { get => id1; set => id1 = value; }
        public string name { get; set; }
        public int nbParts { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public List<string> steps { get; set; }
        public int userId { get; set; }
    }
}
