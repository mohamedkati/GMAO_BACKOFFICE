using GMAO.Domain.Entities;
using GMAO.Domain.Entities.siteAggregate;
using GMAO.Domain.Enums;
using GMAO.Domain.ValueObjects;
using GMAO.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GMAO.DATA.Seed.SeedData;

/// <summary>
/// Seeder pour la gestion des équipements et actifs
/// Catégories, équipements, plans de maintenance et garanties
/// </summary>
public static class AssetManagementSeeder
{
    #region AssetCategory IDs

    // Catégories principales
    public static readonly Guid Cat_CVC = Guid.Parse("f1000001-0000-0000-0000-000000000001");
    public static readonly Guid Cat_Plomberie = Guid.Parse("f1000001-0000-0000-0000-000000000002");
    public static readonly Guid Cat_Electricite = Guid.Parse("f1000001-0000-0000-0000-000000000003");
    public static readonly Guid Cat_Ascenseurs = Guid.Parse("f1000001-0000-0000-0000-000000000004");
    public static readonly Guid Cat_Securite = Guid.Parse("f1000001-0000-0000-0000-000000000005");
    public static readonly Guid Cat_PartiesCommunes = Guid.Parse("f1000001-0000-0000-0000-000000000006");
    public static readonly Guid Cat_Toiture = Guid.Parse("f1000001-0000-0000-0000-000000000007");

    // Sous-catégories CVC
    public static readonly Guid Cat_CVC_Chauffage = Guid.Parse("f1000001-0000-0000-0000-000000000011");
    public static readonly Guid Cat_CVC_Climatisation = Guid.Parse("f1000001-0000-0000-0000-000000000012");
    public static readonly Guid Cat_CVC_Ventilation = Guid.Parse("f1000001-0000-0000-0000-000000000013");

    // Sous-catégories Plomberie
    public static readonly Guid Cat_Plomb_Production = Guid.Parse("f1000001-0000-0000-0000-000000000021");
    public static readonly Guid Cat_Plomb_Distribution = Guid.Parse("f1000001-0000-0000-0000-000000000022");
    public static readonly Guid Cat_Plomb_Evacuation = Guid.Parse("f1000001-0000-0000-0000-000000000023");

    // Sous-catégories Électricité
    public static readonly Guid Cat_Elec_Distribution = Guid.Parse("f1000001-0000-0000-0000-000000000031");
    public static readonly Guid Cat_Elec_Eclairage = Guid.Parse("f1000001-0000-0000-0000-000000000032");

    // Sous-catégories Sécurité
    public static readonly Guid Cat_Secu_Incendie = Guid.Parse("f1000001-0000-0000-0000-000000000041");
    public static readonly Guid Cat_Secu_Controle = Guid.Parse("f1000001-0000-0000-0000-000000000042");

    #endregion

    #region Asset IDs (exemples représentatifs)

    // Équipements Résidence La Chapelle
    public static readonly Guid Asset_Chapelle_Chaufferie = Guid.Parse("f2000001-0000-0000-0000-000000000001");
    public static readonly Guid Asset_Chapelle_VMC = Guid.Parse("f2000001-0000-0000-0000-000000000002");
    public static readonly Guid Asset_Chapelle_TGBT = Guid.Parse("f2000001-0000-0000-0000-000000000003");
    public static readonly Guid Asset_Chapelle_Ascenseur = Guid.Parse("f2000001-0000-0000-0000-000000000004");
    public static readonly Guid Asset_Chapelle_SSI = Guid.Parse("f2000001-0000-0000-0000-000000000005");

    // Équipements Ensemble Belleville
    public static readonly Guid Asset_Belleville_Chaufferie_A = Guid.Parse("f2000001-0000-0000-0000-000000000006");
    public static readonly Guid Asset_Belleville_Chaufferie_B = Guid.Parse("f2000001-0000-0000-0000-000000000007");
    public static readonly Guid Asset_Belleville_Surpresseur = Guid.Parse("f2000001-0000-0000-0000-000000000008");

