using System;
using System.Collections.Generic;
using System.Text;

namespace Ketan.Square2.Service.Authentication.Data.Interface
{
    public class ObjectExistsException : Exception
    {
        public ObjectExistsException(string message)
            : base(message)
        {
        }
    }
}
