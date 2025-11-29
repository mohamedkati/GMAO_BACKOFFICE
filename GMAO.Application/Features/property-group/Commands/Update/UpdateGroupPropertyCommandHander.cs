using AutoMapper;
using GMAO.Application.Common.Exceptions;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.property_group.Commands.Update
{
    public class UpdateGroupPropertyCommandHander : IRequestHandler<UpdateGroupPropertyCommand, ResponseResult<bool>>
    {
        private readonly IPropertyGroupRepository _repository;
        private readonly IMapper _mapper;

        public UpdateGroupPropertyCommandHander(IPropertyGroupRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        public async Task<ResponseResult<bool>> Handle(UpdateGroupPropertyCommand request, CancellationToken cancellationToken)
        {
            await ValidateProperyGroup(request, cancellationToken);

            var group = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (group == null)
                return ResponseResult<bool>.FailResult("Property group not found.");

            var groupUpdated = _mapper.Map(request, group);
            _repository.Update(groupUpdated);
            await _repository.SaveChangesAsync(cancellationToken);
            return ResponseResult<bool>.OkResult(true);
        }

        private async Task ValidateProperyGroup(UpdateGroupPropertyCommand request, CancellationToken cancellationToken)
        {
            var nameAlreadyExists = await _repository.AnyAsync(x => x.Name.ToLower().Equals(request.Name.ToLower()) && x.Id != request.Id, cancellationToken);
            if (nameAlreadyExists)
                throw new AppValidationException(errors: new() { { "name", ["Property group already exist"] } });

            var referenceAlreadyExists = await _repository.AnyAsync(x => x.Reference.ToLower().Equals(request.Reference.ToLower())
            && x.Id != request.Id, cancellationToken);
            if (referenceAlreadyExists)
                throw new AppValidationException(errors: new() { { "reference", ["Property group reference already exist"] } });
        }
    }
}
