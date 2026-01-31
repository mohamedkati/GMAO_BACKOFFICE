using GMAO.Domain.Entities;
using GMAO.Domain.Enums;
using GMAO.Domain.ValueObjects;
using GMAO.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GMAO.DATA.Seed.SeedData;

/// <summary>
/// Seeder pour la gestion client GMAO
/// PropertyGroups, Customers, Contacts et Budgets de maintenance
/// </summary>
public static class CustomerManagementSeeder
{
    #region PropertyGroup IDs

    public static readonly Guid PG_Foncia = Guid.Parse("d1000001-0000-0000-0000-000000000001");
    public static readonly Guid PG_Nexity = Guid.Parse("d1000001-0000-0000-0000-000000000002");
    public static readonly Guid PG_ParisHabitat = Guid.Parse("d1000001-0000-0000-0000-000000000003");
    public static readonly Guid PG_ICF = Guid.Parse("d1000001-0000-0000-0000-000000000004");
    public static readonly Guid PG_Citallios = Guid.Parse("d1000001-0000-0000-0000-000000000005");

    #endregion

    #region Customer IDs

    // Clients Foncia
    public static readonly Guid Customer_Foncia_ParisNord = Guid.Parse("d2000001-0000-0000-0000-000000000001");
    public static readonly Guid Customer_Foncia_ParisSud = Guid.Parse("d2000001-0000-0000-0000-000000000002");
    public static readonly Guid Customer_Foncia_92 = Guid.Parse("d2000001-0000-0000-0000-000000000003");

    // Clients Nexity
    public static readonly Guid Customer_Nexity_Studea = Guid.Parse("d2000001-0000-0000-0000-000000000004");
    public static readonly Guid Customer_Nexity_Lamy = Guid.Parse("d2000001-0000-0000-0000-000000000005");

    // Clients Paris Habitat
    public static readonly Guid Customer_ParisHabitat_Est = Guid.Parse("d2000001-0000-0000-0000-000000000006");
    public static readonly Guid Customer_ParisHabitat_Ouest = Guid.Parse("d2000001-0000-0000-0000-000000000007");

    // Clients indépendants (sans groupe)
    public static readonly Guid Customer_SCI_Monceau = Guid.Parse("d2000001-0000-0000-0000-000000000008");
    public static readonly Guid Customer_ASL_Jardins = Guid.Parse("d2000001-0000-0000-0000-000000000009");
    public static readonly Guid Customer_Copro_Rivoli = Guid.Parse("d2000001-0000-0000-0000-000000000010");
    public static readonly Guid Customer_Bureau_Defense = Guid.Parse("d2000001-0000-0000-0000-000000000011");
    public static readonly Guid Customer_Centre_Beaugrenelle = Guid.Parse("d2000001-0000-0000-0000-000000000012");

    #endregion

    public static async Task SeedAsync(AppDbContext context, Guid tenantId)
    {
        await SeedPropertyGroupsAsync(context, tenantId);
        await SeedPropertyGroupContactsAsync(context, tenantId);
        await SeedCustomersAsync(context, tenantId);
        await SeedCustomerContactsAsync(context, tenantId);
        await SeedMaintenanceBudgetsAsync(context, tenantId);
    }

    #region PropertyGroups

