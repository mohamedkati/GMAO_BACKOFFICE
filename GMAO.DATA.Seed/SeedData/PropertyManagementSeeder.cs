using GMAO.Domain.Entities;
using GMAO.Domain.Entities.siteAggregate;
using GMAO.Domain.Enums;
using GMAO.Domain.ValueObjects;
using GMAO.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GMAO.DATA.Seed.SeedData;

/// <summary>
/// Seeder pour la gestion du patrimoine immobilier
/// Sites, Units, Occupants et contacts de site
/// </summary>
public static class PropertyManagementSeeder
{
    #region Site IDs

    // Sites Foncia Paris Nord
    public static readonly Guid Site_Residence_Chapelle = Guid.Parse("e1000001-0000-0000-0000-000000000001");
    public static readonly Guid Site_Immeuble_Ordener = Guid.Parse("e1000001-0000-0000-0000-000000000002");
    public static readonly Guid Site_Copro_Montmartre = Guid.Parse("e1000001-0000-0000-0000-000000000003");

    // Sites Foncia Paris Sud
    public static readonly Guid Site_Residence_Italie = Guid.Parse("e1000001-0000-0000-0000-000000000004");
    public static readonly Guid Site_Immeuble_Convention = Guid.Parse("e1000001-0000-0000-0000-000000000005");

    // Sites Paris Habitat Est
    public static readonly Guid Site_Ensemble_Belleville = Guid.Parse("e1000001-0000-0000-0000-000000000006");
    public static readonly Guid Site_Tour_Flandre = Guid.Parse("e1000001-0000-0000-0000-000000000007");
    public static readonly Guid Site_Residence_Menilmontant = Guid.Parse("e1000001-0000-0000-0000-000000000008");

    // Sites Paris Habitat Ouest
    public static readonly Guid Site_Groupe_Leblanc = Guid.Parse("e1000001-0000-0000-0000-000000000009");
    public static readonly Guid Site_Residence_Brancion = Guid.Parse("e1000001-0000-0000-0000-000000000010");

    // Sites clients indépendants
    public static readonly Guid Site_SCI_Monceau_1 = Guid.Parse("e1000001-0000-0000-0000-000000000011");
    public static readonly Guid Site_ASL_Jardins = Guid.Parse("e1000001-0000-0000-0000-000000000012");
    public static readonly Guid Site_Rivoli = Guid.Parse("e1000001-0000-0000-0000-000000000013");
    public static readonly Guid Site_Tour_Defense = Guid.Parse("e1000001-0000-0000-0000-000000000014");
    public static readonly Guid Site_Beaugrenelle = Guid.Parse("e1000001-0000-0000-0000-000000000015");

    #endregion

    #region Unit IDs (exemples)

    public static readonly Guid Unit_Chapelle_A01 = Guid.Parse("e2000001-0000-0000-0000-000000000001");
    public static readonly Guid Unit_Chapelle_A02 = Guid.Parse("e2000001-0000-0000-0000-000000000002");
    public static readonly Guid Unit_Chapelle_B01 = Guid.Parse("e2000001-0000-0000-0000-000000000003");
    public static readonly Guid Unit_Belleville_101 = Guid.Parse("e2000001-0000-0000-0000-000000000004");
    public static readonly Guid Unit_Belleville_102 = Guid.Parse("e2000001-0000-0000-0000-000000000005");
    public static readonly Guid Unit_Belleville_201 = Guid.Parse("e2000001-0000-0000-0000-000000000006");
    public static readonly Guid Unit_Defense_Etage5 = Guid.Parse("e2000001-0000-0000-0000-000000000007");
    public static readonly Guid Unit_Defense_Etage10 = Guid.Parse("e2000001-0000-0000-0000-000000000008");

    #endregion

    public static async Task SeedAsync(AppDbContext context, Guid tenantId)
    {
        await SeedSitesAsync(context, tenantId);
        await SeedUnitsAsync(context, tenantId);
        await SeedOccupantsAsync(context, tenantId);
        await SeedSiteContactsAsync(context, tenantId);
        await SeedSiteKeepersAsync(context, tenantId);
    }

    #region Sites

