using GMAO.Domain.Entities.Auth;
using GMAO.Domain.Entities;
using GMAO.Infrastructure.Persistance.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using GMAO.Application.Common.Interfaces.Authentication;

namespace GMAO.Infrastructure.Persistance.Seed
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            //await context.Database.MigrateAsync();

            // 1️ Permissions de base
            if (!await context.Permissions.AnyAsync())
            {
                var permissions = new[]
                {
                new Permission("WorkOrder.View", "Consulter les interventions"),
                new Permission("WorkOrder.Create", "Créer une intervention"),
                new Permission("WorkOrder.Update", "Mettre à jour une intervention"),
                new Permission("WorkOrder.Close", "Clôturer une intervention"),

                new Permission("Quote.View", "Voir les devis"),
                new Permission("Quote.Create", "Créer un devis"),
                new Permission("Quote.Approve", "Approuver un devis"),

                new Permission("PurchaseOrder.Create", "Créer un bon de commande"),
                new Permission("Part.View", "Voir les pièces"),
                new Permission("Part.Edit", "Modifier les pièces"),

                new Permission("User.Manage", "Gérer les utilisateurs du tenant"),
                new Permission("Role.Manage", "Gérer les rôles du tenant"),
            };

                await context.Permissions.AddRangeAsync(permissions);
                await context.SaveChangesAsync();
            }

            // 2️ Créer les rôles de base dans le domaine (Tenant)
            if (!await context.DomainRoles.AnyAsync())
            {
                var allPermissions = await context.Permissions.ToListAsync();

                var admin = new Role("Admin", "Accès complet au tenant");
                foreach (var p in allPermissions) admin.GrantPermission(p);

                var manager = new Role("Manager", "Gère les interventions et devis");
                foreach (var p in allPermissions.Where(x =>
                    x.Code.StartsWith("WorkOrder") ||
                    x.Code.StartsWith("Quote"))) manager.GrantPermission(p);

                var technician = new Role("Technician", "Exécute les interventions");
                foreach (var p in allPermissions.Where(x =>
                    x.Code.StartsWith("WorkOrder"))) technician.GrantPermission(p);

                var client = new Role("Client", "Accède à ses demandes et devis");
                foreach (var p in allPermissions.Where(x => x.Code.Contains("View"))) client.GrantPermission(p);

                await context.DomainRoles.AddRangeAsync(admin, manager, technician, client);
                await context.SaveChangesAsync();
            }

            // 3️ Créer le tenant principal
            if (!await context.Tenants.AnyAsync())
            {
                var tenant = new Tenant
                {
                    Id = Guid.NewGuid(),
                    Name = "TechMaint Services",
                    Country = "Maroc",
                    Currency = "MAD"
                };
                context.Tenants.Add(tenant);
                await context.SaveChangesAsync();
            }

            var demoTenant = await context.Tenants.FirstAsync();

            // 4️ Créer le rôle Identity global SuperAdmin
            if (!await roleManager.RoleExistsAsync("SuperAdmin"))
            {
                var superRole = new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Name = "SuperAdmin",
                    Description = "Accès global SaaS"
                };
                await roleManager.CreateAsync(superRole);
            }

            // 5️ Créer l’utilisateur SuperAdmin global
            var superAdminEmail = "superadmin@techmaint.com";
            var superAdmin = await userManager.FindByEmailAsync(superAdminEmail);
            if (superAdmin == null)
            {
                superAdmin = new ApplicationUser
                {
                    UserName = "superadmin",
                    Email = superAdminEmail,
                    EmailConfirmed = true,
                    IsActive = true
                };

                await userManager.CreateAsync(superAdmin, "Admin@123");
                await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
            }

            // 6️ Créer un utilisateur Admin du tenant
            var tenantAdminEmail = "mohammed.kati@axeciel.fr";
            var tenantAdmin = await userManager.FindByEmailAsync(tenantAdminEmail);
            if (tenantAdmin == null)
            {
                tenantAdmin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = tenantAdminEmail,
                    EmailConfirmed = true,
                    IsActive = true
                };

                await userManager.CreateAsync(tenantAdmin, "Admin@123");
            }

            // 7️ Lier cet utilisateur au tenant avec le rôle Admin
            var adminRole = await context.DomainRoles.FirstAsync(r => r.Name == "Admin");
            var staffExist = await context.Staffs.AnyAsync(x => x.Email == tenantAdminEmail);
            if (!staffExist)
            {
                // changed to Test@demo123
                var staff = new Staff(tenantAdmin.Id, demoTenant.Id, "Mohammed", "KATI", tenantAdminEmail, "0641830560", "admin", adminRole.Id, "Admin@123");
                await context.Staffs.AddAsync(staff);
                await context.SaveChangesAsync();
            }
            if (!await context.TenantUsers.AnyAsync(tu =>
                tu.UserId == tenantAdmin.Id && tu.TenantId == demoTenant.Id))
            {
                var tu = new TenantUser(demoTenant.Id, tenantAdmin.Id, adminRole.Id, tenantAdmin.Id);
                context.TenantUsers.Add(tu);
                await context.SaveChangesAsync();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Seeding terminé avec succès !");
            Console.ResetColor();
        }



    }
}
