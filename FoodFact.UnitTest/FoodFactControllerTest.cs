using FoodFact.Business;
using FoodFact.BusinessContract;
using FoodFact.DTO;
using FoodFact.MicroService;
using FoodFact.MicroService.Controllers;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Xunit;

namespace FoodFact.UnitTest
{
    public class FoodFactControllerTest
    {
        FoodFactResponse<Product> product = new FoodFactResponse<Product>
            {
                Count = 1,
                Page = "1",
                Page_Size = "10",
                Skip = 0,
                Products = new Product[] 
                { new Product 
                    { Product_Name="Test"
                    ,Ingredients=new Ingredient[]   { new Ingredient { Text="test1"}
} 
                    } 
                }
            };

        [Fact]
        public void Test_Valid_Limit()
        {
            //Arrange
            int limit = -1;
            Mock<IProductService> mock = new Mock<IProductService>();
            FoodFactController _controller = new FoodFactController(mock.Object);

            // Assert
            Assert.Throws<ArgumentException>(()=> _controller.Get("sugar", limit));
        }

        [Fact]
        public void Test_Get_ReturnsProducts()
        {
            //Arrange
            
            Mock<IProductService> mock = new Mock<IProductService>();
            FoodFactController _controller = new FoodFactController(mock.Object);

            mock.Setup(p => p.GetProductsByIngredients()).Returns(product);

            // Act
            ProductResponse[] productResponses = _controller.Get();

            // Assert
            
            Assert.Equal(1, productResponses.Length);
        }


    }
}
