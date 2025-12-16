using GMAO.Domain.Entities.Auth;

namespace GMAO.Domain.Authorization
{
    /// <summary>
    /// Configuration centralisée de toutes les permissions du système
    /// </summary>
    public static class PermissionConfig
    {
        // ===== HELPER METHODS =====

        /// <summary>
        /// Convertit une action standard en string lowercase
        /// </summary>
        private static string S(StandardAction action) => action.ToString().ToLowerInvariant();

        /// <summary>
        /// Crée un objet Permission
        /// </summary>
        private static Permission P(Resource resource, string action) =>
            new Permission(resource.ToString().ToLowerInvariant(), action);

        // ===== CONFIGURATION DES PERMISSIONS =====

        /// <summary>
        /// Configuration complète : Standard + Spécifique par ressource
        /// </summary>
        public static readonly Dictionary<Resource, List<string>> ResourceActions = new()
        {
            // ===== CUSTOMERS =====
            {
                Resource.Customers,
                new List<string>
                {
                    // Standard CRUD
                    S(StandardAction.View),
                    S(StandardAction.ViewDetails),
                    S(StandardAction.Create),
                    S(StandardAction.Edit),
                    S(StandardAction.Delete),
                    S(StandardAction.Export),
                    // Spécifique métier
                    ResourceSpecificActions.Customers.Merge,
                    ResourceSpecificActions.Customers.Archive,
                    ResourceSpecificActions.Customers.Restore,
                    ResourceSpecificActions.Customers.ChangeCommercial,
                }
            },

            // ===== PROPERTY GROUPS =====
            {
                Resource.PropertyGroups,
                new List<string>
                {
                    // Standard CRUD uniquement
                    S(StandardAction.View),
                    S(StandardAction.ViewDetails),
                    S(StandardAction.Create),
                    S(StandardAction.Edit),
                    S(StandardAction.Delete),
                }
            },

            // ===== CONTACTS =====
            {
                Resource.CustomerContacts,
                new List<string>
                {
                    S(StandardAction.View),
                    S(StandardAction.Create),
                    S(StandardAction.Edit),
                    S(StandardAction.Delete),
                }
            },

            // ===== BUDGETS =====
            {
                Resource.CustomerBudgets,
                new List<string>
                {
                    // Standard
                    S(StandardAction.View),
                    S(StandardAction.Create),
                    S(StandardAction.Edit),
                    S(StandardAction.Delete),
                    // Spécifique
                    ResourceSpecificActions.Budgets.Validate,
                    ResourceSpecificActions.Budgets.Lock,
                    ResourceSpecificActions.Budgets.Unlock,
                    ResourceSpecificActions.Budgets.SendAlert,
                }
            },

            // ===== WORK ORDERS =====
            {
                Resource.WorkOrders,
                new List<string>
                {
                    // Standard
                    S(StandardAction.View),
                    S(StandardAction.ViewDetails),
                    S(StandardAction.Create),
                    S(StandardAction.Edit),
                    S(StandardAction.Delete),
                    S(StandardAction.Export),
                    // Spécifique (workflow)
                    ResourceSpecificActions.WorkOrders.Assign,
                    ResourceSpecificActions.WorkOrders.Cancel,
                    ResourceSpecificActions.WorkOrders.Complete,
                    ResourceSpecificActions.WorkOrders.Reopen,
                    ResourceSpecificActions.WorkOrders.Schedule,
                    ResourceSpecificActions.WorkOrders.ValidateTechnician,
                }
            },

            // ===== INVOICES =====
            {
                Resource.Invoices,
                new List<string>
                {
                    // Standard
                    S(StandardAction.View),
                    S(StandardAction.ViewDetails),
                    S(StandardAction.Create),
                    S(StandardAction.Edit),
                    S(StandardAction.Delete),
                    S(StandardAction.Export),
                    // Spécifique (workflow approbation)
                    ResourceSpecificActions.Invoices.Approve,
                    ResourceSpecificActions.Invoices.Reject,
                    ResourceSpecificActions.Invoices.Send,
                    ResourceSpecificActions.Invoices.MarkAsPaid,
                    ResourceSpecificActions.Invoices.GeneratePDF,
                }
            },

            // ===== STAFF =====
            {
                Resource.Staff,
                new List<string>
                {
                    // Standard
                    S(StandardAction.View),
                    S(StandardAction.ViewDetails),
                    S(StandardAction.Create),
                    S(StandardAction.Edit),
                    S(StandardAction.Delete),
                    // Spécifique
                    ResourceSpecificActions.Staff.Activate,
                    ResourceSpecificActions.Staff.Deactivate,
                    ResourceSpecificActions.Staff.ResetPassword,
                    ResourceSpecificActions.Staff.ChangeRole,
                }
            },

            // ===== CONTRACTS =====
            {
                Resource.Contracts,
                new List<string>
                {
                    // Standard
                    S(StandardAction.View),
                    S(StandardAction.ViewDetails),
                    S(StandardAction.Create),
                    S(StandardAction.Edit),
                    S(StandardAction.Delete),
                    S(StandardAction.Export),
                    // Spécifique (lifecycle)
                    ResourceSpecificActions.Contracts.Sign,
                    ResourceSpecificActions.Contracts.Renew,
                    ResourceSpecificActions.Contracts.Terminate,
                    ResourceSpecificActions.Contracts.Suspend,
                }
            },

            // ===== SITES =====
            {
                Resource.Sites,
                new List<string>
                {
                    S(StandardAction.View),
                    S(StandardAction.ViewDetails),
                    S(StandardAction.Create),
                    S(StandardAction.Edit),
                    S(StandardAction.Delete),
                }
            },

            // ===== REPORTS =====
            {
                Resource.Reports,
                new List<string>
                {
                    S(StandardAction.View),
                    S(StandardAction.Export),
                }
            },

            // ===== SETTINGS =====
            {
                Resource.Settings,
                new List<string>
                {
                    S(StandardAction.View),
                    S(StandardAction.Edit),
                }
            },
        };

