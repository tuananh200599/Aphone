using Aphone.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aphone.ViewModel.Users
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
