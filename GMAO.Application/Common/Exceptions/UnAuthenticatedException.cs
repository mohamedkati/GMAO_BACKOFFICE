using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Exceptions
{
    public class UnAuthenticatedException : Exception 
    {
        public UnAuthenticatedException()
        {
            
        }

        public UnAuthenticatedException(string message):base(message) 
        {
            
        }
    }
}
