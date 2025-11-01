using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class Address : BaseEntity<Guid>
    {
        public string FirstAddressLine { get; set; }
        public string SecondAddressLine { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public bool IsDefault { get; set; }

        protected Address() { }


        public Address(string firstAddressLine, string secondAddressLine, string street, string city, string postalCode, string country)
        {
            FirstAddressLine = firstAddressLine;
            SecondAddressLine = secondAddressLine;
            Street = street;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }

    }
}
