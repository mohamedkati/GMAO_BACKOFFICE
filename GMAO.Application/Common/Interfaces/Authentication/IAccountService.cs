using GMAO.Application.Features.Auth.Queries.Login.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Interfaces.Authentication
{
    public interface IAccountService
    {
        Task<Guid> RegisterAsync(string username, string phoneNumber, string email, string password);
        Task<LoggedUserDto> LoginAsync(string email, string password);
        Task<bool> ConfirmUserRegistrationAsync(string code, string userId);
        Task<bool> ForceChangePasswordForFirstTimeAsync(string email, string oldPassword, string newPassword); // here, sure the user must be connected with the old password but he has to change it.
        Task<bool> ChangePasswordAsync(Guid userId, string newPassword, string oldPassword);
        Task RequestForgotPasswordLinkAsync(string email);
        Task<bool> ForgotPasswordAsync(string userId, string token, string newPassword);
        Task<bool> ConfirmPhoneNumberAsync(string userId, string token, string checkNumber);
        Task<bool> ConfirmEmailAsync(string userId, string token);
        Task SendConfirmEmailAfterRegistrationAsync(string email, string password);
        Task RequestEmailConfirmAsync(string email);
    }
}
