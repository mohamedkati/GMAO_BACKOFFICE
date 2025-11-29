using GMAO.Application.Common.Interfaces.Infrastructure;
using GMAO.Application.Common.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Behaviours
{
    /// <summary>
    /// A pipeline behavior that manages transactions for commands implementing ITransactionnalCommand.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class TransactionnalBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionnalBehaviour(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is not ITransactionnalCommand)
            {
                return await next();
            }

            try
            {
                await _unitOfWork.StartTransactionAsync();
                var response = await next();
                await _unitOfWork.CommitTransactionAsync();
                return response;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