    // Équipements Tour Défense
    public static readonly Guid Asset_Defense_CTA_Principal = Guid.Parse("f2000001-0000-0000-0000-000000000009");
    public static readonly Guid Asset_Defense_GroupeFroid = Guid.Parse("f2000001-0000-0000-0000-000000000010");
    public static readonly Guid Asset_Defense_GE = Guid.Parse("f2000001-0000-0000-0000-000000000011");
    public static readonly Guid Asset_Defense_Ascenseur_1 = Guid.Parse("f2000001-0000-0000-0000-000000000012");
    public static readonly Guid Asset_Defense_SSI = Guid.Parse("f2000001-0000-0000-0000-000000000013");

    #endregion

    public static async Task SeedAsync(AppDbContext context, Guid tenantId)
    {
        await SeedAssetCategoriesAsync(context, tenantId);
        await SeedAssetsAsync(context, tenantId);
        await SeedMaintenancePlansAsync(context, tenantId);
        await SeedWarrantiesAsync(context);
    }

    #region AssetCategories

    private static async Task SeedAssetCategoriesAsync(AppDbContext context, Guid tenantId)
    {
        if (await context.AssetCategories.AnyAsync())
            return;

        var categories = new List<AssetCategory>
        {
            // ══════════════════════════════════════════════════════════════════
            // CATÉGORIES PRINCIPALES
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Cat_CVC,
                TenantId = tenantId,
                Name = "CVC - Chauffage Ventilation Climatisation",
                Description = "Équipements de chauffage, ventilation et climatisation",
                Code = "CVC",
                ParentCategoryId = null,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Cat_Plomberie,
                TenantId = tenantId,
                Name = "Plomberie - Sanitaires",
                Description = "Équipements de plomberie, production et distribution d'eau",
                Code = "PLB",
                ParentCategoryId = null,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Cat_Electricite,
                TenantId = tenantId,
                Name = "Électricité",
                Description = "Équipements électriques, tableaux, distribution",
                Code = "ELEC",
                ParentCategoryId = null,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Cat_Ascenseurs,
                TenantId = tenantId,
                Name = "Ascenseurs - Monte-charges",
                Description = "Équipements de transport vertical",
                Code = "ASC",
                ParentCategoryId = null,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Cat_Securite,
                TenantId = tenantId,
                Name = "Sécurité",
                Description = "Équipements de sécurité incendie et contrôle d'accès",
                Code = "SECU",
                ParentCategoryId = null,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Cat_PartiesCommunes,
                TenantId = tenantId,
                Name = "Parties Communes",
                Description = "Équipements des parties communes (portes, interphones...)",
                Code = "PC",
                ParentCategoryId = null,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Cat_Toiture,
                TenantId = tenantId,
                Name = "Toiture - Étanchéité",
                Description = "Toiture, terrasse, étanchéité, zinguerie",
                Code = "TOIT",
                ParentCategoryId = null,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // ══════════════════════════════════════════════════════════════════
            // SOUS-CATÉGORIES CVC
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Cat_CVC_Chauffage,
                TenantId = tenantId,
                Name = "Chauffage",
                Description = "Chaudières, radiateurs, planchers chauffants",
                Code = "CVC-CH",
                ParentCategoryId = Cat_CVC,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Cat_CVC_Climatisation,
                TenantId = tenantId,
                Name = "Climatisation",
                Description = "Groupes froids, climatiseurs, splits",
                Code = "CVC-CL",
                ParentCategoryId = Cat_CVC,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Cat_CVC_Ventilation,
                TenantId = tenantId,
                Name = "Ventilation",
                Description = "VMC, CTA, extracteurs, gaines",
                Code = "CVC-VE",
                ParentCategoryId = Cat_CVC,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // ══════════════════════════════════════════════════════════════════
            // SOUS-CATÉGORIES PLOMBERIE
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Cat_Plomb_Production,
                TenantId = tenantId,
                Name = "Production ECS",
                Description = "Production d'eau chaude sanitaire",
                Code = "PLB-PR",
                ParentCategoryId = Cat_Plomberie,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Cat_Plomb_Distribution,
                TenantId = tenantId,
                Name = "Distribution eau",
                Description = "Réseaux, surpresseurs, pompes",
                Code = "PLB-DI",
                ParentCategoryId = Cat_Plomberie,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Cat_Plomb_Evacuation,
                TenantId = tenantId,
                Name = "Évacuations",
                Description = "Colonnes, regards, pompes de relevage",
                Code = "PLB-EV",
                ParentCategoryId = Cat_Plomberie,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // ══════════════════════════════════════════════════════════════════
            // SOUS-CATÉGORIES ÉLECTRICITÉ
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Cat_Elec_Distribution,
                TenantId = tenantId,
                Name = "Distribution électrique",
                Description = "TGBT, tableaux divisionnaires, armoires",
                Code = "ELEC-DI",
                ParentCategoryId = Cat_Electricite,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Cat_Elec_Eclairage,
                TenantId = tenantId,
                Name = "Éclairage",
                Description = "Éclairage parties communes, parking, extérieur",
                Code = "ELEC-EC",
                ParentCategoryId = Cat_Electricite,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // ══════════════════════════════════════════════════════════════════
            // SOUS-CATÉGORIES SÉCURITÉ
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Cat_Secu_Incendie,
                TenantId = tenantId,
                Name = "Sécurité Incendie",
                Description = "SSI, détection, désenfumage, extincteurs",
                Code = "SECU-INC",
                ParentCategoryId = Cat_Securite,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Cat_Secu_Controle,
                TenantId = tenantId,
                Name = "Contrôle d'accès",
                Description = "Vigik, interphones, vidéosurveillance",
                Code = "SECU-ACC",
                ParentCategoryId = Cat_Securite,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
        };

        await context.AssetCategories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ AssetCategories créées");
    }

