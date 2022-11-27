using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAccountService
    {
        Task RegisterAsync(RegisterDTO userData);
        Task<LoginResponse> LoginAsync(string login, string password);
        Task LogoutAsync();
        DownloadFileDTO GetImage(string userId);
        Task RequestResetPassword(string userEmail);
        Task ResetPassword(ResetPasswordDTO resetPasswordDTO);


    }
}