    private static async Task SeedSitesAsync(AppDbContext context, Guid tenantId)
    {
        if (await context.Sites.AnyAsync())
            return;

        var sites = new List<Site>
        {
            // ══════════════════════════════════════════════════════════════════
            // SITES FONCIA PARIS NORD
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Site_Residence_Chapelle,
                TenantId = tenantId,
                Reference = "SITE-FON-CHAP",
                Name = "Résidence La Chapelle",
                Type = SiteType.ResidentialBuilding,
                CustomerId = CustomerManagementSeeder.Customer_Foncia_ParisNord,
                SectorTypeId = ReferenceDataSeeder.SectorType_Residentiel,
                ClientTypeId = ReferenceDataSeeder.ClientType_Syndic,
                VatId = ReferenceDataSeeder.TVA_10,
                Address = new Address("45 Rue de la Chapelle", "Paris", "75018", "France"),
                BillingAddress = new Address("45 Rue de la Chapelle", "Paris", "75018", "France"),
                Coordinates = new GeoCoordinates(48.8924, 2.3599),
                BuildingYear = 1965,
                TotalArea = 3500m,
                FloorsCount = 8,
                UnitsCount = 48,
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_Senior,
                OperationsManagerId = StaffAndRolesSeeder.Staff_ResponsableExploitation_Paris,
                SectorManagerId = StaffAndRolesSeeder.Staff_ChefSecteur_Paris_Nord,
                Technician1Id = StaffAndRolesSeeder.Staff_Technicien_MultiTechnique_1,
                Technician2Id = StaffAndRolesSeeder.Staff_Technicien_Plombier_1,
                PaymentMethodId = ReferenceDataSeeder.Payment_Virement45J,
                Siren = "345678901",
                Siret = "34567890100012",
                MainMailAddress = "syndic@residence-chapelle.fr",
                InvoiceMailAddress = "facturation@residence-chapelle.fr",
                Comment = "Résidence années 60 - Ravalement prévu 2025",
                CommentReport = "Attention: Chaudière collective ancienne - Remplacement programmé",
                SiteAccessInfo = new SiteAccess
                {
                    AccessCodes = "A1234 - B5678",
                    KeyInstructions = "Clé gardienne - Badge parking au local technique",
                    RequiresBadge = true,
                    WorkingHours = "8h-18h du lundi au vendredi",
                    AccessRestrictions = "Accès parking souterrain interdit aux véhicules > 2m",
                    ParkingInfo = "Places visiteurs disponibles niveau -1",
                    GeneralInstructions = "Se présenter à la loge gardienne à l'arrivée"
                },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Site_Immeuble_Ordener,
                TenantId = tenantId,
                Reference = "SITE-FON-ORD",
                Name = "Immeuble Ordener",
                Type = SiteType.ResidentialBuilding,
                CustomerId = CustomerManagementSeeder.Customer_Foncia_ParisNord,
                SectorTypeId = ReferenceDataSeeder.SectorType_Residentiel,
                ClientTypeId = ReferenceDataSeeder.ClientType_Syndic,
                VatId = ReferenceDataSeeder.TVA_10,
                Address = new Address("78 Rue Ordener", "Paris", "75018", "France"),
                BillingAddress = new Address("78 Rue Ordener", "Paris", "75018", "France"),
                Coordinates = new GeoCoordinates(48.8912, 2.3456),
                BuildingYear = 1925,
                TotalArea = 2200m,
                FloorsCount = 6,
                UnitsCount = 24,
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_Senior,
                OperationsManagerId = StaffAndRolesSeeder.Staff_ResponsableExploitation_Paris,
                SectorManagerId = StaffAndRolesSeeder.Staff_ChefSecteur_Paris_Nord,
                Technician1Id = StaffAndRolesSeeder.Staff_Technicien_Electricien_1,
                PaymentMethodId = ReferenceDataSeeder.Payment_Virement45J,
                Comment = "Immeuble haussmannien classé - Travaux soumis ABF",
                SiteAccessInfo = new SiteAccess
                {
                    AccessCodes = "4589B",
                    KeyInstructions = "Clés au cabinet Foncia Paris Nord",
                    RequiresBadge = false,
                    WorkingHours = "Accès libre parties communes"
                },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Site_Copro_Montmartre,
                TenantId = tenantId,
                Reference = "SITE-FON-MONT",
                Name = "Copropriété Montmartre Village",
                Type = SiteType.MixedUse,
                CustomerId = CustomerManagementSeeder.Customer_Foncia_ParisNord,
                SectorTypeId = ReferenceDataSeeder.SectorType_Residentiel,
                ClientTypeId = ReferenceDataSeeder.ClientType_Syndic,
                VatId = ReferenceDataSeeder.TVA_10,
                Address = new Address("15 Rue Lepic", "Paris", "75018", "France"),
                BillingAddress = new Address("15 Rue Lepic", "Paris", "75018", "France"),
                BuildingYear = 1890,
                TotalArea = 1800m,
                FloorsCount = 5,
                UnitsCount = 18,
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_Senior,
                SectorManagerId = StaffAndRolesSeeder.Staff_ChefSecteur_Paris_Nord,
                Comment = "2 commerces en RDC - Accès par cour intérieure",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // ══════════════════════════════════════════════════════════════════
            // SITES FONCIA PARIS SUD
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Site_Residence_Italie,
                TenantId = tenantId,
                Reference = "SITE-FON-ITA",
                Name = "Résidence Place d'Italie",
                Type = SiteType.ResidentialBuilding,
                CustomerId = CustomerManagementSeeder.Customer_Foncia_ParisSud,
                SectorTypeId = ReferenceDataSeeder.SectorType_Residentiel,
                ClientTypeId = ReferenceDataSeeder.ClientType_Syndic,
                VatId = ReferenceDataSeeder.TVA_10,
                Address = new Address("25 Place d'Italie", "Paris", "75013", "France"),
                BillingAddress = new Address("120 Avenue d'Italie", "Paris", "75013", "France"),
                Coordinates = new GeoCoordinates(48.8312, 2.3556),
                BuildingYear = 1972,
                TotalArea = 5500m,
                FloorsCount = 15,
                UnitsCount = 85,
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_Senior,
                OperationsManagerId = StaffAndRolesSeeder.Staff_ResponsableExploitation_Paris,
                SectorManagerId = StaffAndRolesSeeder.Staff_ChefSecteur_Paris_Sud,
                Technician1Id = StaffAndRolesSeeder.Staff_Technicien_CVC,
                Technician2Id = StaffAndRolesSeeder.Staff_Technicien_Electricien_2,
                PaymentMethodId = ReferenceDataSeeder.Payment_Virement45J,
                Comment = "Tour années 70 - Chauffage collectif gaz",
                SiteAccessInfo = new SiteAccess
                {
                    AccessCodes = "Digicode 7845A - Interphone",
                    KeyInstructions = "Badge magnétique obligatoire",
                    RequiresBadge = true,
                    WorkingHours = "Loge gardien 7h-20h",
                    SafetyRequirements = "EPI obligatoires en chaufferie",
                    ParkingInfo = "Parking souterrain 3 niveaux - Accès badge"
                },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Site_Immeuble_Convention,
                TenantId = tenantId,
                Reference = "SITE-FON-CONV",
                Name = "Immeuble Convention",
                Type = SiteType.ResidentialBuilding,
                CustomerId = CustomerManagementSeeder.Customer_Foncia_ParisSud,
                SectorTypeId = ReferenceDataSeeder.SectorType_Residentiel,
                ClientTypeId = ReferenceDataSeeder.ClientType_Syndic,
                VatId = ReferenceDataSeeder.TVA_10,
                Address = new Address("156 Rue de la Convention", "Paris", "75015", "France"),
                BillingAddress = new Address("156 Rue de la Convention", "Paris", "75015", "France"),
                BuildingYear = 1955,
                TotalArea = 2800m,
                FloorsCount = 7,
                UnitsCount = 35,
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_Senior,
                SectorManagerId = StaffAndRolesSeeder.Staff_ChefSecteur_Paris_Sud,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // ══════════════════════════════════════════════════════════════════
            // SITES PARIS HABITAT EST
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Site_Ensemble_Belleville,
                TenantId = tenantId,
                Reference = "SITE-PH-BELL",
                Name = "Ensemble Belleville - Bâtiments A-D",
                Type = SiteType.ResidentialBuilding,
                CustomerId = CustomerManagementSeeder.Customer_ParisHabitat_Est,
                SectorTypeId = ReferenceDataSeeder.SectorType_Residentiel,
                ClientTypeId = ReferenceDataSeeder.ClientType_HLM,
                VatId = ReferenceDataSeeder.TVA_10,
                Address = new Address("45-55 Rue de Belleville", "Paris", "75020", "France"),
                BillingAddress = new Address("21 bis Rue Claude Bernard", "Paris", "75005", "France"),
                Coordinates = new GeoCoordinates(48.8712, 2.3845),
                BuildingYear = 1968,
                TotalArea = 12000m,
                FloorsCount = 12,
                UnitsCount = 180,
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_GrandsComptes,
                OperationsManagerId = StaffAndRolesSeeder.Staff_ResponsableExploitation_Paris,
                SectorManagerId = StaffAndRolesSeeder.Staff_ChefSecteur_93_94,
                Technician1Id = StaffAndRolesSeeder.Staff_Technicien_MultiTechnique_1,
                Technician2Id = StaffAndRolesSeeder.Staff_Technicien_MultiTechnique_2,
                PaymentMethodId = ReferenceDataSeeder.Payment_Virement60J,
                Comment = "Ensemble HLM 4 bâtiments - Réhabilitation thermique en cours",
                CommentReport = "Chantier isolation façades Bât. A et B - Fin prévue mars 2025",
                SiteAccessInfo = new SiteAccess
                {
                    AccessCodes = "Vigik + Code gardien",
                    KeyInstructions = "Pass Paris Habitat obligatoire",
                    RequiresBadge = true,
                    WorkingHours = "Gardien sur place 7h-19h",
                    SafetyRequirements = "Port du gilet fluo obligatoire sur le chantier"
                },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Site_Tour_Flandre,
                TenantId = tenantId,
                Reference = "SITE-PH-FLAN",
                Name = "Tour Flandre",
                Type = SiteType.ResidentialBuilding,
                CustomerId = CustomerManagementSeeder.Customer_ParisHabitat_Est,
                SectorTypeId = ReferenceDataSeeder.SectorType_Residentiel,
                ClientTypeId = ReferenceDataSeeder.ClientType_HLM,
                VatId = ReferenceDataSeeder.TVA_10,
                Address = new Address("89 Avenue de Flandre", "Paris", "75019", "France"),
                BillingAddress = new Address("21 bis Rue Claude Bernard", "Paris", "75005", "France"),
                Coordinates = new GeoCoordinates(48.8923, 2.3789),
                BuildingYear = 1975,
                TotalArea = 8500m,
                FloorsCount = 22,
                UnitsCount = 120,
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_GrandsComptes,
                SectorManagerId = StaffAndRolesSeeder.Staff_ChefSecteur_93_94,
                Technician1Id = StaffAndRolesSeeder.Staff_Technicien_Plombier_2,
                Comment = "IGH - Normes sécurité incendie renforcées",
                SiteAccessInfo = new SiteAccess
                {
                    RequiresBadge = true,
                    SafetyRequirements = "Formation IGH obligatoire pour intervention",
                    GeneralInstructions = "Signalement obligatoire au PC sécurité"
                },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Site_Residence_Menilmontant,
                TenantId = tenantId,
                Reference = "SITE-PH-MENI",
                Name = "Résidence Ménilmontant",
                Type = SiteType.ResidentialBuilding,
                CustomerId = CustomerManagementSeeder.Customer_ParisHabitat_Est,
                SectorTypeId = ReferenceDataSeeder.SectorType_Residentiel,
                ClientTypeId = ReferenceDataSeeder.ClientType_HLM,
                VatId = ReferenceDataSeeder.TVA_10,
                Address = new Address("23 Rue de Ménilmontant", "Paris", "75020", "France"),
                BillingAddress = new Address("21 bis Rue Claude Bernard", "Paris", "75005", "France"),
                BuildingYear = 1982,
                TotalArea = 4500m,
                FloorsCount = 8,
                UnitsCount = 65,
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_GrandsComptes,
                SectorManagerId = StaffAndRolesSeeder.Staff_ChefSecteur_93_94,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // ══════════════════════════════════════════════════════════════════
            // SITES PARIS HABITAT OUEST
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Site_Groupe_Leblanc,
                TenantId = tenantId,
                Reference = "SITE-PH-LEBL",
                Name = "Groupe Leblanc",
                Type = SiteType.ResidentialBuilding,
                CustomerId = CustomerManagementSeeder.Customer_ParisHabitat_Ouest,
                SectorTypeId = ReferenceDataSeeder.SectorType_Residentiel,
                ClientTypeId = ReferenceDataSeeder.ClientType_HLM,
                VatId = ReferenceDataSeeder.TVA_10,
                Address = new Address("35-45 Rue Leblanc", "Paris", "75015", "France"),
                BillingAddress = new Address("21 bis Rue Claude Bernard", "Paris", "75005", "France"),
                BuildingYear = 1970,
                TotalArea = 9500m,
                FloorsCount = 10,
                UnitsCount = 140,
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_GrandsComptes,
                SectorManagerId = StaffAndRolesSeeder.Staff_ChefSecteur_Paris_Sud,
                Technician1Id = StaffAndRolesSeeder.Staff_Technicien_CVC,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Site_Residence_Brancion,
                TenantId = tenantId,
                Reference = "SITE-PH-BRAN",
                Name = "Résidence Brancion",
                Type = SiteType.ResidentialBuilding,
                CustomerId = CustomerManagementSeeder.Customer_ParisHabitat_Ouest,
                SectorTypeId = ReferenceDataSeeder.SectorType_Residentiel,
                ClientTypeId = ReferenceDataSeeder.ClientType_HLM,
                VatId = ReferenceDataSeeder.TVA_10,
                Address = new Address("12 Rue Brancion", "Paris", "75015", "France"),
                BillingAddress = new Address("21 bis Rue Claude Bernard", "Paris", "75005", "France"),
                BuildingYear = 1985,
                TotalArea = 3200m,
                FloorsCount = 6,
                UnitsCount = 45,
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_GrandsComptes,
                SectorManagerId = StaffAndRolesSeeder.Staff_ChefSecteur_Paris_Sud,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // ══════════════════════════════════════════════════════════════════
            // SITES CLIENTS INDÉPENDANTS
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Site_SCI_Monceau_1,
                TenantId = tenantId,
                Reference = "SITE-SCI-MON1",
                Name = "25 Boulevard Malesherbes",
                Type = SiteType.ResidentialBuilding,
                CustomerId = CustomerManagementSeeder.Customer_SCI_Monceau,
                SectorTypeId = ReferenceDataSeeder.SectorType_Residentiel,
                ClientTypeId = ReferenceDataSeeder.ClientType_Syndic,
                VatId = ReferenceDataSeeder.TVA_10,
                Address = new Address("25 Boulevard Malesherbes", "Paris", "75008", "France"),
                BillingAddress = new Address("25 Boulevard Malesherbes", "Paris", "75008", "France"),
                BuildingYear = 1880,
                TotalArea = 1500m,
                FloorsCount = 5,
                UnitsCount = 12,
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_Senior,
                SectorManagerId = StaffAndRolesSeeder.Staff_ChefSecteur_Paris_Nord,
                Comment = "Immeuble de prestige - Pierre de taille",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Site_ASL_Jardins,
                TenantId = tenantId,
                Reference = "SITE-ASL-JARD",
                Name = "Les Jardins de Neuilly",
                Type = SiteType.ResidentialBuilding,
                CustomerId = CustomerManagementSeeder.Customer_ASL_Jardins,
                SectorTypeId = ReferenceDataSeeder.SectorType_Residentiel,
                ClientTypeId = ReferenceDataSeeder.ClientType_ASL,
                VatId = ReferenceDataSeeder.TVA_10,
                Address = new Address("45 Avenue Charles de Gaulle", "Neuilly-sur-Seine", "92200", "France"),
                BillingAddress = new Address("45 Avenue Charles de Gaulle", "Neuilly-sur-Seine", "92200", "France"),
                Coordinates = new GeoCoordinates(48.8845, 2.2678),
                BuildingYear = 1998,
                TotalArea = 8500m,
                FloorsCount = 6,
                UnitsCount = 95,
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_Senior,
                SectorManagerId = StaffAndRolesSeeder.Staff_ChefSecteur_92,
                Technician1Id = StaffAndRolesSeeder.Staff_Technicien_MultiTechnique_2,
                Comment = "Résidence standing avec piscine et tennis",
                SiteAccessInfo = new SiteAccess
                {
                    AccessCodes = "Badge résident uniquement",
                    RequiresBadge = true,
                    WorkingHours = "Gardien 24/7",
                    ParkingInfo = "Parking souterrain 2 niveaux"
                },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Site_Rivoli,
                TenantId = tenantId,
                Reference = "SITE-COP-RIV",
                Name = "156 Rue de Rivoli",
                Type = SiteType.MixedUse,
                CustomerId = CustomerManagementSeeder.Customer_Copro_Rivoli,
                SectorTypeId = ReferenceDataSeeder.SectorType_Residentiel,
                ClientTypeId = ReferenceDataSeeder.ClientType_Syndic,
                VatId = ReferenceDataSeeder.TVA_10,
                Address = new Address("156 Rue de Rivoli", "Paris", "75001", "France"),
                BillingAddress = new Address("156 Rue de Rivoli", "Paris", "75001", "France"),
                BuildingYear = 1850,
                TotalArea = 2000m,
                FloorsCount = 6,
                UnitsCount = 15,
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_Junior,
                Comment = "Immeuble historique - Commerce en RDC",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Site_Tour_Defense,
                TenantId = tenantId,
                Reference = "SITE-TER-DEF",
                Name = "Tour Areva",
                Type = SiteType.Office,
                CustomerId = CustomerManagementSeeder.Customer_Bureau_Defense,
                SectorTypeId = ReferenceDataSeeder.SectorType_Tertiaire,
                ClientTypeId = ReferenceDataSeeder.ClientType_Bureau,
                VatId = ReferenceDataSeeder.TVA_20,
                Address = new Address("1 Place Jean Millier", "Courbevoie", "92400", "France"),
                BillingAddress = new Address("1 Place Jean Millier", "Courbevoie", "92400", "France"),
                Coordinates = new GeoCoordinates(48.8925, 2.2378),
                BuildingYear = 2008,
                TotalArea = 45000m,
                FloorsCount = 35,
                UnitsCount = 35, // 1 plateau par étage
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_GrandsComptes,
                OperationsManagerId = StaffAndRolesSeeder.Staff_ResponsableExploitation_IDF,
                SectorManagerId = StaffAndRolesSeeder.Staff_ChefSecteur_92,
                Technician1Id = StaffAndRolesSeeder.Staff_Technicien_CVC,
                Technician2Id = StaffAndRolesSeeder.Staff_Technicien_Electricien_1,
                Comment = "IGH Bureaux - Contrat multi-technique P3",
                CommentReport = "GTB centralisée - Astreinte 24/7",
                SiteAccessInfo = new SiteAccess
                {
                    RequiresBadge = true,
                    SafetyRequirements = "Habilitation IGH + Formation sécurité obligatoire",
                    WorkingHours = "PC Sécurité 24/7",
                    GeneralInstructions = "Enregistrement obligatoire au PC sécurité - Pièce d'identité"
                },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Site_Beaugrenelle,
                TenantId = tenantId,
                Reference = "SITE-COM-BEAU",
                Name = "Centre Commercial Beaugrenelle",
                Type = SiteType.RetailStore,
                CustomerId = CustomerManagementSeeder.Customer_Centre_Beaugrenelle,
                SectorTypeId = ReferenceDataSeeder.SectorType_Commercial,
                ClientTypeId = ReferenceDataSeeder.ClientType_Boutique,
                VatId = ReferenceDataSeeder.TVA_20,
                Address = new Address("12 Rue Linois", "Paris", "75015", "France"),
                BillingAddress = new Address("12 Rue Linois", "Paris", "75015", "France"),
                Coordinates = new GeoCoordinates(48.8456, 2.2789),
                BuildingYear = 2013,
                TotalArea = 52000m,
                FloorsCount = 5,
                UnitsCount = 130, // Boutiques
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_GrandsComptes,
                OperationsManagerId = StaffAndRolesSeeder.Staff_ResponsableExploitation_Paris,
                SectorManagerId = StaffAndRolesSeeder.Staff_ChefSecteur_Paris_Sud,
                Comment = "Centre commercial 3 bâtiments - Ouvert 7j/7",
                SiteAccessInfo = new SiteAccess
                {
                    AccessCodes = "Badge prestataire obligatoire",
                    RequiresBadge = true,
                    WorkingHours = "Interventions parties communes: 6h-10h ou 21h-23h",
                    SafetyRequirements = "Gilet fluo + chaussures de sécurité",
                    ParkingInfo = "Parking livraison niveau -2"
                },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
        };

        await context.Sites.AddRangeAsync(sites);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ Sites créés");
    }

