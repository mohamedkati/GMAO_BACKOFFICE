namespace GMAO.Application.Features.sites.DTOs
{
    public class SiteContactTypeDto 
    {
        public string Name { get; set; } /// "Technique" | "Administratif" | "Urgence" | "Gardien"
        public int? Priority { get; set; } // Pour ordre d'appel
    }
}
