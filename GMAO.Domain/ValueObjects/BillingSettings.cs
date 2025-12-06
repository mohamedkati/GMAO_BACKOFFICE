using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.ValueObjects
{
    public class BillingSettings : ValueObject
    {
        public BillingMode Mode { get; init; } = BillingMode.Centralized;
        public bool AutoGenerateInvoices { get; init; } = true;
        public InvoiceFrequency InvoiceFrequency { get; init; } = InvoiceFrequency.PerWorkOrder;
        public bool SendEmailNotifications { get; init; } = true;
        public bool ApplyLatePaymentFees { get; init; } = false;
        public decimal? LatePaymentFeePercent { get; init; }

        public static BillingSettings Default => new BillingSettings();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Mode;
            yield return AutoGenerateInvoices;
        }
    }

    public enum BillingMode
    {
        Centralized = 1,
        PerSite = 2,
        DistributedByTantièmes = 3
    }

    public enum InvoiceFrequency
    {
        PerWorkOrder = 1,
        Weekly = 2,
        Monthly = 3,
        Quarterly = 4
    }
}