    #endregion

    #region Units

    private static async Task SeedUnitsAsync(AppDbContext context, Guid tenantId)
    {
        if (await context.Units.AnyAsync())
            return;

        var units = new List<Unit>();

        // ══════════════════════════════════════════════════════════════════
        // UNITS RÉSIDENCE LA CHAPELLE (exemples)
        // ══════════════════════════════════════════════════════════════════
        units.AddRange(new[]
        {
            new Unit
            {
                Id = Unit_Chapelle_A01,
                TenantId = tenantId,
                Reference = "CHAP-A01",
                SiteId = Site_Residence_Chapelle,
                Type = UnitType.Apartment,
                Status = UnitStatus.Occupied,
                Floor = "RDC",
                DoorNumber = "A01",
                SurfaceArea = 65m,
                Rooms = 3,
                OwnershipSharesCount = 450,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new Unit
            {
                Id = Unit_Chapelle_A02,
                TenantId = tenantId,
                Reference = "CHAP-A02",
                SiteId = Site_Residence_Chapelle,
                Type = UnitType.Apartment,
                Status = UnitStatus.Occupied,
                Floor = "RDC",
                DoorNumber = "A02",
                SurfaceArea = 45m,
                Rooms = 2,
                OwnershipSharesCount = 320,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new Unit
            {
                Id = Unit_Chapelle_B01,
                TenantId = tenantId,
                Reference = "CHAP-B01",
                SiteId = Site_Residence_Chapelle,
                Type = UnitType.Apartment,
                Status = UnitStatus.Vacant,
                Floor = "1er",
                DoorNumber = "B01",
                SurfaceArea = 85m,
                Rooms = 4,
                OwnershipSharesCount = 580,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
        });

        // ══════════════════════════════════════════════════════════════════
        // UNITS ENSEMBLE BELLEVILLE (exemples)
        // ══════════════════════════════════════════════════════════════════
        units.AddRange(new[]
        {
            new Unit
            {
                Id = Unit_Belleville_101,
                TenantId = tenantId,
                Reference = "BELL-A-101",
                SiteId = Site_Ensemble_Belleville,
                Type = UnitType.Apartment,
                Status = UnitStatus.Occupied,
                Floor = "1er",
                DoorNumber = "101",
                SurfaceArea = 55m,
                Rooms = 3,
                OwnershipSharesCount = 0, // HLM
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new Unit
            {
                Id = Unit_Belleville_102,
                TenantId = tenantId,
                Reference = "BELL-A-102",
                SiteId = Site_Ensemble_Belleville,
                Type = UnitType.Apartment,
                Status = UnitStatus.Occupied,
                Floor = "1er",
                DoorNumber = "102",
                SurfaceArea = 70m,
                Rooms = 4,
                OwnershipSharesCount = 0,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new Unit
            {
                Id = Unit_Belleville_201,
                TenantId = tenantId,
                Reference = "BELL-A-201",
                SiteId = Site_Ensemble_Belleville,
                Type = UnitType.Apartment,
                Status = UnitStatus.UnderRenovation,
                Floor = "2ème",
                DoorNumber = "201",
                SurfaceArea = 55m,
                Rooms = 3,
                OwnershipSharesCount = 0,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
        });

        // ══════════════════════════════════════════════════════════════════
        // UNITS TOUR DÉFENSE (plateaux de bureaux)
        // ══════════════════════════════════════════════════════════════════
        units.AddRange(new[]
        {
            new Unit
            {
                Id = Unit_Defense_Etage5,
                TenantId = tenantId,
                Reference = "DEF-E05",
                SiteId = Site_Tour_Defense,
                Type = UnitType.Office,
                Status = UnitStatus.Occupied,
                Floor = "5ème",
                DoorNumber = "Plateau 5",
                SurfaceArea = 1200m,
                Rooms = 1,
                OwnershipSharesCount = 0,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new Unit
            {
                Id = Unit_Defense_Etage10,
                TenantId = tenantId,
                Reference = "DEF-E10",
                SiteId = Site_Tour_Defense,
                Type = UnitType.Office,
                Status = UnitStatus.Occupied,
                Floor = "10ème",
                DoorNumber = "Plateau 10",
                SurfaceArea = 1200m,
                Rooms = 1,
                OwnershipSharesCount = 0,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
        });

        // Ajouter quelques parkings et caves
        units.AddRange(new[]
        {
            new Unit
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Reference = "CHAP-P01",
                SiteId = Site_Residence_Chapelle,
                Type = UnitType.Parking,
                Status = UnitStatus.Occupied,
                Floor = "-1",
                DoorNumber = "P01",
                SurfaceArea = 12m,
                OwnershipSharesCount = 50,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new Unit
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Reference = "CHAP-C01",
                SiteId = Site_Residence_Chapelle,
                Type = UnitType.Storage,
                Status = UnitStatus.Occupied,
                Floor = "-1",
                DoorNumber = "Cave 01",
                SurfaceArea = 8m,
                OwnershipSharesCount = 30,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
        });

        await context.Units.AddRangeAsync(units);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ Units créées");
    }

    #endregion

    #region Occupants

    private static async Task SeedOccupantsAsync(AppDbContext context, Guid tenantId)
    {
        if (await context.Occupants.AnyAsync())
            return;

        var occupants = new List<Occupant>
        {
            // Occupants Résidence La Chapelle
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                UnitId = Unit_Chapelle_A01,
                Type = OccupantType.Owner,
                PersonType = PersonType.Individual,
                FirstName = "Marie",
                LastName = "DUPONT",
                Email = "marie.dupont@email.fr",
                Phone = "0145678901",
                Mobile = "0612345678",
                MoveInDate = new DateTime(2015, 6, 1),
                HasPortalAccess = true,
                PreferredContactMethod = PreferredContactMethod.Email,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                UnitId = Unit_Chapelle_A02,
                Type = OccupantType.Tenant,
                PersonType = PersonType.Individual,
                FirstName = "Pierre",
                LastName = "MARTIN",
                Email = "p.martin@gmail.com",
                Mobile = "0698765432",
                MoveInDate = new DateTime(2022, 9, 1),
                HasPortalAccess = true,
                PreferredContactMethod = PreferredContactMethod.SMS,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // Occupants Ensemble Belleville (locataires HLM)
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                UnitId = Unit_Belleville_101,
                Type = OccupantType.Tenant,
                PersonType = PersonType.Individual,
                FirstName = "Fatou",
                LastName = "DIOP",
                Email = "fatou.diop@orange.fr",
                Phone = "0149876543",
                Mobile = "0678901234",
                MoveInDate = new DateTime(2018, 3, 15),
                HasPortalAccess = false,
                PreferredContactMethod = PreferredContactMethod.Phone,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                UnitId = Unit_Belleville_102,
                Type = OccupantType.Tenant,
                PersonType = PersonType.Individual,
                FirstName = "Ahmed",
                LastName = "BENZEMA",
                Email = "ahmed.b@free.fr",
                Mobile = "0656789012",
                MoveInDate = new DateTime(2020, 1, 1),
                HasPortalAccess = true,
                PreferredContactMethod = PreferredContactMethod.WhatsApp,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // Occupants Tour Défense (entreprises)
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                UnitId = Unit_Defense_Etage5,
                Type = OccupantType.Tenant,
                PersonType = PersonType.Company,
                FirstName = "Services",
                LastName = "Généraux",
                CompanyName = "KPMG France",
                Email = "services.generaux@kpmg.fr",
                Phone = "0155687500",
                MoveInDate = new DateTime(2019, 4, 1),
                HasPortalAccess = true,
                PreferredContactMethod = PreferredContactMethod.Email,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                UnitId = Unit_Defense_Etage10,
                Type = OccupantType.Tenant,
                PersonType = PersonType.Company,
                FirstName = "Facility",
                LastName = "Management",
                CompanyName = "Capgemini",
                Email = "facility.fr@capgemini.com",
                Phone = "0149003000",
                MoveInDate = new DateTime(2017, 9, 1),
                HasPortalAccess = true,
                PreferredContactMethod = PreferredContactMethod.Email,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
        };

        await context.Occupants.AddRangeAsync(occupants);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ Occupants créés");
    }

    #endregion

    #region SiteContacts

    private static async Task SeedSiteContactsAsync(AppDbContext context, Guid tenantId)
    {
        if (await context.Set<SiteContact>().AnyAsync())
            return;

        var contacts = new List<SiteContact>
        {
            // Contacts Résidence La Chapelle
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                SiteId = Site_Residence_Chapelle,
                SiteContactCategoryId = ReferenceDataSeeder.ContactType_Gardien,
                Firstname = "Maria",
                Lastname = "SILVA",
                Email = "gardienne.chapelle@foncia.fr",
                Phone = "0140123470",
                CellPhone = "0678901236",
                IsPrimary = true,
                AvailabilityHours = "7h-12h et 14h-19h du lundi au samedi",
                PreferredContactMethod = PreferredContactMethod.Phone,
                Note = "Gardienne très réactive - Possède toutes les clés",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                SiteId = Site_Residence_Chapelle,
                SiteContactCategoryId = ReferenceDataSeeder.ContactType_Technique,
                Firstname = "Jean-Claude",
                Lastname = "RENARD",
                Email = "president.cs@chapelle.fr",
                Phone = "0145678902",
                IsPrimary = false,
                Note = "Président du conseil syndical - À contacter pour travaux importants",
                PreferredContactMethod = PreferredContactMethod.Email,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // Contacts Ensemble Belleville
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                SiteId = Site_Ensemble_Belleville,
                SiteContactCategoryId = ReferenceDataSeeder.ContactType_Gardien,
                Firstname = "Moussa",
                Lastname = "KONE",
                Email = "gardien.belleville@parishabitat.fr",
                Phone = "0149876544",
                CellPhone = "0698765434",
                IsPrimary = true,
                AvailabilityHours = "7h-19h du lundi au vendredi, 8h-12h samedi",
                PreferredContactMethod = PreferredContactMethod.Phone,
                Note = "Gardien référent pour les 4 bâtiments",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                SiteId = Site_Ensemble_Belleville,
                SiteContactCategoryId = ReferenceDataSeeder.ContactType_Urgence,
                Firstname = "Urgences",
                Lastname = "Paris Habitat",
                Email = "urgences@parishabitat.fr",
                Phone = "0800123456",
                IsPrimary = false,
                AvailabilityHours = "24h/24 - 7j/7",
                PreferredContactMethod = PreferredContactMethod.Phone,
                Note = "Numéro vert urgences - Astreinte permanente",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // Contacts Tour Défense
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                SiteId = Site_Tour_Defense,
                SiteContactCategoryId = ReferenceDataSeeder.ContactType_Technique,
                Firstname = "Sébastien",
                Lastname = "ROCHE",
                Email = "s.roche@defense-office.fr",
                Phone = "0147890236",
                CellPhone = "0612345679",
                IsPrimary = true,
                AvailabilityHours = "8h-18h du lundi au vendredi",
                PreferredContactMethod = PreferredContactMethod.Email,
                Note = "Responsable technique du bâtiment - Toutes les autorisations",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                SiteId = Site_Tour_Defense,
                SiteContactCategoryId = ReferenceDataSeeder.ContactType_Urgence,
                Firstname = "PC",
                Lastname = "Sécurité",
                Email = "pc.securite@defense-office.fr",
                Phone = "0147890200",
                IsPrimary = false,
                AvailabilityHours = "24h/24",
                PreferredContactMethod = PreferredContactMethod.Phone,
                Note = "PC Sécurité - Contact obligatoire avant toute intervention",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // Contact Centre Commercial Beaugrenelle
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                SiteId = Site_Beaugrenelle,
                SiteContactCategoryId = ReferenceDataSeeder.ContactType_Technique,
                Firstname = "Régis",
                Lastname = "CHAMPION",
                Email = "r.champion@beaugrenelle.com",
                Phone = "0145789020",
                CellPhone = "0678901237",
                IsPrimary = true,
                AvailabilityHours = "9h-19h tous les jours",
                PreferredContactMethod = PreferredContactMethod.Phone,
                Note = "Directeur technique du centre commercial",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
        };

        await context.Set<SiteContact>().AddRangeAsync(contacts);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ SiteContacts créés");
    }

    #endregion

    #region SiteKeepers

    private static async Task SeedSiteKeepersAsync(AppDbContext context, Guid tenantId)
    {
        if (await context.Set<SiteKeeper>().AnyAsync())
            return;

        var keepers = new List<SiteKeeper>
        {
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                SiteId = Site_Residence_Chapelle,
                Firstname = "Maria",
                Lastname = "SILVA",
                Email = "gardienne.chapelle@foncia.fr",
                Phone = "0140123470",
                CellPhone = "0678901236",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                SiteId = Site_Residence_Italie,
                Firstname = "Robert",
                Lastname = "CHEN",
                Email = "gardien.italie@foncia.fr",
                Phone = "0145678903",
                CellPhone = "0612345680",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                SiteId = Site_Ensemble_Belleville,
                Firstname = "Moussa",
                Lastname = "KONE",
                Email = "gardien.belleville@parishabitat.fr",
                Phone = "0149876544",
                CellPhone = "0698765434",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                SiteId = Site_Tour_Flandre,
                Firstname = "Amadou",
                Lastname = "TOURE",
                Email = "gardien.flandre@parishabitat.fr",
                Phone = "0149876545",
                CellPhone = "0698765435",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                SiteId = Site_ASL_Jardins,
                Firstname = "Philippe",
                Lastname = "MOREAU",
                Email = "concierge@jardins-neuilly.fr",
                Phone = "0146789012",
                CellPhone = "0656789013",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
        };

        await context.Set<SiteKeeper>().AddRangeAsync(keepers);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ SiteKeepers créés");
    }

    #endregion
}
