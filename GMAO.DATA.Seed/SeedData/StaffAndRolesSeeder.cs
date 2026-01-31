using GMAO.Domain.Entities;
using GMAO.Domain.Entities.Auth;
using GMAO.Domain.Enums;
using GMAO.Domain.ValueObjects;
using GMAO.Infrastructure.Persistance;
using GMAO.Infrastructure.Persistance.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMAO.DATA.Seed.SeedData;

/// <summary>
/// Seeder pour les rôles métier GMAO et le personnel
/// Rôles typiques d'une entreprise de maintenance immobilière
/// </summary>
public static class StaffAndRolesSeeder
{
    #region Role IDs

    public static readonly Guid Role_SuperAdmin = Guid.Parse("11111111-1111-1111-1111-111111111111");
    public static readonly Guid Role_Admin = Guid.Parse("22222222-2222-2222-2222-222222222222");
    public static readonly Guid Role_DirecteurExploitation = Guid.Parse("b1000001-0000-0000-0000-000000000001");
    public static readonly Guid Role_ResponsableExploitation = Guid.Parse("b1000001-0000-0000-0000-000000000002");
    public static readonly Guid Role_ChefSecteur = Guid.Parse("b1000001-0000-0000-0000-000000000003");
    public static readonly Guid Role_Commercial = Guid.Parse("b1000001-0000-0000-0000-000000000004");
    public static readonly Guid Role_Technicien = Guid.Parse("b1000001-0000-0000-0000-000000000005");
    public static readonly Guid Role_Dispatcher = Guid.Parse("b1000001-0000-0000-0000-000000000006");
    public static readonly Guid Role_Comptable = Guid.Parse("b1000001-0000-0000-0000-000000000007");
    public static readonly Guid Role_AssistantTechnique = Guid.Parse("b1000001-0000-0000-0000-000000000008");

    #endregion

    #region Staff IDs

    // Direction
    public static readonly Guid Staff_DirecteurExploitation = Guid.Parse("c1000001-0000-0000-0000-000000000001");
    
    // Responsables Exploitation (par zone géographique)
    public static readonly Guid Staff_ResponsableExploitation_Paris = Guid.Parse("c1000001-0000-0000-0000-000000000002");
    public static readonly Guid Staff_ResponsableExploitation_IDF = Guid.Parse("c1000001-0000-0000-0000-000000000003");
    
    // Chefs de Secteur
    public static readonly Guid Staff_ChefSecteur_Paris_Nord = Guid.Parse("c1000001-0000-0000-0000-000000000004");
    public static readonly Guid Staff_ChefSecteur_Paris_Sud = Guid.Parse("c1000001-0000-0000-0000-000000000005");
    public static readonly Guid Staff_ChefSecteur_92 = Guid.Parse("c1000001-0000-0000-0000-000000000006");
    public static readonly Guid Staff_ChefSecteur_93_94 = Guid.Parse("c1000001-0000-0000-0000-000000000007");
    
    // Commerciaux
    public static readonly Guid Staff_Commercial_Senior = Guid.Parse("c1000001-0000-0000-0000-000000000008");
    public static readonly Guid Staff_Commercial_Junior = Guid.Parse("c1000001-0000-0000-0000-000000000009");
    public static readonly Guid Staff_Commercial_GrandsComptes = Guid.Parse("c1000001-0000-0000-0000-000000000010");
    
    // Techniciens
    public static readonly Guid Staff_Technicien_Plombier_1 = Guid.Parse("c1000001-0000-0000-0000-000000000011");
    public static readonly Guid Staff_Technicien_Plombier_2 = Guid.Parse("c1000001-0000-0000-0000-000000000012");
    public static readonly Guid Staff_Technicien_Electricien_1 = Guid.Parse("c1000001-0000-0000-0000-000000000013");
    public static readonly Guid Staff_Technicien_Electricien_2 = Guid.Parse("c1000001-0000-0000-0000-000000000014");
    public static readonly Guid Staff_Technicien_CVC = Guid.Parse("c1000001-0000-0000-0000-000000000015");
    public static readonly Guid Staff_Technicien_MultiTechnique_1 = Guid.Parse("c1000001-0000-0000-0000-000000000016");
    public static readonly Guid Staff_Technicien_MultiTechnique_2 = Guid.Parse("c1000001-0000-0000-0000-000000000017");
    public static readonly Guid Staff_Technicien_Serrurerie = Guid.Parse("c1000001-0000-0000-0000-000000000018");
    
