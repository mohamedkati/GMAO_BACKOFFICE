using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.ValueObjects
{
    public class ServiceRequestOrigin : ValueObject
    {
        public string SourceType { get; init; } = string.Empty;
        public string? SourceIdentifier { get; init; }
        public string? SourceIPAddress { get; init; }
        public string? UserAgent { get; init; }
        public DateTime ReceivedAt { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
           yield return SourceType;
        }
    }
}
