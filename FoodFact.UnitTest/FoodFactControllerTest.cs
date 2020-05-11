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
using System.Threading;
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



        [Fact]
        public void Test_Validate_ProductService()
        {
            //Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            // Setup Protected method on HttpMessageHandler mock.
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                {
                    HttpResponseMessage response = new HttpResponseMessage();

                    response.StatusCode = (HttpStatusCode)StatusCodes.Status200OK;
                    response.Content = new StringContent("OK");
                return response;
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);

            string url = @"https://us.openfoodfacts.org/cgi/search.pl?action=process&tagtype_0=categories
                                &tag_contains_0=contains&tag_0=breakfast_cereals
                                &tagtype_1=nutrition_grades&tag_contains_1=contains&tag_1=A
                                &additives=without&ingredients_from_palm_oil=without&json=true";

            var result = httpClient.GetAsync(url).Result;

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }


    }
}