    // Dispatcher / Planification
    public static readonly Guid Staff_Dispatcher_1 = Guid.Parse("c1000001-0000-0000-0000-000000000019");
    public static readonly Guid Staff_Dispatcher_2 = Guid.Parse("c1000001-0000-0000-0000-000000000020");
    
    // Comptabilité
    public static readonly Guid Staff_Comptable = Guid.Parse("c1000001-0000-0000-0000-000000000021");
    
    // Assistants
    public static readonly Guid Staff_AssistantTechnique_1 = Guid.Parse("c1000001-0000-0000-0000-000000000022");
    public static readonly Guid Staff_AssistantTechnique_2 = Guid.Parse("c1000001-0000-0000-0000-000000000023");

    #endregion

    public static async Task SeedAsync(
        AppDbContext context, 
        UserManager<ApplicationUser> userManager,
        Guid tenantId)
    {
        await SeedRolesAsync(context);
        await SeedStaffAsync(context, userManager, tenantId);
    }

    #region Roles

    private static async Task SeedRolesAsync(AppDbContext context)
    {
        var existingRoles = await context.DomainRoles.Select(r => r.Id).ToListAsync();

        var roles = new List<Role>
        {
            new()
            {
                Id = Role_DirecteurExploitation,
                Name = "DirecteurExploitation",
                DisplayName = "Directeur d'Exploitation",
                Description = "Direction générale de l'exploitation, supervision de tous les responsables",
                IsSystem = true,
                Priority = 95
            },
            new()
            {
                Id = Role_ResponsableExploitation,
                Name = "ResponsableExploitation",
                DisplayName = "Responsable d'Exploitation",
                Description = "Gestion opérationnelle d'une zone géographique, supervision des chefs de secteur",
                IsSystem = true,
                Priority = 85
            },
            new()
            {
                Id = Role_ChefSecteur,
                Name = "ChefSecteur",
                DisplayName = "Chef de Secteur",
                Description = "Responsable d'un secteur géographique, encadrement des techniciens terrain",
                IsSystem = true,
                Priority = 75
            },
            new()
            {
                Id = Role_Commercial,
                Name = "Commercial",
                DisplayName = "Commercial",
                Description = "Gestion de la relation client, devis, contrats",
                IsSystem = true,
                Priority = 70
            },
            new()
            {
                Id = Role_Technicien,
                Name = "Technicien",
                DisplayName = "Technicien",
                Description = "Exécution des interventions de maintenance sur le terrain",
                IsSystem = true,
                Priority = 50
            },
            new()
            {
                Id = Role_Dispatcher,
                Name = "Dispatcher",
                DisplayName = "Dispatcher / Planificateur",
                Description = "Réception des demandes, planification et affectation des interventions",
                IsSystem = true,
                Priority = 65
            },
            new()
            {
                Id = Role_Comptable,
                Name = "Comptable",
                DisplayName = "Comptable",
                Description = "Gestion de la facturation, suivi des paiements",
                IsSystem = true,
                Priority = 60
            },
            new()
            {
                Id = Role_AssistantTechnique,
                Name = "AssistantTechnique",
                DisplayName = "Assistant Technique",
                Description = "Support administratif et technique, saisie des rapports",
                IsSystem = true,
                Priority = 40
            },
        };

        var rolesToAdd = roles.Where(r => !existingRoles.Contains(r.Id)).ToList();
        
        if (rolesToAdd.Any())
        {
            await context.DomainRoles.AddRangeAsync(rolesToAdd);
            await context.SaveChangesAsync();
            Console.WriteLine($"✓ {rolesToAdd.Count} Rôles métier GMAO créés");
        }
    }

    #endregion

    #region Staff

    private static async Task SeedStaffAsync(
        AppDbContext context, 
        UserManager<ApplicationUser> userManager,
        Guid tenantId)
    {
        var staffDefinitions = GetStaffDefinitions();

        foreach (var def in staffDefinitions)
        {
            await CreateStaffMemberAsync(context, userManager, tenantId, def);
        }

        Console.WriteLine($"✓ {staffDefinitions.Count} membres du personnel créés");
    }