    private static async Task SeedPropertyGroupsAsync(AppDbContext context, Guid tenantId)
    {
        if (await context.PropertyGroups.AnyAsync())
            return;

        var propertyGroups = new List<PropertyGroup>
        {
            // ══════════════════════════════════════════════════════════════════
            // FONCIA - Grand groupe de gestion immobilière
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = PG_Foncia,
                TenantId = tenantId,
                Reference = "GRP-FONCIA",
                Name = "Groupe Foncia",
                Description = "Leader français de l'administration de biens - Syndic et gestion locative",
                Type = PropertyGroupType.PropertyManagementCompany,
                Status = PropertyGroupStatus.Active,
                LegalName = "FONCIA GROUPE SA",
                SIREN = "345678901",
                VATNumber = "FR12345678901",
                LegalForm = LegalForm.SA,
                HeadquartersAddress = new Address("27 Avenue de l'Opéra", "Paris", "75001", "France"),
                MainContactName = "Jean-Marc TORROLLION",
                MainContactPosition = "Directeur Régional Ile-de-France",
                MainContactEmail = "jm.torrollion@foncia.fr",
                MainContactPhone = "0140123456",
                AccountingContactName = "Claire DUMONT",
                AccountingContactEmail = "comptabilite.idf@foncia.fr",
                AccountingContactPhone = "0140123457",
                ConsolidatedBilling = true,
                PaymentTermsDays = 45,
                VolumeDiscountPercent = 5.0m,
                PreferredPaymentMethod = "Virement",
                GroupPricingCoefficients = new GroupPricingCoefficients(1.25m, 1.20m, 1.15m, 1.10m, 5.0m, 100000),
                TotalCustomers = 3,
                TotalSites = 45,
                TotalUnits = 2500,
                TotalAnnualRevenue = 850000m,
                FrameworkContractStartDate = new DateTime(2023, 1, 1),
                FrameworkContractEndDate = new DateTime(2025, 12, 31),
                FrameworkContractReference = "CTR-FONCIA-2023-001",
                AutoRenewalFrameworkContract = true,
                InternalNotes = "Client stratégique - Contrat cadre en cours",
                CommercialNotes = "Potentiel d'extension sur nouveaux territoires",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // ══════════════════════════════════════════════════════════════════
            // NEXITY - Promotion et gestion immobilière
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = PG_Nexity,
                TenantId = tenantId,
                Reference = "GRP-NEXITY",
                Name = "Groupe Nexity",
                Description = "Groupe immobilier intégré - Promotion, gestion, services",
                Type = PropertyGroupType.PropertyManagementCompany,
                Status = PropertyGroupStatus.Active,
                LegalName = "NEXITY SA",
                SIREN = "456789012",
                VATNumber = "FR23456789012",
                LegalForm = LegalForm.SA,
                HeadquartersAddress = new Address("19 Rue de Vienne", "Paris", "75008", "France"),
                MainContactName = "Philippe LAURENT",
                MainContactPosition = "Directeur Property Management",
                MainContactEmail = "philippe.laurent@nexity.fr",
                MainContactPhone = "0153456789",
                AccountingContactName = "Marie VINCENT",
                AccountingContactEmail = "facturation@nexity.fr",
                AccountingContactPhone = "0153456790",
                ConsolidatedBilling = true,
                PaymentTermsDays = 30,
                VolumeDiscountPercent = 3.0m,
                PreferredPaymentMethod = "Virement",
                GroupPricingCoefficients = new GroupPricingCoefficients(1.28m, 1.22m, 1.18m, 1.12m, 3.0m, 50000),
                TotalCustomers = 2,
                TotalSites = 30,
                TotalUnits = 1800,
                TotalAnnualRevenue = 520000m,
                FrameworkContractStartDate = new DateTime(2024, 1, 1),
                FrameworkContractEndDate = new DateTime(2026, 12, 31),
                FrameworkContractReference = "CTR-NEXITY-2024-001",
                AutoRenewalFrameworkContract = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // ══════════════════════════════════════════════════════════════════
            // PARIS HABITAT - Bailleur social
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = PG_ParisHabitat,
                TenantId = tenantId,
                Reference = "GRP-PHABITAT",
                Name = "Paris Habitat",
                Description = "Premier bailleur social de Paris - Office public de l'habitat",
                Type = PropertyGroupType.PublicHousing,
                Status = PropertyGroupStatus.Active,
                LegalName = "PARIS HABITAT - OPH",
                SIREN = "567890123",
                VATNumber = "FR34567890123",
                LegalForm = LegalForm.PublicEntity,
                HeadquartersAddress = new Address("21 bis Rue Claude Bernard", "Paris", "75005", "France"),
                MainContactName = "Stéphane DAUPHIN",
                MainContactPosition = "Directeur de la Maintenance",
                MainContactEmail = "s.dauphin@parishabitat.fr",
                MainContactPhone = "0171234567",
                AccountingContactName = "Béatrice MORIN",
                AccountingContactEmail = "comptabilite@parishabitat.fr",
                AccountingContactPhone = "0171234568",
                ConsolidatedBilling = false,
                PaymentTermsDays = 60,
                VolumeDiscountPercent = 8.0m,
                PreferredPaymentMethod = "Virement",
                GroupPricingCoefficients = new GroupPricingCoefficients(1.20m, 1.15m, 1.12m, 1.08m, 8.0m, 200000),
                TotalCustomers = 2,
                TotalSites = 120,
                TotalUnits = 8500,
                TotalAnnualRevenue = 1200000m,
                FrameworkContractStartDate = new DateTime(2022, 1, 1),
                FrameworkContractEndDate = new DateTime(2025, 12, 31),
                FrameworkContractReference = "MAPA-PH-2022-MAINT-001",
                AutoRenewalFrameworkContract = false,
                InternalNotes = "Marché public - Procédure de renouvellement en cours",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // ══════════════════════════════════════════════════════════════════
            // ICF HABITAT - Bailleur social groupe SNCF
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = PG_ICF,
                TenantId = tenantId,
                Reference = "GRP-ICF",
                Name = "ICF Habitat",
                Description = "Bailleur social - Groupe SNCF",
                Type = PropertyGroupType.PublicHousing,
                Status = PropertyGroupStatus.Active,
                LegalName = "ICF HABITAT LA SABLIERE",
                SIREN = "678901234",
                VATNumber = "FR45678901234",
                LegalForm = LegalForm.SA,
                HeadquartersAddress = new Address("19 Rue de Châteaudun", "Paris", "75009", "France"),
                MainContactName = "Caroline PERROT",
                MainContactPosition = "Responsable Patrimoine",
                MainContactEmail = "c.perrot@icfhabitat.fr",
                MainContactPhone = "0144567890",
                ConsolidatedBilling = true,
                PaymentTermsDays = 45,
                TotalCustomers = 1,
                TotalSites = 25,
                TotalUnits = 3200,
                TotalAnnualRevenue = 380000m,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // ══════════════════════════════════════════════════════════════════
            // CITALLIOS - Aménageur / EPL
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = PG_Citallios,
                TenantId = tenantId,
                Reference = "GRP-CITALLIOS",
                Name = "Citallios",
                Description = "Entreprise Publique Locale d'aménagement des Hauts-de-Seine",
                Type = PropertyGroupType.PublicHousing,
                Status = PropertyGroupStatus.Prospect,
                LegalName = "CITALLIOS",
                SIREN = "789012345",
                LegalForm = LegalForm.SAS,
                HeadquartersAddress = new Address("2 Rue Pablo Neruda", "Nanterre", "92000", "France"),
                MainContactName = "François BERTRAND",
                MainContactPosition = "Directeur Technique",
                MainContactEmail = "f.bertrand@citallios.fr",
                MainContactPhone = "0147890123",
                CommercialNotes = "En prospection - RDV prévu Q1 2025",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
        };

        await context.PropertyGroups.AddRangeAsync(propertyGroups);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ PropertyGroups créés");
    }

