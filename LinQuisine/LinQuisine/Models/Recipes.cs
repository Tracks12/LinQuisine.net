using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQuisine.Models
{
    public class Ingredient
    {
        [Required(ErrorMessage = "Ingredient name is required")]
        public string name { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
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

        [Required(ErrorMessage = "Recipe id is required")]
        public int id { get => id1; set => id1 = value; }

        [Required(ErrorMessage = "Recipe name is required")]
        public string name { get; set; }

        [Required(ErrorMessage = "Parts number is required")]
        public int nbParts { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public List<string> steps { get; set; }

        [Required(ErrorMessage = "User id is required")]
        public int userId { get; set; }
    }
}