    private static List<StaffDefinition> GetStaffDefinitions()
    {
        return new List<StaffDefinition>
        {
            // ══════════════════════════════════════════════════════════════════
            // DIRECTION
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Staff_DirecteurExploitation,
                EmployeeNumber = "DIR001",
                FirstName = "Philippe",
                LastName = "MARTIN",
                Email = "philippe.martin@techmaint.fr",
                Phone = "0145678901",
                UserName = "pmartin",
                RoleId = Role_DirecteurExploitation,
                Status = StaffStatus.Active,
                Address = new Address("15 Avenue des Champs-Élysées", "Paris", "75008", "France")
            },

            // ══════════════════════════════════════════════════════════════════
            // RESPONSABLES EXPLOITATION
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Staff_ResponsableExploitation_Paris,
                EmployeeNumber = "RE001",
                FirstName = "Sophie",
                LastName = "DUBOIS",
                Email = "sophie.dubois@techmaint.fr",
                Phone = "0145678902",
                UserName = "sdubois",
                RoleId = Role_ResponsableExploitation,
                Status = StaffStatus.Active,
                Address = new Address("25 Rue de la Paix", "Paris", "75002", "France")
            },
            new()
            {
                Id = Staff_ResponsableExploitation_IDF,
                EmployeeNumber = "RE002",
                FirstName = "Laurent",
                LastName = "BERNARD",
                Email = "laurent.bernard@techmaint.fr",
                Phone = "0145678903",
                UserName = "lbernard",
                RoleId = Role_ResponsableExploitation,
                Status = StaffStatus.Active,
                Address = new Address("8 Boulevard Haussmann", "Paris", "75009", "France")
            },

