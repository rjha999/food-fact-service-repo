using System;

namespace FoodFact.DTO
{
    public class Product
    {
        public string Product_Name { get; set; }

        public Ingredient[] Ingredients { get; set; }

    }
}