    #endregion

    #region PropertyGroupContacts

    private static async Task SeedPropertyGroupContactsAsync(AppDbContext context, Guid tenantId)
    {
        if (await context.PropertyGroupsContacts.AnyAsync())
            return;

        var contacts = new List<PropertyGroupContact>
        {
            // Contacts Foncia
            new()
            {
                Id = Guid.NewGuid(),
                PropertyGroupId = PG_Foncia,
                TenantId = tenantId,
                Role = ContactRole.TechnicalManager,
                PersonType = PersonType.Individual,
                FirstName = "Antoine",
                LastName = "MARCHAND",
                Position = "Responsable Technique Régional",
                Department = "Direction Technique",
                Email = "a.marchand@foncia.fr",
                Phone = "0140123458",
                Mobile = "0678901234",
                IsPrimary = false,
                ReceivesInvoices = false,
                ReceivesReports = true,
                ReceivesAlerts = true,
                PreferredContactMethod = PreferredContactMethod.Email,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                PropertyGroupId = PG_Foncia,
                TenantId = tenantId,
                Role = ContactRole.AccountingManager,
                PersonType = PersonType.Individual,
                FirstName = "Claire",
                LastName = "DUMONT",
                Position = "Responsable Comptabilité",
                Department = "Direction Financière",
                Email = "c.dumont@foncia.fr",
                Phone = "0140123459",
                IsPrimary = false,
                ReceivesInvoices = true,
                ReceivesReports = false,
                ReceivesAlerts = false,
                PreferredContactMethod = PreferredContactMethod.Email,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            
            // Contacts Paris Habitat
            new()
            {
                Id = Guid.NewGuid(),
                PropertyGroupId = PG_ParisHabitat,
                TenantId = tenantId,
                Role = ContactRole.OperationsManager,
                PersonType = PersonType.Individual,
                FirstName = "Michel",
                LastName = "BLANCHARD",
                Position = "Chef de Service Maintenance",
                Department = "Direction du Patrimoine",
                Email = "m.blanchard@parishabitat.fr",
                Phone = "0171234569",
                Mobile = "0698765432",
                IsPrimary = true,
                ReceivesInvoices = false,
                ReceivesReports = true,
                ReceivesAlerts = true,
                PreferredContactMethod = PreferredContactMethod.Phone,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
        };

        await context.PropertyGroupsContacts.AddRangeAsync(contacts);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ PropertyGroupContacts créés");
    }

    #endregion

    #region Customers

    private static async Task SeedCustomersAsync(AppDbContext context, Guid tenantId)
    {
        if (await context.Customers.AnyAsync())
            return;

        var customers = new List<Customer>
        {
            // ══════════════════════════════════════════════════════════════════
            // CLIENTS FONCIA
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Customer_Foncia_ParisNord,
                TenantId = tenantId,
                Reference = "CLI-FON-75N",
                CompanyName = "Foncia Paris Nord",
                Type = CustomerType.Syndic,
                PropertyGroupId = PG_Foncia,
                Siren = "345678901",
                Comment = "Agence principale secteur Nord Paris - 18ème, 19ème, 10ème",
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_Senior,
                PaymentMethodId = ReferenceDataSeeder.Payment_Virement45J,
                InvoiceAddress = new Address("45 Rue de la Chapelle", "Paris", "75018", "France"),
                MailingAddress = new Address("45 Rue de la Chapelle", "Paris", "75018", "France"),
                PricingCoefficients = new PricingCoefficients(1.25m, 1.20m, 1.15m, 1.10m),
                BillingSettings = new BillingSettings
                {
                    Mode = BillingMode.PerSite,
                    AutoGenerateInvoices = true,
                    InvoiceFrequency = InvoiceFrequency.Monthly,
                    SendEmailNotifications = true,
                    ApplyLatePaymentFees = false
                },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Customer_Foncia_ParisSud,
                TenantId = tenantId,
                Reference = "CLI-FON-75S",
                CompanyName = "Foncia Paris Sud",
                Type = CustomerType.Syndic,
                PropertyGroupId = PG_Foncia,
                Siren = "345678901",
                Comment = "Agence secteur Sud Paris - 13ème, 14ème, 15ème",
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_Senior,
                PaymentMethodId = ReferenceDataSeeder.Payment_Virement45J,
                InvoiceAddress = new Address("120 Avenue d'Italie", "Paris", "75013", "France"),
                MailingAddress = new Address("120 Avenue d'Italie", "Paris", "75013", "France"),
                PricingCoefficients = new PricingCoefficients(1.25m, 1.20m, 1.15m, 1.10m),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Customer_Foncia_92,
                TenantId = tenantId,
                Reference = "CLI-FON-92",
                CompanyName = "Foncia Hauts-de-Seine",
                Type = CustomerType.Syndic,
                PropertyGroupId = PG_Foncia,
                Siren = "345678901",
                Comment = "Agence département 92",
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_GrandsComptes,
                PaymentMethodId = ReferenceDataSeeder.Payment_Virement45J,
                InvoiceAddress = new Address("8 Place de la Défense", "Puteaux", "92800", "France"),
                MailingAddress = new Address("8 Place de la Défense", "Puteaux", "92800", "France"),
                PricingCoefficients = new PricingCoefficients(1.25m, 1.20m, 1.15m, 1.10m),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // ══════════════════════════════════════════════════════════════════
            // CLIENTS NEXITY
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Customer_Nexity_Studea,
                TenantId = tenantId,
                Reference = "CLI-NEX-STU",
                CompanyName = "Nexity Studéa",
                Type = CustomerType.PropertyManager,
                PropertyGroupId = PG_Nexity,
                Siren = "456789012",
                Comment = "Résidences étudiantes Studéa en Ile-de-France",
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_Junior,
                PaymentMethodId = ReferenceDataSeeder.Payment_Virement30J,
                InvoiceAddress = new Address("19 Rue de Vienne", "Paris", "75008", "France"),
                MailingAddress = new Address("19 Rue de Vienne", "Paris", "75008", "France"),
                PricingCoefficients = new PricingCoefficients(1.28m, 1.22m, 1.18m, 1.12m),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Customer_Nexity_Lamy,
                TenantId = tenantId,
                Reference = "CLI-NEX-LAMY",
                CompanyName = "Nexity Lamy",
                Type = CustomerType.Syndic,
                PropertyGroupId = PG_Nexity,
                Siren = "456789012",
                Comment = "Syndic de copropriété branche Lamy",
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_Junior,
                PaymentMethodId = ReferenceDataSeeder.Payment_Virement30J,
                InvoiceAddress = new Address("15 Rue Marbeuf", "Paris", "75008", "France"),
                MailingAddress = new Address("15 Rue Marbeuf", "Paris", "75008", "France"),
                PricingCoefficients = new PricingCoefficients(1.28m, 1.22m, 1.18m, 1.12m),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // ══════════════════════════════════════════════════════════════════
            // CLIENTS PARIS HABITAT
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Customer_ParisHabitat_Est,
                TenantId = tenantId,
                Reference = "CLI-PH-EST",
                CompanyName = "Paris Habitat - Direction Est",
                Type = CustomerType.Government,
                PropertyGroupId = PG_ParisHabitat,
                Siren = "567890123",
                Comment = "Direction territoriale Est - 10ème, 11ème, 12ème, 19ème, 20ème",
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_GrandsComptes,
                PaymentMethodId = ReferenceDataSeeder.Payment_Virement60J,
                InvoiceAddress = new Address("15 Rue de Belleville", "Paris", "75020", "France"),
                MailingAddress = new Address("21 bis Rue Claude Bernard", "Paris", "75005", "France"),
                PricingCoefficients = new PricingCoefficients(1.20m, 1.15m, 1.12m, 1.08m),
                BillingSettings = new BillingSettings
                {
                    Mode = BillingMode.PerSite,
                    AutoGenerateInvoices = false,
                    InvoiceFrequency = InvoiceFrequency.Monthly,
                    SendEmailNotifications = true,
                    ApplyLatePaymentFees = false
                },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Customer_ParisHabitat_Ouest,
                TenantId = tenantId,
                Reference = "CLI-PH-OUEST",
                CompanyName = "Paris Habitat - Direction Ouest",
                Type = CustomerType.Government,
                PropertyGroupId = PG_ParisHabitat,
                Siren = "567890123",
                Comment = "Direction territoriale Ouest - 13ème, 14ème, 15ème, 16ème, 17ème, 18ème",
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_GrandsComptes,
                PaymentMethodId = ReferenceDataSeeder.Payment_Virement60J,
                InvoiceAddress = new Address("35 Rue Leblanc", "Paris", "75015", "France"),
                MailingAddress = new Address("21 bis Rue Claude Bernard", "Paris", "75005", "France"),
                PricingCoefficients = new PricingCoefficients(1.20m, 1.15m, 1.12m, 1.08m),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // ══════════════════════════════════════════════════════════════════
            // CLIENTS INDÉPENDANTS
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Customer_SCI_Monceau,
                TenantId = tenantId,
                Reference = "CLI-SCI-MONC",
                CompanyName = "SCI Monceau Investissement",
                Type = CustomerType.Individual,
                PropertyGroupId = null,
                Siren = "890123456",
                Comment = "SCI familiale - Immeubles de rapport secteur Monceau",
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_Senior,
                PaymentMethodId = ReferenceDataSeeder.Payment_Virement30J,
                InvoiceAddress = new Address("25 Boulevard Malesherbes", "Paris", "75008", "France"),
                MailingAddress = new Address("25 Boulevard Malesherbes", "Paris", "75008", "France"),
                PricingCoefficients = PricingCoefficients.Default,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Customer_ASL_Jardins,
                TenantId = tenantId,
                Reference = "CLI-ASL-JARD",
                CompanyName = "ASL Les Jardins de Neuilly",
                Type = CustomerType.Syndic,
                PropertyGroupId = null,
                Siren = "901234567",
                Comment = "Association Syndicale Libre - Ensemble résidentiel haut de gamme",
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_Senior,
                PaymentMethodId = ReferenceDataSeeder.Payment_Virement30J,
                InvoiceAddress = new Address("45 Avenue Charles de Gaulle", "Neuilly-sur-Seine", "92200", "France"),
                MailingAddress = new Address("45 Avenue Charles de Gaulle", "Neuilly-sur-Seine", "92200", "France"),
                PricingCoefficients = new PricingCoefficients(1.35m, 1.30m, 1.25m, 1.20m),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Customer_Copro_Rivoli,
                TenantId = tenantId,
                Reference = "CLI-COP-RIV",
                CompanyName = "Copropriété 156 Rue de Rivoli",
                Type = CustomerType.Syndic,
                PropertyGroupId = null,
                Siren = "012345678",
                Comment = "Immeuble haussmannien - Syndic bénévole",
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_Junior,
                PaymentMethodId = ReferenceDataSeeder.Payment_Cheque,
                InvoiceAddress = new Address("156 Rue de Rivoli", "Paris", "75001", "France"),
                MailingAddress = new Address("156 Rue de Rivoli", "Paris", "75001", "France"),
                PricingCoefficients = PricingCoefficients.Default,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Customer_Bureau_Defense,
                TenantId = tenantId,
                Reference = "CLI-TER-DEF",
                CompanyName = "Tour Areva - SCI Défense Office",
                Type = CustomerType.Corporate,
                PropertyGroupId = null,
                Siren = "123456789",
                Comment = "Tour de bureaux à La Défense - Contrat multi-technique",
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_GrandsComptes,
                PaymentMethodId = ReferenceDataSeeder.Payment_Virement30J,
                InvoiceAddress = new Address("1 Place Jean Millier", "Courbevoie", "92400", "France"),
                MailingAddress = new Address("1 Place Jean Millier", "Courbevoie", "92400", "France"),
                PricingCoefficients = new PricingCoefficients(1.22m, 1.18m, 1.15m, 1.10m),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Customer_Centre_Beaugrenelle,
                TenantId = tenantId,
                Reference = "CLI-COM-BEAU",
                CompanyName = "Centre Commercial Beaugrenelle",
                Type = CustomerType.Corporate,
                PropertyGroupId = null,
                Siren = "234567890",
                Comment = "Centre commercial - Maintenance parties communes et techniques",
                CommercialId = StaffAndRolesSeeder.Staff_Commercial_GrandsComptes,
                PaymentMethodId = ReferenceDataSeeder.Payment_Prelevement,
                InvoiceAddress = new Address("12 Rue Linois", "Paris", "75015", "France"),
                MailingAddress = new Address("12 Rue Linois", "Paris", "75015", "France"),
                PricingCoefficients = new PricingCoefficients(1.25m, 1.20m, 1.18m, 1.12m),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
        };

        await context.Customers.AddRangeAsync(customers);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ Customers créés");
    }

    #endregion

    #region CustomerContacts

    private static async Task SeedCustomerContactsAsync(AppDbContext context, Guid tenantId)
    {
        if (await context.CustomerContacts.AnyAsync())
            return;

        var contacts = new List<CustomerContact>
        {
            // Contacts Foncia Paris Nord
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CustomerId = Customer_Foncia_ParisNord,
                Type = PersonType.Individual,
                FirstName = "Sandrine",
                LastName = "PETIT",
                Email = "s.petit@foncia.fr",
                Phone = "0140123460",
                Mobile = "0678901235",
                Position = "Gestionnaire Principal",
                IsPrimary = true,
                PreferredContactMethod = PreferredContactMethod.Email,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CustomerId = Customer_Foncia_ParisNord,
                Type = PersonType.Individual,
                FirstName = "Julien",
                LastName = "LEMAIRE",
                Email = "j.lemaire@foncia.fr",
                Phone = "0140123461",
                Position = "Assistant Technique",
                IsPrimary = false,
                PreferredContactMethod = PreferredContactMethod.Phone,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // Contacts Paris Habitat Est
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CustomerId = Customer_ParisHabitat_Est,
                Type = PersonType.Individual,
                FirstName = "Fatima",
                LastName = "BENALI",
                Email = "f.benali@parishabitat.fr",
                Phone = "0171234570",
                Mobile = "0698765433",
                Position = "Responsable de Secteur",
                IsPrimary = true,
                PreferredContactMethod = PreferredContactMethod.Email,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CustomerId = Customer_ParisHabitat_Est,
                Type = PersonType.Individual,
                FirstName = "Youssef",
                LastName = "HAMIDI",
                Email = "y.hamidi@parishabitat.fr",
                Phone = "0171234571",
                Position = "Technicien de Proximité",
                IsPrimary = false,
                PreferredContactMethod = PreferredContactMethod.Phone,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // Contact SCI Monceau
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CustomerId = Customer_SCI_Monceau,
                Type = PersonType.Individual,
                FirstName = "Henri",
                LastName = "DUVAL",
                Email = "h.duval@sci-monceau.fr",
                Phone = "0145678902",
                Mobile = "0612345678",
                Position = "Gérant",
                IsPrimary = true,
                PreferredContactMethod = PreferredContactMethod.Phone,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // Contact Bureau Défense
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CustomerId = Customer_Bureau_Defense,
                Type = PersonType.Individual,
                FirstName = "Caroline",
                LastName = "MARTIN",
                Email = "c.martin@defense-office.fr",
                Phone = "0147890234",
                Position = "Facility Manager",
                IsPrimary = true,
                PreferredContactMethod = PreferredContactMethod.Email,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CustomerId = Customer_Bureau_Defense,
                Type = PersonType.Individual,
                FirstName = "Patrick",
                LastName = "LEGRAND",
                Email = "p.legrand@defense-office.fr",
                Phone = "0147890235",
                Position = "Responsable Sécurité",
                IsPrimary = false,
                PreferredContactMethod = PreferredContactMethod.Phone,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
        };

        await context.CustomerContacts.AddRangeAsync(contacts);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ CustomerContacts créés");
    }

    #endregion

    #region MaintenanceBudgets

    private static async Task SeedMaintenanceBudgetsAsync(AppDbContext context, Guid tenantId)
    {
        if (await context.MaintenanceBudgets.AnyAsync())
            return;

        var currentYear = DateTime.UtcNow.Year;

        var budgets = new List<MaintenanceBudget>
        {
            // Budgets Foncia Paris Nord
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CustomerId = Customer_Foncia_ParisNord,
                Year = currentYear,
                BudgetedAmount = 180000m,
                CommittedAmount = 45000m,
                InvoicedAmount = 95000m,
                AlertThreshold = 80m,
                AlertSent = false,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CustomerId = Customer_Foncia_ParisNord,
                Year = currentYear - 1,
                BudgetedAmount = 165000m,
                CommittedAmount = 0m,
                InvoicedAmount = 158500m,
                AlertThreshold = 80m,
                AlertSent = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // Budgets Paris Habitat Est
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CustomerId = Customer_ParisHabitat_Est,
                Year = currentYear,
                BudgetedAmount = 450000m,
                CommittedAmount = 85000m,
                InvoicedAmount = 210000m,
                AlertThreshold = 75m,
                AlertSent = false,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // Budget Bureau Défense
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CustomerId = Customer_Bureau_Defense,
                Year = currentYear,
                BudgetedAmount = 320000m,
                CommittedAmount = 55000m,
                InvoicedAmount = 145000m,
                AlertThreshold = 85m,
                AlertSent = false,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },

            // Budget Centre Commercial Beaugrenelle
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CustomerId = Customer_Centre_Beaugrenelle,
                Year = currentYear,
                BudgetedAmount = 280000m,
                CommittedAmount = 35000m,
                InvoicedAmount = 190000m,
                AlertThreshold = 80m,
                AlertSent = true, // Alerte déjà envoyée car > 80%
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Empty
            },
        };

        await context.MaintenanceBudgets.AddRangeAsync(budgets);
        await context.SaveChangesAsync();
        Console.WriteLine("✓ MaintenanceBudgets créés");
    }

    #endregion
}
