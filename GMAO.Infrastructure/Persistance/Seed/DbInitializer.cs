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
            //return; // Disable seeding for now
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

            //await context.Database.MigrateAsync();

            // 1️ Permissions de base
            if (!await context.Permissions.AnyAsync())
            {
                //    var permissions = new Permission[]
                //    {
                //     new() { Id = Guid.NewGuid(), Resource = "customers", Action = "view", Code = "customers:view", DisplayName = "Voir les clients", Category = "standard" },
                //    new() { Id = Guid.NewGuid(), Resource = "customers", Action = "create", Code = "customers:create", DisplayName = "Créer des clients", Category = "standard" },
                //    new() { Id = Guid.NewGuid(), Resource = "customers", Action = "edit", Code = "customers:edit", DisplayName = "Modifier les clients", Category = "standard" },
                //    new() { Id = Guid.NewGuid(), Resource = "customers", Action = "delete", Code = "customers:delete", DisplayName = "Supprimer les clients", Category = "standard", IsDangerous = true,},

                //    // Work Orders
                //    new() { Id = Guid.NewGuid(), Resource = "workorders", Action = "view", Code = "workorders:view", DisplayName = "Voir les bons de travail", Category = "standard" },
                //    new() { Id = Guid.NewGuid(), Resource = "workorders", Action = "create", Code = "workorders:create", DisplayName = "Créer des bons de travail", Category = "standard" },
                //    new() { Id = Guid.NewGuid(), Resource = "workorders", Action = "cancel", Code = "workorders:cancel", DisplayName = "Annuler des bons de travail", Category = "specific", IsDangerous = true },
                //    new() { Id = Guid.NewGuid(), Resource = "workorders", Action = "assign", Code = "workorders:assign", DisplayName = "Assigner des bons de travail", Category = "specific" },

                //};

                //await context.Permissions.AddRangeAsync(permissions);
                //await context.SaveChangesAsync();

                await permissionService.SyncPermissionsFromConfigAsync();
            }

            // 2️ Créer les rôles de base dans le domaine (Tenant)
            if (!await context.DomainRoles.AnyAsync())
            {
                var superAdminRole = new Role
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "SuperAdmin",
                    DisplayName = "Super Administrateur",
                    Description = "Accès complet au système",
                    IsSystem = true,
                    Priority = 100,
                };

                var adminRole = new Role
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Admin",
                    DisplayName = "Administrateur",
                    Description = "Gestion complète sauf paramètres système",
                    IsSystem = true,
                    Priority = 90,
                };

                var managerRole = new Role
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Manager",
                    DisplayName = "Manager",
                    Description = "Gestion opérationnelle et validation",
                    IsSystem = true,
                    Priority = 70,
                };

                await context.DomainRoles.AddRangeAsync(adminRole, managerRole, superAdminRole);
                await context.SaveChangesAsync();

                if (!await context.RolePermissions.AnyAsync(rp => rp.RoleId == adminRole.Id))
                {
                    Permission[] allPermissions = await context.Permissions.ToArrayAsync();
                    foreach (var perm in allPermissions)
                    {
                        if (perm.Resource == "customers") continue;
                        var rp = new RolePermission
                        {
                            RoleId = adminRole.Id,
                            PermissionId = perm.Id
                        };
                        context.RolePermissions.Add(rp);
                    }
                    await context.SaveChangesAsync();
                }
            }

            // 3️ Créer le tenant principal
            if (!await context.Tenants.AnyAsync())
            {
                var tenant = new Tenant
                {
                    Id = Guid.NewGuid(),
                    Name = "TechMaint Services",
                    //Country = "Maroc",
                    //Currency = "MAD"
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
            //var tenantAdminEmail = "mohammed.kati@axeciel.fr";
            //var tenantAdmin = await userManager.FindByEmailAsync(tenantAdminEmail);
            //if (tenantAdmin == null)
            //{
            //    tenantAdmin = new ApplicationUser
            //    {
            //        UserName = "admin",
            //        Email = tenantAdminEmail,
            //        EmailConfirmed = true,
            //        IsActive = true
            //    };

            //    await userManager.CreateAsync(tenantAdmin, "Admin@123");
            //}

            //// 7️ Lier cet utilisateur au tenant avec le rôle Admin
            //var adminRole = await context.DomainRoles.FirstAsync(r => r.Name == "Admin");
            //var staffExist = await context.Staffs.AnyAsync(x => x.Email == tenantAdminEmail);
            //if (!staffExist)
            //{
            //    // changed to Test@demo123
            //    var staff = new Staff(tenantAdmin.Id, demoTenant.Id, "Mohammed", "KATI", tenantAdminEmail, "0641830560", "admin", adminRole.Id, "Admin@123");
            //    await context.Staffs.AddAsync(staff);
            //    await context.SaveChangesAsync();
            //}
            //if (!await context.TenantUsers.AnyAsync(tu =>
            //    tu.UserId == tenantAdmin.Id && tu.TenantId == demoTenant.Id))
            //{
            //    var tu = new TenantUser(demoTenant.Id, tenantAdmin.Id, adminRole.Id, tenantAdmin.Id);
            //    context.TenantUsers.Add(tu);
            //    await context.SaveChangesAsync();
            //}
            await CreateUserWithRole(userManager, context, demoTenant, "mohammed.kati@axeciel.fr", "Admin", "Mohammed", "Kati", "0641830560", "admin");
            await CreateUserWithRole(userManager, context, demoTenant, "semo.katti.7@gmail.com", "Admin", "SEMO", "Kati", "0708105412", "Commercial");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Seeding terminé avec succès !");
            Console.ResetColor();
        }

        private static async Task CreateUserWithRole(UserManager<ApplicationUser> userManager, AppDbContext context, Tenant demoTenant, string userEmail, string userRole, string firstName, string lastName, string phoneNumber, string userName)
        {
            var tenantAdminEmail = userEmail;
            var tenantAdmin = await userManager.FindByEmailAsync(tenantAdminEmail);
            if (tenantAdmin == null)
            {
                tenantAdmin = new ApplicationUser
                {
                    UserName = userName,
                    Email = tenantAdminEmail,
                    EmailConfirmed = true,
                    IsActive = true
                };

                await userManager.CreateAsync(tenantAdmin, "Admin@123");
            }

            // 7️ Lier cet utilisateur au tenant avec le rôle Admin
            var adminRole = await context.DomainRoles.FirstAsync(r => r.Name == userRole);
            var staffExist = await context.Staffs.AnyAsync(x => x.Email == tenantAdminEmail);
            if (!staffExist)
            {
                // changed to Test@demo123
                var staff = new Staff(tenantAdmin.Id, demoTenant.Id, firstName, lastName, tenantAdminEmail, phoneNumber, userName, "Admin@123");
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
        }
    }
}