            // ══════════════════════════════════════════════════════════════════
            // CHEFS DE SECTEUR
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Staff_ChefSecteur_Paris_Nord,
                EmployeeNumber = "CS001",
                FirstName = "Marc",
                LastName = "LEROY",
                Email = "marc.leroy@techmaint.fr",
                Phone = "0645678901",
                UserName = "mleroy",
                RoleId = Role_ChefSecteur,
                Status = StaffStatus.Active,
                Address = new Address("45 Rue de la Chapelle", "Paris", "75018", "France")
            },
            new()
            {
                Id = Staff_ChefSecteur_Paris_Sud,
                EmployeeNumber = "CS002",
                FirstName = "Nathalie",
                LastName = "MOREAU",
                Email = "nathalie.moreau@techmaint.fr",
                Phone = "0645678902",
                UserName = "nmoreau",
                RoleId = Role_ChefSecteur,
                Status = StaffStatus.Active,
                Address = new Address("120 Avenue d'Italie", "Paris", "75013", "France")
            },
            new()
            {
                Id = Staff_ChefSecteur_92,
                EmployeeNumber = "CS003",
                FirstName = "Christophe",
                LastName = "PETIT",
                Email = "christophe.petit@techmaint.fr",
                Phone = "0645678903",
                UserName = "cpetit",
                RoleId = Role_ChefSecteur,
                Status = StaffStatus.Active,
                Address = new Address("5 Place de la Défense", "Puteaux", "92800", "France")
            },
            new()
            {
                Id = Staff_ChefSecteur_93_94,
                EmployeeNumber = "CS004",
                FirstName = "Karim",
                LastName = "BENALI",
                Email = "karim.benali@techmaint.fr",
                Phone = "0645678904",
                UserName = "kbenali",
                RoleId = Role_ChefSecteur,
                Status = StaffStatus.Active,
                Address = new Address("15 Avenue Jean Jaurès", "Montreuil", "93100", "France")
            },

            // ══════════════════════════════════════════════════════════════════
            // COMMERCIAUX
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Staff_Commercial_Senior,
                EmployeeNumber = "COM001",
                FirstName = "Isabelle",
                LastName = "ROUSSEAU",
                Email = "isabelle.rousseau@techmaint.fr",
                Phone = "0678901234",
                UserName = "irousseau",
                RoleId = Role_Commercial,
                Status = StaffStatus.Active,
                Address = new Address("30 Rue du Commerce", "Paris", "75015", "France")
            },
            new()
            {
                Id = Staff_Commercial_Junior,
                EmployeeNumber = "COM002",
                FirstName = "Thomas",
                LastName = "GARCIA",
                Email = "thomas.garcia@techmaint.fr",
                Phone = "0678901235",
                UserName = "tgarcia",
                RoleId = Role_Commercial,
                Status = StaffStatus.Active,
                Address = new Address("22 Rue de Rivoli", "Paris", "75004", "France")
            },
            new()
            {
                Id = Staff_Commercial_GrandsComptes,
                EmployeeNumber = "COM003",
                FirstName = "Éric",
                LastName = "LAMBERT",
                Email = "eric.lambert@techmaint.fr",
                Phone = "0678901236",
                UserName = "elambert",
                RoleId = Role_Commercial,
                Status = StaffStatus.Active,
                Address = new Address("1 Place Vendôme", "Paris", "75001", "France")
            },

            // ══════════════════════════════════════════════════════════════════
            // TECHNICIENS
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Staff_Technicien_Plombier_1,
                EmployeeNumber = "TECH001",
                FirstName = "Jean-Pierre",
                LastName = "DURAND",
                Email = "jp.durand@techmaint.fr",
                Phone = "0612345601",
                UserName = "jpdurand",
                RoleId = Role_Technicien,
                Status = StaffStatus.Active,
                IsPdaActive = true,
                Address = new Address("56 Rue des Lilas", "Aubervilliers", "93300", "France")
            },
            new()
            {
                Id = Staff_Technicien_Plombier_2,
                EmployeeNumber = "TECH002",
                FirstName = "Mohamed",
                LastName = "DIALLO",
                Email = "mohamed.diallo@techmaint.fr",
                Phone = "0612345602",
                UserName = "mdiallo",
                RoleId = Role_Technicien,
                Status = StaffStatus.Active,
                IsPdaActive = true,
                Address = new Address("23 Avenue de la République", "Saint-Denis", "93200", "France")
            },
            new()
            {
                Id = Staff_Technicien_Electricien_1,
                EmployeeNumber = "TECH003",
                FirstName = "Pierre",
                LastName = "FONTAINE",
                Email = "pierre.fontaine@techmaint.fr",
                Phone = "0612345603",
                UserName = "pfontaine",
                RoleId = Role_Technicien,
                Status = StaffStatus.Active,
                IsPdaActive = true,
                Address = new Address("78 Rue Victor Hugo", "Boulogne-Billancourt", "92100", "France")
            },
            new()
            {
                Id = Staff_Technicien_Electricien_2,
                EmployeeNumber = "TECH004",
                FirstName = "David",
                LastName = "SIMON",
                Email = "david.simon@techmaint.fr",
                Phone = "0612345604",
                UserName = "dsimon",
                RoleId = Role_Technicien,
                Status = StaffStatus.Active,
                IsPdaActive = true,
                Address = new Address("34 Rue Gambetta", "Vincennes", "94300", "France")
            },
            new()
            {
                Id = Staff_Technicien_CVC,
                EmployeeNumber = "TECH005",
                FirstName = "Stéphane",
                LastName = "MERCIER",
                Email = "stephane.mercier@techmaint.fr",
                Phone = "0612345605",
                UserName = "smercier",
                RoleId = Role_Technicien,
                Status = StaffStatus.Active,
                IsPdaActive = true,
                Address = new Address("12 Avenue de Paris", "Versailles", "78000", "France")
            },
            new()
            {
                Id = Staff_Technicien_MultiTechnique_1,
                EmployeeNumber = "TECH006",
                FirstName = "Olivier",
                LastName = "ROUX",
                Email = "olivier.roux@techmaint.fr",
                Phone = "0612345606",
                UserName = "oroux",
                RoleId = Role_Technicien,
                Status = StaffStatus.Active,
                IsPdaActive = true,
                Address = new Address("89 Boulevard de Strasbourg", "Pantin", "93500", "France")
            },
            new()
            {
                Id = Staff_Technicien_MultiTechnique_2,
                EmployeeNumber = "TECH007",
                FirstName = "François",
                LastName = "CLEMENT",
                Email = "francois.clement@techmaint.fr",
                Phone = "0612345607",
                UserName = "fclement",
                RoleId = Role_Technicien,
                Status = StaffStatus.Active,
                IsPdaActive = true,
                Address = new Address("45 Rue de la Gare", "Créteil", "94000", "France")
            },
            new()
            {
                Id = Staff_Technicien_Serrurerie,
                EmployeeNumber = "TECH008",
                FirstName = "Vincent",
                LastName = "LEFEBVRE",
                Email = "vincent.lefebvre@techmaint.fr",
                Phone = "0612345608",
                UserName = "vlefebvre",
                RoleId = Role_Technicien,
                Status = StaffStatus.Active,
                IsPdaActive = true,
                Address = new Address("67 Rue du Moulin", "Levallois-Perret", "92300", "France")
            },

            // ══════════════════════════════════════════════════════════════════
            // DISPATCHERS
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Staff_Dispatcher_1,
                EmployeeNumber = "DISP001",
                FirstName = "Marie",
                LastName = "LEGRAND",
                Email = "marie.legrand@techmaint.fr",
                Phone = "0145789012",
                UserName = "mlegrand",
                RoleId = Role_Dispatcher,
                Status = StaffStatus.Active,
                Address = new Address("10 Rue de l'Opéra", "Paris", "75001", "France")
            },
            new()
            {
                Id = Staff_Dispatcher_2,
                EmployeeNumber = "DISP002",
                FirstName = "Céline",
                LastName = "FOURNIER",
                Email = "celine.fournier@techmaint.fr",
                Phone = "0145789013",
                UserName = "cfournier",
                RoleId = Role_Dispatcher,
                Status = StaffStatus.Active,
                Address = new Address("10 Rue de l'Opéra", "Paris", "75001", "France")
            },

            // ══════════════════════════════════════════════════════════════════
            // COMPTABILITÉ
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Staff_Comptable,
                EmployeeNumber = "COMPTA001",
                FirstName = "Martine",
                LastName = "ROBERT",
                Email = "martine.robert@techmaint.fr",
                Phone = "0145789014",
                UserName = "mrobert",
                RoleId = Role_Comptable,
                Status = StaffStatus.Active,
                Address = new Address("10 Rue de l'Opéra", "Paris", "75001", "France")
            },

            // ══════════════════════════════════════════════════════════════════
            // ASSISTANTS TECHNIQUES
            // ══════════════════════════════════════════════════════════════════
            new()
            {
                Id = Staff_AssistantTechnique_1,
                EmployeeNumber = "AST001",
                FirstName = "Julie",
                LastName = "GIRARD",
                Email = "julie.girard@techmaint.fr",
                Phone = "0145789015",
                UserName = "jgirard",
                RoleId = Role_AssistantTechnique,
                Status = StaffStatus.Active,
                Address = new Address("10 Rue de l'Opéra", "Paris", "75001", "France")
            },
            new()
            {
                Id = Staff_AssistantTechnique_2,
                EmployeeNumber = "AST002",
                FirstName = "Sandrine",
                LastName = "MOREL",
                Email = "sandrine.morel@techmaint.fr",
                Phone = "0145789016",
                UserName = "smorel",
                RoleId = Role_AssistantTechnique,
                Status = StaffStatus.Active,
                Address = new Address("10 Rue de l'Opéra", "Paris", "75001", "France")
            },
        };
    }

    private static async Task CreateStaffMemberAsync(
        AppDbContext context,
        UserManager<ApplicationUser> userManager,
        Guid tenantId,
        StaffDefinition def)
    {
        // Vérifier si le staff existe déjà
        var existingStaff = await context.Staffs.AnyAsync(s => s.Id == def.Id);
        if (existingStaff)
            return;

        // Créer l'ApplicationUser si nécessaire
        var user = await userManager.FindByEmailAsync(def.Email);
        if (user == null)
        {
            user = new ApplicationUser
            {
                Id = def.Id,
                UserName = def.UserName,
                Email = def.Email,
                EmailConfirmed = true,
                IsActive = def.Status == StaffStatus.Active
            };
            await userManager.CreateAsync(user, "Password@123");
        }

        // Créer le Staff
        var staff = new Staff
        {
            Id = def.Id,
            TenantId = tenantId,
            EmployeeNumber = def.EmployeeNumber,
            FirstName = def.FirstName,
            LastName = def.LastName,
            Email = def.Email,
            CellPhone = def.Phone,
            Status = def.Status,
            Address = def.Address,
            IsPdaActive = def.IsPdaActive,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = Guid.Empty
        };

        await context.Staffs.AddAsync(staff);
        await context.SaveChangesAsync();

        // Créer le TenantUser (liaison staff-tenant-role)
        var tenantUserExists = await context.TenantUsers.AnyAsync(tu =>
            tu.UserId == def.Id && tu.TenantId == tenantId);

        if (!tenantUserExists)
        {
            var tenantUser = new TenantUser(tenantId, def.Id, def.RoleId, def.Id);
            context.TenantUsers.Add(tenantUser);
            await context.SaveChangesAsync();
        }
    }

    #endregion

    #region Helper Classes

    private class StaffDefinition
    {
        public Guid Id { get; set; }
        public string EmployeeNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public Guid RoleId { get; set; }
        public StaffStatus Status { get; set; } = StaffStatus.Active;
        public Address Address { get; set; } = new Address("", "", "", "France");
        public bool IsPdaActive { get; set; } = false;
    }

    #endregion
}
