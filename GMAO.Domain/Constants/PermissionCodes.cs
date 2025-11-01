using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Constants
{
    /// <summary>Facultatif : facilite le seeding et évite les fautes de frappe.</summary>
    public static class PermissionCodes
    {
        // WorkOrders
        public const string WorkOrder_View = "WorkOrder.View";
        public const string WorkOrder_Create = "WorkOrder.Create";
        public const string WorkOrder_Update = "WorkOrder.Update";
        public const string WorkOrder_Assign = "WorkOrder.Assign";
        public const string WorkOrder_Close = "WorkOrder.Close";

        // Quotes
        public const string Quote_View = "Quote.View";
        public const string Quote_Create = "Quote.Create";
        public const string Quote_Send = "Quote.Send";
        public const string Quote_Approve = "Quote.Approve";

        // Inventory / Purchase
        public const string Part_View = "Part.View";
        public const string PurchaseOrder_Create = "PO.Create";
        public const string PurchaseOrder_Receive = "PO.Receive";

        // Admin
        public const string User_Manage = "User.Manage";
        public const string Role_Manage = "Role.Manage";
    }
}
