using GMAO.Domain.Entities;
using GMAO.Domain.Entities.siteAggregate;
using GMAO.Domain.Enums;
using GMAO.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GMAO.DATA.Seed.SeedData;

/// <summary>
/// Seeder pour les données de référence (lookup tables)
/// Ces données sont indépendantes et doivent être créées en premier
/// </summary>
public static class ReferenceDataSeeder
{
    #region IDs Constants - Pour références croisées

    // SectorTypes
    public static readonly Guid SectorType_Tertiaire = Guid.Parse("a1000001-0000-0000-0000-000000000001");
    public static readonly Guid SectorType_Residentiel = Guid.Parse("a1000001-0000-0000-0000-000000000002");
    public static readonly Guid SectorType_Industriel = Guid.Parse("a1000001-0000-0000-0000-000000000003");
    public static readonly Guid SectorType_Commercial = Guid.Parse("a1000001-0000-0000-0000-000000000004");
    public static readonly Guid SectorType_Sante = Guid.Parse("a1000001-0000-0000-0000-000000000005");
    public static readonly Guid SectorType_Enseignement = Guid.Parse("a1000001-0000-0000-0000-000000000006");

    // SiteCategories
    public static readonly Guid SiteCategory_Copropriete = Guid.Parse("a2000001-0000-0000-0000-000000000001");
    public static readonly Guid SiteCategory_BailleurSocial = Guid.Parse("a2000001-0000-0000-0000-000000000002");
    public static readonly Guid SiteCategory_Tertiaire = Guid.Parse("a2000001-0000-0000-0000-000000000003");
    public static readonly Guid SiteCategory_Commerce = Guid.Parse("a2000001-0000-0000-0000-000000000004");

    // SiteClientTypes
    public static readonly Guid ClientType_Syndic = Guid.Parse("a3000001-0000-0000-0000-000000000001");
    public static readonly Guid ClientType_ASL = Guid.Parse("a3000001-0000-0000-0000-000000000002");
    public static readonly Guid ClientType_HLM = Guid.Parse("a3000001-0000-0000-0000-000000000003");
    public static readonly Guid ClientType_SEM = Guid.Parse("a3000001-0000-0000-0000-000000000004");
    public static readonly Guid ClientType_Bureau = Guid.Parse("a3000001-0000-0000-0000-000000000005");
    public static readonly Guid ClientType_Boutique = Guid.Parse("a3000001-0000-0000-0000-000000000006");

    // ContactTypes
    public static readonly Guid ContactType_Technique = Guid.Parse("a4000001-0000-0000-0000-000000000001");
    public static readonly Guid ContactType_Administratif = Guid.Parse("a4000001-0000-0000-0000-000000000002");
    public static readonly Guid ContactType_Urgence = Guid.Parse("a4000001-0000-0000-0000-000000000003");
    public static readonly Guid ContactType_Gardien = Guid.Parse("a4000001-0000-0000-0000-000000000004");
    public static readonly Guid ContactType_Comptable = Guid.Parse("a4000001-0000-0000-0000-000000000005");
    public static readonly Guid ContactType_Direction = Guid.Parse("a4000001-0000-0000-0000-000000000006");

    // TVA
    public static readonly Guid TVA_20 = Guid.Parse("a5000001-0000-0000-0000-000000000001");
    public static readonly Guid TVA_10 = Guid.Parse("a5000001-0000-0000-0000-000000000002");
    public static readonly Guid TVA_5_5 = Guid.Parse("a5000001-0000-0000-0000-000000000003");
    public static readonly Guid TVA_0 = Guid.Parse("a5000001-0000-0000-0000-000000000004");

    // PaymentMethods
    public static readonly Guid Payment_Virement30J = Guid.Parse("a6000001-0000-0000-0000-000000000001");
    public static readonly Guid Payment_Virement45J = Guid.Parse("a6000001-0000-0000-0000-000000000002");
    public static readonly Guid Payment_Virement60J = Guid.Parse("a6000001-0000-0000-0000-000000000003");
    public static readonly Guid Payment_Cheque = Guid.Parse("a6000001-0000-0000-0000-000000000004");
    public static readonly Guid Payment_Prelevement = Guid.Parse("a6000001-0000-0000-0000-000000000005");

