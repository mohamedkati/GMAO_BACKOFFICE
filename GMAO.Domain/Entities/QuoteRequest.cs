using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    // APRES JE DOIS ReFLECHIR A METTRE UN LINK VERS LE CUSTOMER OU SITE OU AUTRE, et aussi dans leur utilité en général dans ce métier ( mais maintenant je pense que ca sera utile lors des interventions des techniciens qui nécessitent des devis pour valider des travaux )
    public class QuoteRequest : BaseAuditableEntity
    {
        public string Reference { get; set; } = string.Empty;
        public QuoteRequestStatus Status { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? InspectionScheduledAt { get; set; }
        public DateTime? InspectionCompletedAt { get; set; }
        public Guid? InspectedBy { get; set; }
        public string? InspectionNotes { get; set; }
        public Quote? Quote { get; set; }
    }
}
