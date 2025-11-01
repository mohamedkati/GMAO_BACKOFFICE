using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException()
            : base("Vous n’avez pas la permission d’accéder à cette ressource.")
        {
        }

        public ForbiddenAccessException(string message)
            : base(message)
        {
        }
    }
}
