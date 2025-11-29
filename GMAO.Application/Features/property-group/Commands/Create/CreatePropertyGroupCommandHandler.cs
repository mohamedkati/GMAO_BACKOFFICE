using AutoMapper;
using GMAO.Application.Common.Exceptions;
using GMAO.Application.Common.Interfaces;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GMAO.Application.Features.property_group.Commands
{
    public class CreatePropertyGroupCommandHandler : IRequestHandler<CreatePropertyGroupCommand, ResponseResult<Guid>>
    {
        private readonly IPropertyGroupRepository repository;
        private readonly IMapper _mapper;

        public CreatePropertyGroupCommandHandler(IPropertyGroupRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this._mapper = mapper;
        }
        public async Task<ResponseResult<Guid>> Handle(CreatePropertyGroupCommand request, CancellationToken cancellationToken)
        {
            await ValidateProperyGroup(request, cancellationToken);

            var propertyGroup = _mapper.Map<PropertyGroup>(request);
            await repository.AddAsync(propertyGroup, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return ResponseResult<Guid>.OkResult(propertyGroup.Id);
        }

        private async Task ValidateProperyGroup(CreatePropertyGroupCommand request, CancellationToken cancellationToken)
        {
            var nameAlreadyExists = await repository.AnyAsync(x => x.Name.ToLower().Equals(request.Name.ToLower()), cancellationToken);
            if (nameAlreadyExists)
                throw new AppValidationException(errors: new() { { "name", ["Property group already exist"] } });

            var referenceAlreadyExists = await repository.AnyAsync(x => x.Reference.ToLower().Equals(request.Reference.ToLower()), cancellationToken);
            if (referenceAlreadyExists)
                throw new AppValidationException(errors: new() { { "reference", ["Property group reference already exist"] } });
        }
    }
}
