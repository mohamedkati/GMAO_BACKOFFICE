using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base("L’entité demandée est introuvable.")
        {
        }

        public NotFoundException(string name, object key)
            : base($"L’entité '{name}' ({key}) est introuvable.")
        {
        }
    }
}
