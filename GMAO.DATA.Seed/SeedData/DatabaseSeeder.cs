using GMAO.Infrastructure.Persistance;
using GMAO.Infrastructure.Persistance.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GMAO.DATA.Seed.SeedData;

/// <summary>
/// Orchestrateur principal pour le seeding de la base de données GMAO
/// Appelle tous les seeders dans l'ordre correct des dépendances
/// </summary>
public static class DatabaseSeeder
{
    /// <summary>
    /// Seed complet de la base de données avec des données métier GMAO réalistes
    /// </summary>
    /// <param name="serviceProvider">Le service provider pour résoudre les dépendances</param>
    /// <param name="tenantId">L'ID du tenant (optionnel, utilise le tenant par défaut si non fourni)</param>
    public static async Task SeedAsync(IServiceProvider serviceProvider, Guid? tenantId = null)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Utiliser le tenant par défaut si non fourni
        var effectiveTenantId = tenantId ?? await GetDefaultTenantIdAsync(context);

        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║           GMAO DATABASE SEEDER - Données Métier              ║");
        Console.WriteLine("╠══════════════════════════════════════════════════════════════╣");
        Console.WriteLine($"║  Tenant ID: {effectiveTenantId}             ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.WriteLine();

        try
        {
            // ══════════════════════════════════════════════════════════════════
            // ÉTAPE 1: Données de référence (aucune dépendance)
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("📚 [1/5] Seeding des données de référence...");
            await ReferenceDataSeeder.SeedAsync(context, effectiveTenantId);
            Console.WriteLine();

            // ══════════════════════════════════════════════════════════════════
            // ÉTAPE 2: Staff et Rôles (dépend de: rien)
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("👥 [2/5] Seeding du personnel et des rôles...");
            await StaffAndRolesSeeder.SeedAsync(context, userManager, effectiveTenantId);
            Console.WriteLine();

            // ══════════════════════════════════════════════════════════════════
            // ÉTAPE 3: Gestion Clients (dépend de: Staff, ReferenceData)
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("🏢 [3/5] Seeding de la gestion clients...");
            await CustomerManagementSeeder.SeedAsync(context, effectiveTenantId);
            Console.WriteLine();

            // ══════════════════════════════════════════════════════════════════
            // ÉTAPE 4: Gestion Patrimoine (dépend de: Customers, Staff, ReferenceData)
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("🏠 [4/5] Seeding de la gestion du patrimoine...");
            await PropertyManagementSeeder.SeedAsync(context, effectiveTenantId);
            Console.WriteLine();

            // ══════════════════════════════════════════════════════════════════
            // ÉTAPE 5: Gestion des Équipements (dépend de: Sites, ReferenceData)
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("⚙️ [5/5] Seeding de la gestion des équipements...");
            await AssetManagementSeeder.SeedAsync(context, effectiveTenantId);
            Console.WriteLine();

            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              ✅ SEEDING TERMINÉ AVEC SUCCÈS                  ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            
            await PrintSummaryAsync(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              ❌ ERREUR LORS DU SEEDING                       ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.WriteLine($"Erreur: {ex.Message}");
            Console.WriteLine($"Stack: {ex.StackTrace}");
            throw;
        }
    }

    /// <summary>
    /// Récupère l'ID du tenant par défaut
    /// </summary>
    private static async Task<Guid> GetDefaultTenantIdAsync(AppDbContext context)
    {
        var tenant = await context.Tenants.FirstOrDefaultAsync();
        if (tenant == null)
        {
            throw new InvalidOperationException(
                "Aucun tenant trouvé dans la base de données. " +
                "Veuillez d'abord exécuter le DbInitializer pour créer le tenant par défaut.");
        }
        return tenant.Id;
    }

    /// <summary>
    /// Affiche un résumé des données créées
    /// </summary>
    private static async Task PrintSummaryAsync(AppDbContext context)
    {
        Console.WriteLine();
        Console.WriteLine("📊 RÉSUMÉ DES DONNÉES CRÉÉES:");
        Console.WriteLine("─────────────────────────────────────────────────");
        
        // Référentiels
        Console.WriteLine("📚 Référentiels:");
        Console.WriteLine($"   • SectorTypes:      {await context.Set<GMAO.Domain.Entities.siteAggregate.SectorType>().CountAsync()}");
        Console.WriteLine($"   • SiteCategories:   {await context.Set<GMAO.Domain.Entities.siteAggregate.SiteCategory>().CountAsync()}");
        Console.WriteLine($"   • SiteClientTypes:  {await context.Set<GMAO.Domain.Entities.siteAggregate.SiteClientType>().CountAsync()}");
        Console.WriteLine($"   • ContactTypes:     {await context.Set<GMAO.Domain.Entities.ContactType>().CountAsync()}");
        Console.WriteLine($"   • TVA:              {await context.Set<GMAO.Domain.Entities.TVA>().CountAsync()}");
        Console.WriteLine($"   • PaymentMethods:   {await context.Set<GMAO.Domain.Entities.PaymentMethod>().CountAsync()}");
        Console.WriteLine($"   • Skills:           {await context.Set<GMAO.Domain.Entities.Skill>().CountAsync()}");
        
        Console.WriteLine();
        Console.WriteLine("👥 Personnel:");
        Console.WriteLine($"   • Rôles:            {await context.DomainRoles.CountAsync()}");
        Console.WriteLine($"   • Staff:            {await context.Staffs.CountAsync()}");
        
        Console.WriteLine();
        Console.WriteLine("🏢 Gestion Clients:");
        Console.WriteLine($"   • PropertyGroups:   {await context.PropertyGroups.CountAsync()}");
        Console.WriteLine($"   • PG Contacts:      {await context.PropertyGroupsContacts.CountAsync()}");
        Console.WriteLine($"   • Customers:        {await context.Customers.CountAsync()}");
        Console.WriteLine($"   • Customer Contacts:{await context.CustomerContacts.CountAsync()}");
        Console.WriteLine($"   • Maint. Budgets:   {await context.MaintenanceBudgets.CountAsync()}");
        
        Console.WriteLine();
        Console.WriteLine("🏠 Gestion Patrimoine:");
        Console.WriteLine($"   • Sites:            {await context.Sites.CountAsync()}");
        Console.WriteLine($"   • Units:            {await context.Units.CountAsync()}");
        Console.WriteLine($"   • Occupants:        {await context.Occupants.CountAsync()}");
        Console.WriteLine($"   • Site Contacts:    {await context.Set<GMAO.Domain.Entities.siteAggregate.SiteContact>().CountAsync()}");
        Console.WriteLine($"   • Site Keepers:     {await context.Set<GMAO.Domain.Entities.siteAggregate.SiteKeeper>().CountAsync()}");
        
        Console.WriteLine();
        Console.WriteLine("⚙️ Gestion Équipements:");
        Console.WriteLine($"   • Asset Categories: {await context.AssetCategories.CountAsync()}");
        Console.WriteLine($"   • Assets:           {await context.Assets.CountAsync()}");
        Console.WriteLine($"   • Maint. Plans:     {await context.MaintenancePlans.CountAsync()}");
        Console.WriteLine($"   • Maint. Tasks:     {await context.Set<GMAO.Domain.Entities.siteAggregate.MaintenanceTask>().CountAsync()}");
        Console.WriteLine($"   • Warranties:       {await context.Set<GMAO.Domain.Entities.siteAggregate.Warranty>().CountAsync()}");
        
        Console.WriteLine("─────────────────────────────────────────────────");
    }

    /// <summary>
    /// Réinitialise toutes les données de seed (pour les tests)
    /// ATTENTION: Supprime toutes les données!
    /// </summary>
    public static async Task ResetAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        Console.WriteLine("⚠️ ATTENTION: Réinitialisation de la base de données...");
        Console.WriteLine("   Cette opération va supprimer toutes les données de seed.");
        
        // Supprimer dans l'ordre inverse des dépendances
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Warranties");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Maintenance_Tasks");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Maintenance_Plans");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Assets");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Asset_Categories");
        
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Site_Keepers");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Site_Contacts");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Occupants");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Units");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Sites");
        
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Maintenance_Budgets");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Customer_Contacts");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Customers");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Property_Group_Contacts");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Property_Groups");
        
        //await context.Database.ExecuteSqlRawAsync("DELETE FROM TenantUsers WHERE UserId IN (SELECT Id FROM Staffs)");
        //await context.Database.ExecuteSqlRawAsync("DELETE FROM Staffs");
        // Ne pas supprimer les rôles système
        
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Skills");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Payment_Methods");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM vats");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM ContactType");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Site_Client_Types");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Site_Categories");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Sector_Types");

        Console.WriteLine("✅ Base de données réinitialisée.");
    }
}
