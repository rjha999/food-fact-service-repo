using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodFact.MicroService
{
    public class ProductResponse
    {
        public string ProductName { get; set; }
        public string[] AllIngredients { get; set; }
    }
}
