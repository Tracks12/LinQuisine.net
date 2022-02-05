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

        public Recipe(int id, int userId, string name, int nbParts, List<Ingredient> ingredients, List<string> steps)
        {
            this.id = id;
            this.userId = userId;
            this.name = name;
            this.nbParts = nbParts;
            this.ingredients = ingredients;
            this.steps = steps;
        }

        public int id { get => id1; set => id1 = value; }
        public string name { get; set; }
        public int nbParts { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public List<string> steps { get; set; }
        public int userId { get; set; }
    }
}
