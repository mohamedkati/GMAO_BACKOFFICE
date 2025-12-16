using GMAO.Application.Common.Authorization;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Customers.Commands.UpdateBudget
{
    public class UpdateBudgetCommand : IRequest<ResponseResult<bool>>, IRequiredPermission
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public decimal BudgetedAmount { get; set; }
        public decimal CommittedAmount { get; set; }
        public decimal InvoicedAmount { get; set; }
        public decimal AlertThreshold { get; set; } = 80m;
        public bool AlertSent { get; set; }

        public string[] RequiredPermissions => [PermissionConfig.CombineResourceAction(Resource.CustomerBudgets, StandardAction.Edit)];
    }
}
