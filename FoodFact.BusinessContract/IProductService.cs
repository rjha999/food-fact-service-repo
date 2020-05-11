using FoodFact.DTO;
using System;
using System.Collections.Generic;

namespace FoodFact.BusinessContract
{
    public interface IProductService
    {
        FoodFactResponse<Product> GetProductsByIngredients();
    }
}
