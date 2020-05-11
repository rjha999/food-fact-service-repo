using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodFact.BusinessContract;
using FoodFact.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodFact.MicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodFactController : ControllerBase
    {
        private readonly IProductService _productService;
        public FoodFactController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: api/FoodFact
        [HttpGet]
        public ProductResponse[] Get(string ingredient="", int limit=20)
        {
            try
            {
                //Validation: Is Limit valid number
                if (limit <= 0)
                    throw new ArgumentException("Limit Cannot be zero or less");
                


                ProductResponse[] productResponses = _productService.GetProductsByIngredients()
                                                    .Products
                                                    .Select(x => new ProductResponse
                                                    {
                                                        ProductName = x.Product_Name,
                                                        AllIngredients = x.Ingredients.Select(y => y.Text).ToArray()
                                                    })
                                                    
                                                    .ToArray();

                List<ProductResponse> productResponsesAfterFilter = new List<ProductResponse>();

                foreach (var item in productResponses)
                {
                    if (item.AllIngredients.Any(x=>x.Contains(ingredient)))
                        productResponsesAfterFilter.Add(item);
                }


                return productResponsesAfterFilter.Take(limit).ToArray();
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        //// GET: api/FoodFact/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

       
    }
}
