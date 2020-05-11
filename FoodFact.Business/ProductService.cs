using FoodFact.BusinessContract;
using FoodFact.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FoodFact.Business
{
    public class ProductService : IProductService
    {
        public FoodFactResponse<Product> GetProductsByIngredients()
        {
            string urlText = @"https://us.openfoodfacts.org/cgi/search.pl?action=process&tagtype_0=categories
                                &tag_contains_0=contains&tag_0=breakfast_cereals
                                &tagtype_1=nutrition_grades&tag_contains_1=contains&tag_1=A
                                &additives=without&ingredients_from_palm_oil=without&json=true";

            System.Uri uri = new System.Uri(urlText);
            var temp = GetAsync<FoodFactResponse<Product>>(uri).Result;

            //return new List<FoodFactResponse<Product>>();
            return temp;
        }

        private async Task<T> GetAsync<T>(Uri requestUrl)
        {
            HttpClient _httpClient = new HttpClient();

            var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
