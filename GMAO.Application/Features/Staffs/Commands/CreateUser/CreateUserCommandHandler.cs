using GMAO.Application.Common.Exceptions;
using GMAO.Application.Common.Interfaces.Authentication;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Common.Interfaces.Services;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities.Auth;
using MediatR;
using StaffEntity = GMAO.Domain.Entities.Staff;
namespace GMAO.Application.Features.Staffs.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseResult<Guid>>
    {
        private readonly IAccountService _accountService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<StaffEntity> _staffRepository;
        private readonly IRepository<TenantUser> _tenantUserRepository;
        private readonly IAuthenticatedUser _authenticatedUser;

        public CreateUserCommandHandler(IAccountService accountService,
            IUnitOfWork unitOfWork,
            IRepository<StaffEntity> staffRepository,
            IRepository<TenantUser> tenantUserRepository,
            IAuthenticatedUser authenticatedUser)
        {
            _accountService = accountService;
            _unitOfWork = unitOfWork;
            _staffRepository = staffRepository;
            _tenantUserRepository = tenantUserRepository;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<ResponseResult<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var password = GenerateNewRandomPassword(12);
                //await _unitOfWork.StartTransactionAsync();
                var userId = await _accountService.RegisterAsync(request.UserName, request.PhoneNumber, request.Email, password);

                // Assign Role to User TODO.

                var staff = new StaffEntity(userId, _authenticatedUser.TenantId, request.FirstName, request.LastName, request.Email, request.PhoneNumber, request.UserName, request.RoleId, password);
                var tenantUser = new TenantUser(_authenticatedUser.TenantId, userId, request.RoleId, userId);
                await _staffRepository.AddAsync(staff);
                await _tenantUserRepository.AddAsync(tenantUser);
                //await _unitOfWork.CommitTransactionAsync();
                return ResponseResult<Guid>.OkResult(userId);
            }
            catch (AppValidationException)
            {
                //await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                //await _unitOfWork.RollbackTransactionAsync();
                throw new ApiException("An error occurred while creating the user.", ex);
            }

        }

        private string GenerateNewRandomPassword(int length)
        {
            string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string LowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
            string Numbers = "0123456789";
            string SpecialCharacters = "!@#$%^&*()-_=+[]{}|;:,.<>?/";
            Random random = new Random();

            // Ensure the password contains at least one of each required character type
            char[] password = new char[length];
            password[0] = UppercaseLetters[random.Next(UppercaseLetters.Length)];
            password[1] = LowercaseLetters[random.Next(LowercaseLetters.Length)];
            password[2] = Numbers[random.Next(Numbers.Length)];
            password[3] = SpecialCharacters[random.Next(SpecialCharacters.Length)];

            // Fill the remaining characters with a random mix of all character sets
            string allCharacters = UppercaseLetters + LowercaseLetters + Numbers + SpecialCharacters;
            for (int i = 4; i < length; i++)
            {
                password[i] = allCharacters[random.Next(allCharacters.Length)];
            }

            // Shuffle the password to randomize the order
            return new string(password.OrderBy(x => random.Next()).ToArray());
        }
    }
}
