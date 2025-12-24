
global using GMAO.Domain.Enums;
namespace GMAO.Domain.Enums
{
    public enum DocumentType
    {
        SitePlan = 1,
        Photo = 2,
        Certificate = 3,
        Contract = 4,
        TechnicalSheet = 5,
        InterventionReport = 6,
        Other = 99
    }
    public enum DueDateType
    {
        Net = 0,
        At = 1,
        EndMonth = 2,
    }
    public enum PropertyGroupType
    {
        PropertyManagementCompany = 1,  // Société de gestion immobilière
        SyndicGroup = 2,                 // Groupe de syndics
        RealEstateInvestor = 3,          // Investisseur immobilier
        PublicHousing = 4,               // Bailleur social
        Corporate = 5,                   // Entreprise (patrimoine immobilier)
        FamilyOffice = 6                 // Family office
    }

    public enum PropertyGroupStatus
    {
        Active = 1,
        Prospect = 2,
        Inactive = 3,
        Suspended = 4,
        Terminated = 5
    }

    public enum LegalForm
    {
        SAS = 1,        // Société par Actions Simplifiée
        SARL = 2,       // Société à Responsabilité Limitée
        SA = 3,         // Société Anonyme
        SCI = 4,        // Société Civile Immobilière
        EURL = 5,       // Entreprise Unipersonnelle à Responsabilité Limitée
        EI = 6,         // Entreprise Individuelle
        Association = 7,
        PublicEntity = 8
    }

    public enum ContactRole
    {
        GeneralManager = 1,          // Directeur Général
        OperationsManager = 2,       // Directeur des Opérations
        CommercialManager = 3,       // Directeur Commercial
        AccountingManager = 4,       // Responsable Comptabilité
        TechnicalManager = 5,        // Responsable Technique
        CustomerServiceManager = 6,  // Responsable Service Client
        LegalManager = 7,            // Responsable Juridique
        Assistant = 8                // Assistant(e)
    }
    // ═══════════════════════════════════════════════════════════════
    // CUSTOMER MANAGEMENT ENUMS
    // ═══════════════════════════════════════════════════════════════

    public enum CustomerType
    {
        PropertyManager = 1,
        Syndic = 2,
        Corporate = 3,
        Individual = 4,
        Government = 5
    }

    public enum PersonType
    {
        Individual = 1,
        Company = 2
    }

    public enum PreferredContactMethod
    {
        Email = 1,
        Phone = 2,
        SMS = 3,
        WhatsApp = 4
    }

    // ═══════════════════════════════════════════════════════════════
    // PROPERTY MANAGEMENT ENUMS
    // ═══════════════════════════════════════════════════════════════

    public enum SiteType
    {
        ResidentialBuilding = 1,
        CommercialBuilding = 2,
        MixedUse = 3,
        IndustrialFacility = 4,
        Office = 5,
        Warehouse = 6,
        RetailStore = 7,
        Hotel = 8,
        Hospital = 9,
        School = 10,
        GovernmentBuilding = 11
    }

    public enum UnitType
    {
        Apartment = 1,
        House = 2,
        Office = 3,
        Store = 4,
        Parking = 5,
        Storage = 6,
        CommonArea = 7
    }

    public enum UnitStatus
    {
        Occupied = 1,
        Vacant = 2,
        UnderRenovation = 3,
        Reserved = 4
    }

    public enum OccupantType
    {
        Owner = 1,
        Tenant = 2,
        Temporary = 3
    }

    // ═══════════════════════════════════════════════════════════════
    // ASSET MANAGEMENT ENUMS
    // ═══════════════════════════════════════════════════════════════

    public enum AssetStatus
    {
        Active = 1,
        Inactive = 2,
        UnderMaintenance = 3,
        Decommissioned = 4,
        Faulty = 5
    }