        // ===== MÉTHODES PUBLIQUES =====

        /// <summary>
        /// Génère toutes les permissions disponibles dans le système
        /// </summary>
        public static List<Permission> GetAllPermissions()
        {
            var permissions = new List<Permission>();

            foreach (var (resource, actions) in ResourceActions)
            {
                foreach (var action in actions)
                {
                    permissions.Add(new Permission(resource.ToString().ToLowerInvariant(), action));
                }
            }

            return permissions;
        }

        /// <summary>
        /// Vérifie si une action est disponible pour une ressource
        /// </summary>
        public static bool IsActionAvailable(Resource resource, string action)
        {
            return ResourceActions.TryGetValue(resource, out var actions) &&
                   actions.Contains(action.ToLowerInvariant());
        }

        /// <summary>
        /// Obtient toutes les actions disponibles pour une ressource
        /// </summary>
        public static List<string> GetActionsForResource(Resource resource)
        {
            return ResourceActions.TryGetValue(resource, out var actions)
                ? new List<string>(actions)
                : new List<string>();
        }

        /// <summary>
        /// Vérifie si une action est une action standard (CRUD)
        /// </summary>
        public static bool IsStandardAction(string action)
        {
            return Enum.TryParse<StandardAction>(action, true, out _);
        }

        /// <summary>
        /// Vérifie si une action est considérée comme dangereuse
        /// </summary>
        public static bool IsDangerousAction(string action)
        {
            var dangerousActions = new HashSet<string>
            {
                "delete", "cancel", "terminate", "deactivate",
                "reject", "lock", "suspend"
            };
            return dangerousActions.Contains(action.ToLowerInvariant());
        }

        /// <summary>
        /// Obtient le label d'affichage pour une action
        /// </summary>
        public static string GetActionLabel(string action)
        {
            return action.ToLowerInvariant() switch
            {
                // Standard
                "view" => "Voir",
                "viewdetails" => "Voir détail",
                "create" => "Créer",
                "edit" => "Modifier",
                "delete" => "Supprimer",
                "export" => "Exporter",
                "import" => "Importer",

                // Work Orders
                "assign" => "Assigner",
                "cancel" => "Annuler",
                "complete" => "Compléter",
                "reopen" => "Réouvrir",
                "schedule" => "Planifier",
                "validate_technician" => "Valider le technicien",

                // Invoices
                "approve" => "Approuver",
                "reject" => "Rejeter",
                "send" => "Envoyer",
                "mark_as_paid" => "Marquer comme payé",
                "generate_pdf" => "Générer PDF",

                // Customers
                "merge" => "Fusionner",
                "archive" => "Archiver",
                "restore" => "Restaurer",
                "change_commercial" => "Changer de commercial",

                // Budgets
                "validate" => "Valider",
                "lock" => "Verrouiller",
                "unlock" => "Déverrouiller",
                "send_alert" => "Envoyer une alerte",

                // Contracts
                "sign" => "Signer",
                "renew" => "Renouveler",
                "terminate" => "Résilier",
                "suspend" => "Suspendre",

                // Staff
                "activate" => "Activer",
                "deactivate" => "Désactiver",
                "reset_password" => "Réinitialiser le mot de passe",
                "change_role" => "Changer le rôle",

                _ => action
            };
        }

        /// <summary>
        /// Obtient le label d'affichage pour une ressource
        /// </summary>
        public static string GetResourceLabel(Resource resource)
        {
            return resource switch
            {
                Resource.Customers => "Clients",
                Resource.PropertyGroups => "Groupes immobiliers",
                Resource.CustomerContacts => "Contacts",
                Resource.CustomerBudgets => "Budgets",
                Resource.Staff => "Personnel",
                Resource.Reports => "Rapports",
                Resource.Settings => "Paramètres",
                Resource.WorkOrders => "Bons de travail",
                Resource.Invoices => "Factures",
                Resource.Sites => "Sites",
                Resource.Contracts => "Contrats",
                _ => resource.ToString()
            };
        }

        /// <summary>
        /// Obtient le label complet d'une permission
        /// </summary>
        public static string GetPermissionLabel(Permission permission)
        {
            //var resourceLabel = GetResourceLabel(permission.Resource);
            var resourceLabel = permission.Resource;
            var actionLabel = GetActionLabel(permission.Action);
            return $"{actionLabel} {resourceLabel.ToLower()}";
        }

        /// <summary>
        /// Obtient la description d'une permission
        /// </summary>
        public static string GetPermissionDescription(Permission permission)
        {
            return $"Permet de {GetPermissionLabel(permission).ToLower()}";
        }

        public static string CombineResourceAction(Resource resource, string action)
        {
            return $"{resource.ToString().ToLowerInvariant()}:{action.ToLowerInvariant()}";
        }

        public static string CombineResourceAction(string resource, string action)
        {
            return $"{resource.ToLowerInvariant()}:{action.ToLowerInvariant()}";
        }

        public static string CombineResourceAction(Resource resource, StandardAction action)
        {
            return $"{resource.ToString().ToLowerInvariant()}:{S(action)}";
        }
    }
}