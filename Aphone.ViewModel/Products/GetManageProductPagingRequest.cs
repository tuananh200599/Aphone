using Aphone.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aphone.ViewModel.Products
{
    public class GetManageProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        //public int? CategoryId { get; set; }
    }
}
