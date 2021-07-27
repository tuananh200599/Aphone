using System;
using System.Collections.Generic;
using System.Text;

namespace Aphone.ViewModel.Products
{
    public class ProductVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DatedCreate { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public string Details { get; set; }
        public int Stock { get; set; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string SeoAlias { get; set; }
        public int CategoryId { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
    }
}