    // Skills
    public static readonly Guid Skill_Plomberie = Guid.Parse("a7000001-0000-0000-0000-000000000001");
    public static readonly Guid Skill_Electricite = Guid.Parse("a7000001-0000-0000-0000-000000000002");
    public static readonly Guid Skill_CVC = Guid.Parse("a7000001-0000-0000-0000-000000000003");
    public static readonly Guid Skill_Serrurerie = Guid.Parse("a7000001-0000-0000-0000-000000000004");
    public static readonly Guid Skill_Menuiserie = Guid.Parse("a7000001-0000-0000-0000-000000000005");
    public static readonly Guid Skill_Peinture = Guid.Parse("a7000001-0000-0000-0000-000000000006");
    public static readonly Guid Skill_Ascenseur = Guid.Parse("a7000001-0000-0000-0000-000000000007");
    public static readonly Guid Skill_Incendie = Guid.Parse("a7000001-0000-0000-0000-000000000008");
    public static readonly Guid Skill_MultiTechnique = Guid.Parse("a7000001-0000-0000-0000-000000000009");
    public static readonly Guid Skill_Toiture = Guid.Parse("a7000001-0000-0000-0000-000000000010");

    #endregion

    public static async Task SeedAsync(AppDbContext context, Guid tenantId)
    {
        await SeedSectorTypesAsync(context, tenantId);
        await SeedSiteCategoriesAsync(context, tenantId);
        await SeedSiteClientTypesAsync(context, tenantId);
        await SeedContactTypesAsync(context);
        await SeedTVAAsync(context);
        await SeedPaymentMethodsAsync(context, tenantId);
        await SeedSkillsAsync(context);
    }

    #region SectorTypes

    private static async Task SeedSectorTypesAsync(AppDbContext context, Guid tenantId)
    {
        if (await context.Set<SectorType>().AnyAsync())
            return;

        var sectorTypes = new List<SectorType>
        {
            new() { Id = SectorType_Tertiaire, Code = "TER", Description = "Tertiaire - Bureaux et services", TenantId = tenantId },
            new() { Id = SectorType_Residentiel, Code = "RES", Description = "Résidentiel - Habitations", TenantId = tenantId },
            new() { Id = SectorType_Industriel, Code = "IND", Description = "Industriel - Usines et entrepôts", TenantId = tenantId },
            new() { Id = SectorType_Commercial, Code = "COM", Description = "Commercial - Commerces et centres commerciaux", TenantId = tenantId },
            new() { Id = SectorType_Sante, Code = "SAN", Description = "Santé - Hôpitaux et cliniques", TenantId = tenantId },
            new() { Id = SectorType_Enseignement, Code = "ENS", Description = "Enseignement - Écoles et universités", TenantId = tenantId },
        };

        await context.Set<SectorType>().AddRangeAsync(sectorTypes);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ SectorTypes créés");
    }

    #endregion

    #region SiteCategories

    private static async Task SeedSiteCategoriesAsync(AppDbContext context, Guid tenantId)
    {
        if (await context.Set<SiteCategory>().AnyAsync())
            return;

        var categories = new List<SiteCategory>
        {
            new() { Id = SiteCategory_Copropriete, Code = "COPRO", Description = "Copropriété", TenantId = tenantId },
            new() { Id = SiteCategory_BailleurSocial, Code = "SOCIAL", Description = "Bailleur Social", TenantId = tenantId },
            new() { Id = SiteCategory_Tertiaire, Code = "TERT", Description = "Tertiaire", TenantId = tenantId },
            new() { Id = SiteCategory_Commerce, Code = "COMM", Description = "Commerce", TenantId = tenantId },
        };

        await context.Set<SiteCategory>().AddRangeAsync(categories);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ SiteCategories créées");
    }

