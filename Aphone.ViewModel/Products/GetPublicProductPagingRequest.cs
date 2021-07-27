using Aphone.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aphone.ViewModel.Products
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
