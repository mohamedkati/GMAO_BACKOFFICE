using GMAO.Domain.Common;
using GMAO.Domain.Events;
using GMAO.Domain.ValueObjects;

namespace GMAO.Domain.Entities
{
    public class PropertyGroup : BaseAuditableEntity
    {
        // ════════════════════════════════════════════════════════
        // INFORMATIONS DE BASE
        // ════════════════════════════════════════════════════════

        public string Reference { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public PropertyGroupType Type { get; set; } 
        public PropertyGroupStatus Status { get; set; } 

        // ════════════════════════════════════════════════════════
        // INFORMATIONS LÉGALES 
        // ════════════════════════════════════════════════════════

        public string? LegalName { get; set; }
        public string? SIREN { get; set; } // France: 9 chiffres
        public string? CompanyRegistrationNumber { get; set; } // International
        public string? VATNumber { get; set; }
        public LegalForm? LegalForm { get; set; }

        // ════════════════════════════════════════════════════════
        // ADRESSE SIÈGE SOCIAL  (Value Object)
        // ════════════════════════════════════════════════════════

        public Address? HeadquartersAddress { get; set; }

        // ════════════════════════════════════════════════════════
        // CONTACT PRINCIPAL
        // ════════════════════════════════════════════════════════

        public string? MainContactName { get; set; }
        public string? MainContactPosition { get; set; }
        public string? MainContactEmail { get; set; }
        public string? MainContactPhone { get; set; }
        public string? MainContactMobile { get; set; }

        // ════════════════════════════════════════════════════════
        // CONTACT COMPTABILITÉ 
        // ════════════════════════════════════════════════════════

        public string? AccountingContactName { get; set; }
        public string? AccountingContactEmail { get; set; }
        public string? AccountingContactPhone { get; set; }

        // ════════════════════════════════════════════════════════
        // PARAMÈTRES FACTURATION GROUPE 
        // ════════════════════════════════════════════════════════

        public bool ConsolidatedBilling { get; set; } = false;
        public int PaymentTermsDays { get; set; } = 30;
        public decimal? VolumeDiscountPercent { get; set; }
        public string? PreferredPaymentMethod { get; set; }

        // ════════════════════════════════════════════════════════
        // TARIFICATION GROUPE 
        // ════════════════════════════════════════════════════════

        public GroupPricingCoefficients? GroupPricingCoefficients { get; set; }

        // ════════════════════════════════════════════════════════
        // STATISTIQUES ⭐ NOUVEAU
        // ════════════════════════════════════════════════════════

        public int TotalCustomers { get; set; }
        public int TotalSites { get; set; }
        public int TotalUnits { get; set; }
        public decimal TotalAnnualRevenue { get; set; }
        public DateTime? LastStatisticsUpdateDate { get; set; }

        // ════════════════════════════════════════════════════════
        // CONTRAT CADRE
        // ════════════════════════════════════════════════════════

        public DateTime? FrameworkContractStartDate { get; set; }
        public DateTime? FrameworkContractEndDate { get; set; }
        public string? FrameworkContractReference { get; set; }
        public bool AutoRenewalFrameworkContract { get; set; }

        // ════════════════════════════════════════════════════════
        // NOTES / DOCUMENTS
        // ════════════════════════════════════════════════════════

        public string? InternalNotes { get; set; }
        public string? CommercialNotes { get; set; }

        // ════════════════════════════════════════════════════════
        // RELATIONS
        // ════════════════════════════════════════════════════════

        public ICollection<Customer> Customers { get; set; } = new List<Customer>();
        public ICollection<PropertyGroupContact> Contacts { get; set; } = new List<PropertyGroupContact>(); // ⭐ NOUVEAU

        // ════════════════════════════════════════════════════════
        // MÉTHODES MÉTIER 
        // ════════════════════════════════════════════════════════

        /// <summary>
        /// Met à jour les statistiques du groupe
        /// </summary>
        public void UpdateStatistics(int totalCustomers, int totalSites, int totalUnits, decimal annualRevenue)
        {
            TotalCustomers = totalCustomers;
            TotalSites = totalSites;
            TotalUnits = totalUnits;
            TotalAnnualRevenue = annualRevenue;
            LastStatisticsUpdateDate = DateTime.UtcNow;

            AddDomainEvent(new PropertyGroupStatisticsUpdatedEvent(Id, annualRevenue));
        }

        /// <summary>
        /// Vérifie si le contrat cadre arrive à expiration
        /// </summary>
        public bool IsContractExpiringSoon(int daysBeforeExpiry = 90)
        {
            if (!FrameworkContractEndDate.HasValue)
                return false;

            var daysUntilExpiry = (FrameworkContractEndDate.Value - DateTime.UtcNow).Days;
            return daysUntilExpiry > 0 && daysUntilExpiry <= daysBeforeExpiry;
        }

        /// <summary>
        /// Applique les tarifs groupe à un nouveau Customer
        /// </summary>
        public PricingCoefficients GetCustomerPricing(decimal customerAnnualRevenue)
        {
            if (GroupPricingCoefficients == null)
                return PricingCoefficients.Default;

            return GroupPricingCoefficients.ApplyToCustomer(customerAnnualRevenue);
        }
    }
}