    #endregion

    #region SiteClientTypes

    private static async Task SeedSiteClientTypesAsync(AppDbContext context, Guid tenantId)
    {
        if (await context.Set<SiteClientType>().AnyAsync())
            return;

        var clientTypes = new List<SiteClientType>
        {
            // Copropriété
            new() { Id = ClientType_Syndic, Code = "SYNDIC", SiteCategoryId = SiteCategory_Copropriete, TenantId = tenantId },
            new() { Id = ClientType_ASL, Code = "ASL", SiteCategoryId = SiteCategory_Copropriete, TenantId = tenantId },
            
            // Bailleur Social
            new() { Id = ClientType_HLM, Code = "HLM", SiteCategoryId = SiteCategory_BailleurSocial, TenantId = tenantId },
            new() { Id = ClientType_SEM, Code = "SEM", SiteCategoryId = SiteCategory_BailleurSocial, TenantId = tenantId },
            
            // Tertiaire
            new() { Id = ClientType_Bureau, Code = "BUREAU", SiteCategoryId = SiteCategory_Tertiaire, TenantId = tenantId },
            
            // Commerce
            new() { Id = ClientType_Boutique, Code = "BOUTIQUE", SiteCategoryId = SiteCategory_Commerce, TenantId = tenantId },
        };

        await context.Set<SiteClientType>().AddRangeAsync(clientTypes);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ SiteClientTypes créés");
    }

    #endregion

    #region ContactTypes

    private static async Task SeedContactTypesAsync(AppDbContext context)
    {
        if (await context.Set<ContactType>().AnyAsync())
            return;

        var contactTypes = new List<ContactType>
        {
            new() { Id = ContactType_Technique, Name = "Technique", Priority = 1 },
            new() { Id = ContactType_Administratif, Name = "Administratif", Priority = 2 },
            new() { Id = ContactType_Urgence, Name = "Urgence", Priority = 0 }, // Priorité haute
            new() { Id = ContactType_Gardien, Name = "Gardien", Priority = 1 },
            new() { Id = ContactType_Comptable, Name = "Comptable", Priority = 3 },
            new() { Id = ContactType_Direction, Name = "Direction", Priority = 2 },
        };

        await context.Set<ContactType>().AddRangeAsync(contactTypes);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ ContactTypes créés");
    }

    #endregion

    #region TVA

    private static async Task SeedTVAAsync(AppDbContext context)
    {
        if (await context.Set<TVA>().AnyAsync())
            return;

        var tvaRates = new List<TVA>
        {
            new() { Id = TVA_20, Code = "TVA20", Name = "TVA 20% - Taux normal", ValuRate = 20.0f },
            new() { Id = TVA_10, Code = "TVA10", Name = "TVA 10% - Taux intermédiaire (travaux rénovation)", ValuRate = 10.0f },
            new() { Id = TVA_5_5, Code = "TVA55", Name = "TVA 5.5% - Taux réduit (amélioration énergétique)", ValuRate = 5.5f },
            new() { Id = TVA_0, Code = "TVA0", Name = "Exonéré de TVA", ValuRate = 0.0f },
        };

        await context.Set<TVA>().AddRangeAsync(tvaRates);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ TVA créées");
    }

    #endregion

    #region PaymentMethods

