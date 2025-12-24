using GMAO.Domain.Common;
using GMAO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.ValueObjects
{
    public class AssignedTeam : ValueObject
    {
       
        public Staff Commercial { get; set; }
        public AssignedTeam() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