    public enum CriticalityLevel
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }

    public enum AssetHealthStatus
    {
        Excellent = 1,
        Good = 2,
        Fair = 3,
        Poor = 4,
        Critical = 5
    }

    public enum WarrantyType
    {
        Manufacturer = 1,
        Extended = 2,
        ServiceContract = 3
    }

    public enum MaintenanceFrequency
    {
        Daily = 1,
        Weekly = 2,
        Monthly = 3,
        Quarterly = 4,
        SemiAnnual = 5,
        Annual = 6,
        Biennial = 7,
        Custom = 99
    }

    // ═══════════════════════════════════════════════════════════════
    // SERVICE REQUEST ENUMS
    // ═══════════════════════════════════════════════════════════════

    public enum RequestType
    {
        Maintenance = 1,
        Repair = 2,
        Installation = 3,
        Inspection = 4,
        Emergency = 5,
        Preventive = 6,
        Quote = 7
    }

    public enum ServiceRequestOriginType
    {
        Phone = 1,
        Email = 2,
        WebPortal = 3,
        MobileApp = 4,
        IoTDevice = 5,
        InPerson = 6
    }

    public enum RequestChannel
    {
        Phone = 1,
        Email = 2,
        Portal = 3,
        MobileApp = 4,
        IoT = 5,
        InPerson = 6,
        Fax = 7
    }

    public enum UrgencyLevel
    {
        Low = 1,
        Normal = 2,
        High = 3,
        Critical = 4,
        Emergency = 5
    }

    public enum ServiceRequestStatus
    {
        New = 1,
        Acknowledged = 2,
        InProgress = 3,
        QuoteRequested = 4,
        QuoteSent = 5,
        Approved = 6,
        Completed = 7,
        Cancelled = 8,
        OnHold = 9
    }

    public enum QuoteRequestStatus
    {
        Pending = 1,
        InspectionScheduled = 2,
        InspectionCompleted = 3,
        QuoteCreated = 4,
        Cancelled = 5
    }

    // ═══════════════════════════════════════════════════════════════
    // WORK ORDER ENUMS
    // ═══════════════════════════════════════════════════════════════



    public enum WorkOrderScope
    {
        CommonAreas = 1,
        PrivateUnit = 2,
        Both = 3
    }



    public enum WorkOrderVisibility
    {
        Internal = 1,
        VisibleToClient = 2,
        VisibleToOccupants = 3
    }

    public enum UnitAccessRequirement
    {
        None = 1,
        KeyRequired = 2,
        OccupantPresenceRequired = 3,
        AppointmentRequired = 4
    }

    public enum TimeEntryType
    {
        Regular = 1,
        Overtime = 2,
        Emergency = 3,
        Travel = 4
    }

    public enum CompletionStatus
    {
        FullyCompleted = 1,
        PartiallyCompleted = 2,
        RequiresFollowUp = 3,
        CannotComplete = 4
    }

    // ═══════════════════════════════════════════════════════════════
    // QUOTE ENUMS
    // ═══════════════════════════════════════════════════════════════



    public enum QuoteItemType
    {
        Labor = 1,
        Material = 2,
        Equipment = 3,
        Subcontractor = 4,
        Other = 5
    }

    public enum QuoteAcceptanceType
    {
        Full = 1,
        Partial = 2
    }


    // ═══════════════════════════════════════════════════════════════
    // PURCHASE ORDER ENUMS
    // ═══════════════════════════════════════════════════════════════

    public enum PurchaseOrderSource
    {
        Manual = 1,
        FromQuote = 2,
        FromWorkOrder = 3,
        AutoReorder = 4
    }

    public enum PurchaseOrderStatus
    {
        Draft = 1,
        Sent = 2,
        Acknowledged = 3,
        PartiallyReceived = 4,
        FullyReceived = 5,
        Cancelled = 6,
        Disputed = 7
    }

    public enum ReceiptType
    {
        Full = 1,
        Partial = 2,
        Final = 3
    }

    // ═══════════════════════════════════════════════════════════════
    // SUPPLIER & INVENTORY ENUMS
    // ═══════════════════════════════════════════════════════════════

    public enum SupplierRating
    {
        Excellent = 5,
        Good = 4,
        Average = 3,
        Poor = 2,
        Unacceptable = 1
    }

    public enum StockTransactionType
    {
        Purchase = 1,
        Sale = 2,
        Adjustment = 3,
        Transfer = 4,
        Return = 5,
        Consumption = 6,
        Reservation = 7,
        Release = 8
    }

    // ═══════════════════════════════════════════════════════════════
    // INVOICE ENUMS
    // ═══════════════════════════════════════════════════════════════

    public enum InvoiceStatus
    {
        Draft = 1,
        Sent = 2,
        Viewed = 3,
        PartiallyPaid = 4,
        Paid = 5,
        Overdue = 6,
        Cancelled = 7,
        Disputed = 8
    }

    public enum InvoiceLineType
    {
        Labor = 1,
        Material = 2,
        Equipment = 3,
        Fee = 4,
        Discount = 5,
        Other = 6
    }

    public enum PaymentMethod
    {
        Cash = 1,
        Check = 2,
        BankTransfer = 3,
        CreditCard = 4,
        DebitCard = 5,
        DirectDebit = 6,
        PayPal = 7,
        Other = 99
    }

    // ═══════════════════════════════════════════════════════════════
    // CONTRACT ENUMS
    // ═══════════════════════════════════════════════════════════════

    //public enum ContractType
    //{
    //    FullService = 1,
    //    MaintenanceOnly = 2,
    //    EmergencyOnly = 3,
    //    PreventiveOnly = 4,
    //    TimeAndMaterials = 5,
    //    FixedPrice = 6
    //}

    public enum ContractStatus
    {
        Draft = 1,
        Active = 2,
        Suspended = 3,
        Expired = 4,
        Terminated = 5,
        Renewed = 6
    }

    public enum PricingModel
    {
        FixedMonthly = 1,
        HourlyRate = 2,
        TimeAndMaterials = 3,
        IncludedHours = 4,
        PerCall = 5,
        Hybrid = 6
    }

    // ═══════════════════════════════════════════════════════════════
    // STAFF ENUMS
    // ═══════════════════════════════════════════════════════════════

    public enum StaffRole
    {
        Administrator = 1,
        Manager = 2,
        Dispatcher = 3,
        Technician = 4,
        Accountant = 5,
        CustomerService = 6
    }

    public enum StaffStatus
    {
        Active = 1,
        Inactive = 2,
        OnLeave = 3,
        Suspended = 4,
        Terminated = 5
    }

    public enum TechnicianStatus
    {
        Available = 1,
        OnJob = 2,
        OnBreak = 3,
        Unavailable = 4,
        OffDuty = 5
    }

    public enum SkillLevel
    {
        Beginner = 1,
        Intermediate = 2,
        Advanced = 3,
        Expert = 4,
        Master = 5
    }


    // ===============================================================
    // TENANT MANAGEMENT ENUMS
    // ===============================================================
    public enum SubscriptionPlan
    {
        Free,
        Starter,
        Professional,
        Enterprise
    }
    public enum ThemeMode
    {
        Light,
        Dark,
        System
    }

    public enum SubscriptionStatus
    {
        Active,
        Inactive,
        Cancelled,
        Expired
    }
}
