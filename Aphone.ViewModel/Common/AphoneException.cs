using System;
using System.Collections.Generic;
using System.Text;

namespace Aphone.ViewModel.Common
{
    public class AphoneException : Exception
    {
        public AphoneException()
        {
        }

        public AphoneException(string message)
            : base(message)
        {
        }

        public AphoneException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