    #endregion

    #region Assets

    private static async Task SeedAssetsAsync(AppDbContext context, Guid tenantId)
    {
        if (await context.Assets.AnyAsync())
            return;

        var assets = new List<Asset>
        {
            // ══════════════════════════════════════════════════════════════════
            // ÉQUIPEMENTS RÉSIDENCE LA CHAPELLE
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Asset_Chapelle_Chaufferie,
                TenantId = tenantId,
                Reference = "CHAP-CVC-001",
                Name = "Chaufferie collective gaz",
                AssetCategoryId = Cat_CVC_Chauffage,
                SiteId = PropertyManagementSeeder.Site_Residence_Chapelle,
                UnitId = null,
                IsCommonAsset = true,
                Manufacturer = "De Dietrich",
                Model = "C 330-200 ECO",
                SerialNumber = "DD2018-123456",
                InstallationDate = new DateTime(2018, 10, 15),
                Status = AssetStatus.Active,
                CriticalityLevel = CriticalityLevel.Critical,
                HealthStatus = AssetHealthStatus.Good,
                Location = new AssetLocation
                {
                    LocationDescription = "Sous-sol, local chaufferie",
                    XPosition = null,
                    YPosition = null
                },
                ReliabilityMetrics = new AssetReliabilityMetrics
                {
                    TotalFailures = 3,
                    TotalMaintenanceHours = 24,
                    MTBF = 2880,
                    MTTR = 4,
                    AvailabilityPercent = 99.2,
                    FailuresPerYear = 0.5,
                    LastMaintenanceDate = DateTime.UtcNow.AddDays(-45)
                },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Asset_Chapelle_VMC,
                TenantId = tenantId,
                Reference = "CHAP-CVC-002",
                Name = "VMC collective",
                AssetCategoryId = Cat_CVC_Ventilation,
                SiteId = PropertyManagementSeeder.Site_Residence_Chapelle,
                IsCommonAsset = true,
                Manufacturer = "Atlantic",
                Model = "Duolix Max",
                SerialNumber = "ATL2020-789012",
                InstallationDate = new DateTime(2020, 3, 20),
                Status = AssetStatus.Active,
                CriticalityLevel = CriticalityLevel.High,
                HealthStatus = AssetHealthStatus.Excellent,
                Location = new AssetLocation { LocationDescription = "Toiture terrasse" },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Asset_Chapelle_TGBT,
                TenantId = tenantId,
                Reference = "CHAP-ELEC-001",
                Name = "TGBT Parties Communes",
                AssetCategoryId = Cat_Elec_Distribution,
                SiteId = PropertyManagementSeeder.Site_Residence_Chapelle,
                IsCommonAsset = true,
                Manufacturer = "Schneider Electric",
                Model = "Prisma Plus P",
                SerialNumber = "SE2015-456789",
                InstallationDate = new DateTime(2015, 6, 10),
                Status = AssetStatus.Active,
                CriticalityLevel = CriticalityLevel.Critical,
                HealthStatus = AssetHealthStatus.Good,
                Location = new AssetLocation { LocationDescription = "Local technique RDC" },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Asset_Chapelle_Ascenseur,
                TenantId = tenantId,
                Reference = "CHAP-ASC-001",
                Name = "Ascenseur principal",
                AssetCategoryId = Cat_Ascenseurs,
                SiteId = PropertyManagementSeeder.Site_Residence_Chapelle,
                IsCommonAsset = true,
                Manufacturer = "Otis",
                Model = "GeN2 Comfort",
                SerialNumber = "OTIS2010-112233",
                InstallationDate = new DateTime(2010, 9, 1),
                Status = AssetStatus.Active,
                CriticalityLevel = CriticalityLevel.Critical,
                HealthStatus = AssetHealthStatus.Fair,
                Location = new AssetLocation { LocationDescription = "Cage d'escalier A" },
                ReliabilityMetrics = new AssetReliabilityMetrics
                {
                    TotalFailures = 12,
                    TotalMaintenanceHours = 48,
                    MTBF = 730,
                    MTTR = 2,
                    AvailabilityPercent = 97.5,
                    FailuresPerYear = 1.0,
                    LastFailureDate = DateTime.UtcNow.AddDays(-30),
                    LastMaintenanceDate = DateTime.UtcNow.AddDays(-15)
                },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Asset_Chapelle_SSI,
                TenantId = tenantId,
                Reference = "CHAP-SECU-001",
                Name = "Système de Sécurité Incendie",
                AssetCategoryId = Cat_Secu_Incendie,
                SiteId = PropertyManagementSeeder.Site_Residence_Chapelle,
                IsCommonAsset = true,
                Manufacturer = "Siemens",
                Model = "Cerberus PRO",
                SerialNumber = "SIEM2019-334455",
                InstallationDate = new DateTime(2019, 1, 15),
                Status = AssetStatus.Active,
                CriticalityLevel = CriticalityLevel.Critical,
                HealthStatus = AssetHealthStatus.Good,
                Location = new AssetLocation { LocationDescription = "Loge gardien + détecteurs tous niveaux" },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // ══════════════════════════════════════════════════════════════════
            // ÉQUIPEMENTS ENSEMBLE BELLEVILLE
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Asset_Belleville_Chaufferie_A,
                TenantId = tenantId,
                Reference = "BELL-CVC-001",
                Name = "Chaufferie Bâtiment A-B",
                AssetCategoryId = Cat_CVC_Chauffage,
                SiteId = PropertyManagementSeeder.Site_Ensemble_Belleville,
                IsCommonAsset = true,
                Manufacturer = "Viessmann",
                Model = "Vitocrossal 300",
                SerialNumber = "VIES2021-667788",
                InstallationDate = new DateTime(2021, 11, 1),
                Status = AssetStatus.Active,
                CriticalityLevel = CriticalityLevel.Critical,
                HealthStatus = AssetHealthStatus.Excellent,
                Location = new AssetLocation { LocationDescription = "Sous-sol Bâtiment A" },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Asset_Belleville_Chaufferie_B,
                TenantId = tenantId,
                Reference = "BELL-CVC-002",
                Name = "Chaufferie Bâtiment C-D",
                AssetCategoryId = Cat_CVC_Chauffage,
                SiteId = PropertyManagementSeeder.Site_Ensemble_Belleville,
                IsCommonAsset = true,
                Manufacturer = "Viessmann",
                Model = "Vitocrossal 300",
                SerialNumber = "VIES2021-667789",
                InstallationDate = new DateTime(2021, 11, 1),
                Status = AssetStatus.Active,
                CriticalityLevel = CriticalityLevel.Critical,
                HealthStatus = AssetHealthStatus.Excellent,
                Location = new AssetLocation { LocationDescription = "Sous-sol Bâtiment C" },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Asset_Belleville_Surpresseur,
                TenantId = tenantId,
                Reference = "BELL-PLB-001",
                Name = "Surpresseur eau froide",
                AssetCategoryId = Cat_Plomb_Distribution,
                SiteId = PropertyManagementSeeder.Site_Ensemble_Belleville,
                IsCommonAsset = true,
                Manufacturer = "Grundfos",
                Model = "Hydro MPC-E",
                SerialNumber = "GRF2019-445566",
                InstallationDate = new DateTime(2019, 5, 15),
                Status = AssetStatus.Active,
                CriticalityLevel = CriticalityLevel.High,
                HealthStatus = AssetHealthStatus.Good,
                Location = new AssetLocation { LocationDescription = "Local technique Bâtiment A" },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // ══════════════════════════════════════════════════════════════════
            // ÉQUIPEMENTS TOUR DÉFENSE
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Asset_Defense_CTA_Principal,
                TenantId = tenantId,
                Reference = "DEF-CVC-001",
                Name = "CTA Principal",
                AssetCategoryId = Cat_CVC_Ventilation,
                SiteId = PropertyManagementSeeder.Site_Tour_Defense,
                IsCommonAsset = true,
                Manufacturer = "Carrier",
                Model = "39HQ",
                SerialNumber = "CAR2008-112233",
                InstallationDate = new DateTime(2008, 6, 1),
                Status = AssetStatus.Active,
                CriticalityLevel = CriticalityLevel.Critical,
                HealthStatus = AssetHealthStatus.Fair,
                Location = new AssetLocation { LocationDescription = "Niveau technique N-2" },
                ReliabilityMetrics = new AssetReliabilityMetrics
                {
                    TotalFailures = 8,
                    TotalMaintenanceHours = 120,
                    MTBF = 1095,
                    MTTR = 8,
                    AvailabilityPercent = 98.5,
                    FailuresPerYear = 0.5,
                    LastMaintenanceDate = DateTime.UtcNow.AddDays(-7)
                },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Asset_Defense_GroupeFroid,
                TenantId = tenantId,
                Reference = "DEF-CVC-002",
                Name = "Groupe Froid Principal",
                AssetCategoryId = Cat_CVC_Climatisation,
                SiteId = PropertyManagementSeeder.Site_Tour_Defense,
                IsCommonAsset = true,
                Manufacturer = "Trane",
                Model = "RTAC 500",
                SerialNumber = "TRA2008-445566",
                InstallationDate = new DateTime(2008, 6, 1),
                Status = AssetStatus.Active,
                CriticalityLevel = CriticalityLevel.Critical,
                HealthStatus = AssetHealthStatus.Good,
                Location = new AssetLocation { LocationDescription = "Toiture technique" },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Asset_Defense_GE,
                TenantId = tenantId,
                Reference = "DEF-ELEC-001",
                Name = "Groupe Électrogène",
                AssetCategoryId = Cat_Elec_Distribution,
                SiteId = PropertyManagementSeeder.Site_Tour_Defense,
                IsCommonAsset = true,
                Manufacturer = "Caterpillar",
                Model = "C32 ACERT",
                SerialNumber = "CAT2008-778899",
                InstallationDate = new DateTime(2008, 6, 1),
                Status = AssetStatus.Active,
                CriticalityLevel = CriticalityLevel.Critical,
                HealthStatus = AssetHealthStatus.Good,
                Location = new AssetLocation { LocationDescription = "Niveau technique N-3" },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Asset_Defense_Ascenseur_1,
                TenantId = tenantId,
                Reference = "DEF-ASC-001",
                Name = "Ascenseur Grande Vitesse 1",
                AssetCategoryId = Cat_Ascenseurs,
                SiteId = PropertyManagementSeeder.Site_Tour_Defense,
                IsCommonAsset = true,
                Manufacturer = "Kone",
                Model = "MonoSpace 700",
                SerialNumber = "KONE2008-001122",
                InstallationDate = new DateTime(2008, 6, 1),
                Status = AssetStatus.Active,
                CriticalityLevel = CriticalityLevel.Critical,
                HealthStatus = AssetHealthStatus.Good,
                Location = new AssetLocation { LocationDescription = "Batterie ascenseurs Hall Nord" },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Asset_Defense_SSI,
                TenantId = tenantId,
                Reference = "DEF-SECU-001",
                Name = "SSI IGH Catégorie A",
                AssetCategoryId = Cat_Secu_Incendie,
                SiteId = PropertyManagementSeeder.Site_Tour_Defense,
                IsCommonAsset = true,
                Manufacturer = "Honeywell",
                Model = "Esser by Honeywell",
                SerialNumber = "HON2008-334455",
                InstallationDate = new DateTime(2008, 6, 1),
                Status = AssetStatus.Active,
                CriticalityLevel = CriticalityLevel.Critical,
                HealthStatus = AssetHealthStatus.Excellent,
                Location = new AssetLocation { LocationDescription = "PC Sécurité + Détecteurs tous niveaux" },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
        };

        await context.Assets.AddRangeAsync(assets);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ Assets créés");
    }

