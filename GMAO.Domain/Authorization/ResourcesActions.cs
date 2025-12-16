namespace GMAO.Domain.Authorization
{
    /// <summary>
    /// Actions spécifiques par ressource (non-CRUD)
    /// Organisées en classes statiques pour faciliter l'utilisation
    /// </summary>
    public static class ResourceSpecificActions
    {
        public static class WorkOrders
        {
            public const string Assign = "assign";
            public const string Cancel = "cancel";
            public const string Complete = "complete";
            public const string Reopen = "reopen";
            public const string Schedule = "schedule";
            public const string ValidateTechnician = "validate_technician";
        }

        public static class Invoices
        {
            public const string Approve = "approve";
            public const string Reject = "reject";
            public const string Send = "send";
            public const string MarkAsPaid = "mark_as_paid";
            public const string GeneratePDF = "generate_pdf";
        }

        public static class Customers
        {
            public const string Merge = "merge";
            public const string Archive = "archive";
            public const string Restore = "restore";
            public const string ChangeCommercial = "change_commercial";
        }

        public static class Budgets
        {
            public const string Validate = "validate";
            public const string Lock = "lock";
            public const string Unlock = "unlock";
            public const string SendAlert = "send_alert";
        }

        public static class Contracts
        {
            public const string Sign = "sign";
            public const string Renew = "renew";
            public const string Terminate = "terminate";
            public const string Suspend = "suspend";
        }

        public static class Staff
        {
            public const string Activate = "activate";
            public const string Deactivate = "deactivate";
            public const string ResetPassword = "reset_password";
            public const string ChangeRole = "change_role";
        }
    }
}