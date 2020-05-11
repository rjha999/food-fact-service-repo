using System;
using System.Collections.Generic;
using System.Text;

namespace FoodFact.DTO
{
    public class FoodFactResponse<T>
    {
        public int Skip { get; set; }
        public string Page_Size { get; set; }
        public T[] Products { get; set; }
        public string Page { get; set; }
        public int Count { get; set; }
    }
}