    #endregion

    #region MaintenancePlans

    private static async Task SeedMaintenancePlansAsync(AppDbContext context, Guid tenantId)
    {
        if (await context.MaintenancePlans.AnyAsync())
            return;

        var plans = new List<MaintenancePlan>
        {
            // ══════════════════════════════════════════════════════════════════
            // PLANS MAINTENANCE CHAUFFERIE LA CHAPELLE
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = "Entretien annuel chaudière",
                AssetId = Asset_Chapelle_Chaufferie,
                Frequency = MaintenanceFrequency.Annual,
                LastExecutionDate = new DateTime(2024, 9, 15),
                NextExecutionDate = new DateTime(2025, 9, 15),
                IsActive = true,
                AlertDaysBefore = 30,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty,
                Tasks = new List<MaintenanceTask>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 1,
                        Description = "Vérification et nettoyage du brûleur",
                        EstimatedDurationMinutes = 60,
                        RequiredSkillId = ReferenceDataSeeder.Skill_CVC
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 2,
                        Description = "Contrôle et réglage de la combustion",
                        EstimatedDurationMinutes = 30,
                        RequiredSkillId = ReferenceDataSeeder.Skill_CVC
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 3,
                        Description = "Vérification du corps de chauffe et ramonage",
                        EstimatedDurationMinutes = 45,
                        RequiredSkillId = ReferenceDataSeeder.Skill_CVC
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 4,
                        Description = "Contrôle des dispositifs de sécurité",
                        EstimatedDurationMinutes = 30,
                        RequiredSkillId = ReferenceDataSeeder.Skill_CVC
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 5,
                        Description = "Analyse des fumées et attestation d'entretien",
                        EstimatedDurationMinutes = 30,
                        RequiredSkillId = ReferenceDataSeeder.Skill_CVC
                    },
                }
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = "Contrôle mensuel chaufferie",
                AssetId = Asset_Chapelle_Chaufferie,
                Frequency = MaintenanceFrequency.Monthly,
                LastExecutionDate = DateTime.UtcNow.AddDays(-15),
                NextExecutionDate = DateTime.UtcNow.AddDays(15),
                IsActive = true,
                AlertDaysBefore = 7,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty,
                Tasks = new List<MaintenanceTask>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 1,
                        Description = "Relevé des compteurs et températures",
                        EstimatedDurationMinutes = 15,
                        RequiredSkillId = ReferenceDataSeeder.Skill_CVC
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 2,
                        Description = "Contrôle visuel général et propreté local",
                        EstimatedDurationMinutes = 15,
                        RequiredSkillId = ReferenceDataSeeder.Skill_CVC
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 3,
                        Description = "Vérification fonctionnement pompes",
                        EstimatedDurationMinutes = 15,
                        RequiredSkillId = ReferenceDataSeeder.Skill_CVC
                    },
                }
            },

            // ══════════════════════════════════════════════════════════════════
            // PLAN MAINTENANCE ASCENSEUR LA CHAPELLE
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = "Visite périodique ascenseur (6 semaines)",
                AssetId = Asset_Chapelle_Ascenseur,
                Frequency = MaintenanceFrequency.Custom,
                LastExecutionDate = DateTime.UtcNow.AddDays(-20),
                NextExecutionDate = DateTime.UtcNow.AddDays(22),
                IsActive = true,
                AlertDaysBefore = 7,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty,
                Tasks = new List<MaintenanceTask>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 1,
                        Description = "Essais de fonctionnement et niveaux",
                        EstimatedDurationMinutes = 20,
                        RequiredSkillId = ReferenceDataSeeder.Skill_Ascenseur
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 2,
                        Description = "Vérification des organes de sécurité",
                        EstimatedDurationMinutes = 30,
                        RequiredSkillId = ReferenceDataSeeder.Skill_Ascenseur
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 3,
                        Description = "Graissage et nettoyage guides",
                        EstimatedDurationMinutes = 20,
                        RequiredSkillId = ReferenceDataSeeder.Skill_Ascenseur
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 4,
                        Description = "Contrôle usure câbles et poulies",
                        EstimatedDurationMinutes = 20,
                        RequiredSkillId = ReferenceDataSeeder.Skill_Ascenseur
                    },
                }
            },

            // ══════════════════════════════════════════════════════════════════
            // PLAN MAINTENANCE SSI LA CHAPELLE
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = "Vérification trimestrielle SSI",
                AssetId = Asset_Chapelle_SSI,
                Frequency = MaintenanceFrequency.Quarterly,
                LastExecutionDate = DateTime.UtcNow.AddMonths(-2),
                NextExecutionDate = DateTime.UtcNow.AddMonths(1),
                IsActive = true,
                AlertDaysBefore = 14,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty,
                Tasks = new List<MaintenanceTask>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 1,
                        Description = "Test fonctionnel de la centrale",
                        EstimatedDurationMinutes = 30,
                        RequiredSkillId = ReferenceDataSeeder.Skill_Incendie
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 2,
                        Description = "Test des détecteurs (échantillonnage)",
                        EstimatedDurationMinutes = 60,
                        RequiredSkillId = ReferenceDataSeeder.Skill_Incendie
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 3,
                        Description = "Vérification des asservissements",
                        EstimatedDurationMinutes = 30,
                        RequiredSkillId = ReferenceDataSeeder.Skill_Incendie
                    },
                }
            },

            // ══════════════════════════════════════════════════════════════════
            // PLANS MAINTENANCE TOUR DÉFENSE
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = "Maintenance mensuelle CTA",
                AssetId = Asset_Defense_CTA_Principal,
                Frequency = MaintenanceFrequency.Monthly,
                LastExecutionDate = DateTime.UtcNow.AddDays(-7),
                NextExecutionDate = DateTime.UtcNow.AddDays(23),
                IsActive = true,
                AlertDaysBefore = 7,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty,
                Tasks = new List<MaintenanceTask>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 1,
                        Description = "Contrôle des filtres et remplacement si nécessaire",
                        EstimatedDurationMinutes = 45,
                        RequiredSkillId = ReferenceDataSeeder.Skill_CVC
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 2,
                        Description = "Vérification tension courroies",
                        EstimatedDurationMinutes = 20,
                        RequiredSkillId = ReferenceDataSeeder.Skill_CVC
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 3,
                        Description = "Relevé des paramètres de fonctionnement",
                        EstimatedDurationMinutes = 15,
                        RequiredSkillId = ReferenceDataSeeder.Skill_CVC
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 4,
                        Description = "Nettoyage batteries et bac de condensats",
                        EstimatedDurationMinutes = 40,
                        RequiredSkillId = ReferenceDataSeeder.Skill_CVC
                    },
                }
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = "Test mensuel Groupe Électrogène",
                AssetId = Asset_Defense_GE,
                Frequency = MaintenanceFrequency.Monthly,
                LastExecutionDate = DateTime.UtcNow.AddDays(-10),
                NextExecutionDate = DateTime.UtcNow.AddDays(20),
                IsActive = true,
                AlertDaysBefore = 7,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty,
                Tasks = new List<MaintenanceTask>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 1,
                        Description = "Démarrage et test en charge 30 minutes",
                        EstimatedDurationMinutes = 45,
                        RequiredSkillId = ReferenceDataSeeder.Skill_Electricite
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 2,
                        Description = "Vérification niveaux (huile, liquide refroidissement, carburant)",
                        EstimatedDurationMinutes = 15,
                        RequiredSkillId = ReferenceDataSeeder.Skill_Electricite
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        TaskOrder = 3,
                        Description = "Contrôle état batteries de démarrage",
                        EstimatedDurationMinutes = 15,
                        RequiredSkillId = ReferenceDataSeeder.Skill_Electricite
                    },
                }
            },
        };

        await context.MaintenancePlans.AddRangeAsync(plans);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ MaintenancePlans et MaintenanceTasks créés");
    }

    #endregion

    #region Warranties

    private static async Task SeedWarrantiesAsync(AppDbContext context)
    {
        if (await context.Set<Warranty>().AnyAsync())
            return;

        var warranties = new List<Warranty>
        {
            // Garantie chaudière La Chapelle
            new()
            {
                Id = Guid.NewGuid(),
                AssetId = Asset_Chapelle_Chaufferie,
                Type = WarrantyType.Manufacturer,
                ProviderName = "De Dietrich",
                WarrantyNumber = "GAR-DD-2018-123456",
                StartDate = new DateTime(2018, 10, 15),
                EndDate = new DateTime(2028, 10, 15),
                CoveredItems = "Corps de chauffe, brûleur, régulation",
                Exclusions = "Consommables, pièces d'usure normale",
                ContactPhone = "0825882588",
                ContactEmail = "sav@dedietrich.com",
                ClaimsCount = 1,
                ClaimedAmount = 850m,
                SendExpirationAlert = true,
                AlertDaysBefore = 90
            },
            
            // Garantie chaudières Belleville
            new()
            {
                Id = Guid.NewGuid(),
                AssetId = Asset_Belleville_Chaufferie_A,
                Type = WarrantyType.Manufacturer,
                ProviderName = "Viessmann",
                WarrantyNumber = "VIES-GAR-2021-001",
                StartDate = new DateTime(2021, 11, 1),
                EndDate = new DateTime(2031, 11, 1),
                CoveredItems = "Échangeur inox, brûleur MatriX, régulation Vitotronic",
                Exclusions = "Main d'œuvre après la 2ème année",
                ContactPhone = "0825123456",
                ContactEmail = "service@viessmann.fr",
                ClaimsCount = 0,
                ClaimedAmount = 0m,
                SendExpirationAlert = true,
                AlertDaysBefore = 180
            },

            // Garantie Groupe Froid Tour Défense
            new()
            {
                Id = Guid.NewGuid(),
                AssetId = Asset_Defense_GroupeFroid,
                Type = WarrantyType.Extended,
                ProviderName = "Trane Service",
                WarrantyNumber = "TRA-EXT-2020-001",
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2025, 12, 31),
                CoveredItems = "Compresseurs, échangeurs, régulation",
                Exclusions = "Fluides frigorigènes",
                ContactPhone = "0800505050",
                ContactEmail = "service.fr@trane.com",
                ClaimsCount = 2,
                ClaimedAmount = 4500m,
                SendExpirationAlert = true,
                AlertDaysBefore = 180
            },

            // Contrat maintenance ascenseur La Chapelle
            new()
            {
                Id = Guid.NewGuid(),
                AssetId = Asset_Chapelle_Ascenseur,
                Type = WarrantyType.ServiceContract,
                ProviderName = "Otis France",
                WarrantyNumber = "CTR-OTIS-CHAP-2023",
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2025, 12, 31),
                CoveredItems = "Maintenance préventive, dépannage 24/7, pièces courantes",
                Exclusions = "Modernisation, vandalisme",
                ContactPhone = "0825100200",
                ContactEmail = "service.client@otis.com",
                ClaimsCount = 5,
                ClaimedAmount = 2200m,
                SendExpirationAlert = true,
                AlertDaysBefore = 120
            },

            // Contrat SSI Tour Défense
            new()
            {
                Id = Guid.NewGuid(),
                AssetId = Asset_Defense_SSI,
                Type = WarrantyType.ServiceContract,
                ProviderName = "Honeywell Security",
                WarrantyNumber = "CTR-HON-DEF-2024",
                StartDate = new DateTime(2024, 1, 1),
                EndDate = new DateTime(2026, 12, 31),
                CoveredItems = "Maintenance Q18, vérifications trimestrielles, hotline 24/7",
                Exclusions = "Détecteurs HS par vandalisme",
                ContactPhone = "0820123456",
                ContactEmail = "ssi.service@honeywell.com",
                ClaimsCount = 0,
                ClaimedAmount = 0m,
                SendExpirationAlert = true,
                AlertDaysBefore = 180
            },
        };

        await context.Set<Warranty>().AddRangeAsync(warranties);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ Warranties créées");
    }

    #endregion
}
