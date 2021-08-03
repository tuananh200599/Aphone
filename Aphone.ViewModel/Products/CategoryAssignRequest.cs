using Aphone.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aphone.ViewModel.Products
{
    public class CategoryAssignRequest
    {
        public int Id { get; set; }
        public List<SelectItem> Categories { get; set; } = new List<SelectItem>();
    }
}
