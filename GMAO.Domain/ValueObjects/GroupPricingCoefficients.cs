using GMAO.Domain.Common;

namespace GMAO.Domain.ValueObjects
{
    public class GroupPricingCoefficients : ValueObject
    {
        public decimal LaborCoefficient { get; init; } = 1.30m;
        public decimal MaterialCoefficient { get; init; } = 1.25m;
        public decimal EquipmentCoefficient { get; init; } = 1.20m;
        public decimal SubcontractorCoefficient { get; init; } = 1.15m;

        // ⭐ NOUVEAUX : Remises volume
        public decimal VolumeDiscountPercent { get; init; } = 0m;
        public int MinimumAnnualRevenue { get; init; } = 0;

        // ⭐ NOUVEAU : Tarifs spéciaux
        public decimal? EmergencyCalloutFee { get; init; }
        public decimal? MonthlyMaintenanceFee { get; init; }

        public static GroupPricingCoefficients Default => new GroupPricingCoefficients();

        public GroupPricingCoefficients(
            decimal laborCoefficient = 1.30m,
            decimal materialCoefficient = 1.25m,
            decimal equipmentCoefficient = 1.20m,
            decimal subcontractorCoefficient = 1.15m,
            decimal volumeDiscountPercent = 0m,
            int minimumAnnualRevenue = 0)
        {
            if (laborCoefficient < 1.0m)
                throw new ArgumentException("Labor coefficient must be >= 1.0");
            if (materialCoefficient < 1.0m)
                throw new ArgumentException("Material coefficient must be >= 1.0");
            if (equipmentCoefficient < 1.0m)
                throw new ArgumentException("Equipment coefficient must be >= 1.0");
            if (subcontractorCoefficient < 1.0m)
                throw new ArgumentException("Subcontractor coefficient must be >= 1.0");
            if (volumeDiscountPercent < 0m || volumeDiscountPercent > 100m)
                throw new ArgumentException("Volume discount must be between 0 and 100");

            LaborCoefficient = laborCoefficient;
            MaterialCoefficient = materialCoefficient;
            EquipmentCoefficient = equipmentCoefficient;
            SubcontractorCoefficient = subcontractorCoefficient;
            VolumeDiscountPercent = volumeDiscountPercent;
            MinimumAnnualRevenue = minimumAnnualRevenue;
        }

        /// <summary>
        /// Applique les tarifs groupe à un Customer en fonction de son CA
        /// </summary>
        public PricingCoefficients ApplyToCustomer(decimal customerAnnualRevenue)
        {
            var discount = customerAnnualRevenue >= MinimumAnnualRevenue
                ? VolumeDiscountPercent / 100m
                : 0m;

            return new PricingCoefficients(
                laborCoefficient: LaborCoefficient * (1 - discount),
                materialCoefficient: MaterialCoefficient * (1 - discount),
                equipmentCoefficient: EquipmentCoefficient * (1 - discount),
                subcontractorCoefficient: SubcontractorCoefficient * (1 - discount)
            );
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
