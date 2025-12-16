using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Authorization
{
    public enum StandardAction
    {
        View,
        ViewDetails,
        Create,
        Edit,
        Delete,
        Export,
        Import
    }

    public enum Resource
    {
        Customers,
        PropertyGroups,
        CustomerContacts,
        CustomerBudgets,
        Staff,
        Reports,
        Settings,
        WorkOrders,
        Invoices,
        Sites,
        Contracts
    }
}