    private static async Task SeedPaymentMethodsAsync(AppDbContext context, Guid tenantId)
    {
        if (await context.Set<GMAO.Domain.Entities.PaymentMethod>().AnyAsync())
            return;

        var paymentMethods = new List<GMAO.Domain.Entities.PaymentMethod>
        {
            new() 
            { 
                Id = Payment_Virement30J, 
                Name = "Virement 30 jours", 
                Terms = "Paiement par virement bancaire à 30 jours date de facture",
                Days = 30,
                DueDays = 30,
                TypeDueDate = DueDateType.Net,
                TenantId = tenantId
            },
            new() 
            { 
                Id = Payment_Virement45J, 
                Name = "Virement 45 jours fin de mois", 
                Terms = "Paiement par virement bancaire à 45 jours fin de mois",
                Days = 45,
                DueDays = 45,
                TypeDueDate = DueDateType.EndMonth,
                TenantId = tenantId
            },
            new() 
            { 
                Id = Payment_Virement60J, 
                Name = "Virement 60 jours", 
                Terms = "Paiement par virement bancaire à 60 jours date de facture",
                Days = 60,
                DueDays = 60,
                TypeDueDate = DueDateType.Net,
                TenantId = tenantId
            },
            new() 
            { 
                Id = Payment_Cheque, 
                Name = "Chèque à réception", 
                Terms = "Paiement par chèque à réception de facture",
                Days = 0,
                DueDays = 0,
                TypeDueDate = DueDateType.Net,
                TenantId = tenantId
            },
            new() 
            { 
                Id = Payment_Prelevement, 
                Name = "Prélèvement automatique", 
                Terms = "Prélèvement SEPA automatique",
                Days = 0,
                DueDays = 15,
                TypeDueDate = DueDateType.At,
                TenantId = tenantId
            },
        };

        await context.Set<GMAO.Domain.Entities.PaymentMethod>().AddRangeAsync(paymentMethods);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ PaymentMethods créés");
    }

    #endregion

    #region Skills

    private static async Task SeedSkillsAsync(AppDbContext context)
    {
        if (await context.Set<Skill>().AnyAsync())
            return;

        var skills = new List<Skill>
        {
            new() 
            { 
                Id = Skill_Plomberie, 
                Name = "Plomberie", 
                Description = "Installation et réparation des systèmes de plomberie, sanitaires, évacuations",
                Category = "Corps d'état technique"
            },
            new() 
            { 
                Id = Skill_Electricite, 
                Name = "Électricité", 
                Description = "Installation et maintenance des systèmes électriques, tableaux, éclairage",
                Category = "Corps d'état technique"
            },
            new() 
            { 
                Id = Skill_CVC, 
                Name = "CVC - Chauffage Ventilation Climatisation", 
                Description = "Maintenance des systèmes de chauffage, ventilation, climatisation, VMC",
                Category = "Corps d'état technique"
            },
            new() 
            { 
                Id = Skill_Serrurerie, 
                Name = "Serrurerie", 
                Description = "Installation et réparation de serrures, portes, contrôle d'accès",
                Category = "Second œuvre"
            },
            new() 
            { 
                Id = Skill_Menuiserie, 
                Name = "Menuiserie", 
                Description = "Travaux de menuiserie bois et PVC, portes, fenêtres, placards",
                Category = "Second œuvre"
            },
            new() 
            { 
                Id = Skill_Peinture, 
                Name = "Peinture - Revêtements", 
                Description = "Travaux de peinture, revêtements muraux et sols",
                Category = "Finitions"
            },
            new() 
            { 
                Id = Skill_Ascenseur, 
                Name = "Ascenseurs", 
                Description = "Maintenance et dépannage d'ascenseurs et monte-charges",
                Category = "Équipements spéciaux"
            },
            new() 
            { 
                Id = Skill_Incendie, 
                Name = "Sécurité Incendie", 
                Description = "Maintenance des systèmes de sécurité incendie, extincteurs, désenfumage",
                Category = "Sécurité"
            },
            new() 
            { 
                Id = Skill_MultiTechnique, 
                Name = "Multi-technique", 
                Description = "Compétences polyvalentes en maintenance multi-technique",
                Category = "Polyvalent"
            },
            new() 
            { 
                Id = Skill_Toiture, 
                Name = "Couverture - Étanchéité", 
                Description = "Travaux de toiture, étanchéité, zinguerie",
                Category = "Gros œuvre"
            },
        };

        await context.Set<Skill>().AddRangeAsync(skills);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ Skills créées");
    }

    #endregion
}
