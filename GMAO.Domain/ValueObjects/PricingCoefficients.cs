using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.ValueObjects
{
    public class PricingCoefficients : ValueObject
    {
        public decimal LaborCoefficient { get; init; } = 1.30m;
        public decimal MaterialCoefficient { get; init; } = 1.25m;
        public decimal EquipmentCoefficient { get; init; } = 1.20m;
        public decimal SubcontractorCoefficient { get; init; } = 1.15m;

        public static PricingCoefficients Default => new PricingCoefficients();

        public PricingCoefficients(
            decimal laborCoefficient = 1.30m,
            decimal materialCoefficient = 1.25m,
            decimal equipmentCoefficient = 1.20m,
            decimal subcontractorCoefficient = 1.15m)
        {
            if (laborCoefficient < 1.0m)
                throw new ArgumentException("Labor coefficient must be >= 1.0");
            if (materialCoefficient < 1.0m)
                throw new ArgumentException("Material coefficient must be >= 1.0");
            if (equipmentCoefficient < 1.0m)
                throw new ArgumentException("Equipment coefficient must be >= 1.0");
            if (subcontractorCoefficient < 1.0m)
                throw new ArgumentException("Subcontractor coefficient must be >= 1.0");

            LaborCoefficient = laborCoefficient;
            MaterialCoefficient = materialCoefficient;
            EquipmentCoefficient = equipmentCoefficient;
            SubcontractorCoefficient = subcontractorCoefficient;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return LaborCoefficient;
            yield return MaterialCoefficient;
            yield return EquipmentCoefficient;
            yield return SubcontractorCoefficient;

        }
    }
}
