using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class AccountService : IAccountService
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        const string imageFolderName = "images";


        public AccountService(IHostingEnvironment hostingEnvironment, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IRepository<AppUser> _userRepository,IMapper mapper)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
            this._userRepository = _userRepository;
            this.mapper = mapper;
        }

        public async Task<LoginResponse> LoginAsync(string login, string password)
        {
           // throw new NotImplementedException();
            var user = await userManager.FindByEmailAsync(login);

            

            if (user == null || !await userManager.CheckPasswordAsync(user, password))
            {
                throw new HttpException(HttpStatusCode.BadRequest, "Invalid login or password.");
            }

            await signInManager.SignInAsync(user, true);

            return new LoginResponse()
            {
                Token = await GenerateTokenAsync(user)
            };
        }

        private async Task<string> GenerateTokenAsync(AppUser user)
        {
            // create claims
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // generate token
            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            var key = Encoding.ASCII.GetBytes(jwtOptions.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = jwtOptions.Issuer,
                Expires = DateTime.UtcNow.AddHours(jwtOptions.Lifetime), // TODO: not working - fix
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task LogoutAsync()
        {
            //throw new NotImplementedException();
            await signInManager.SignOutAsync();
        }

        public async Task RegisterAsync(RegisterDTO userData)
        {

           var user =  new AppUser
            {
                Email = userData.Email,
                UserName = userData.Email,
               // ImagePath = await SaveImageAsync(userData.Image)
           };

            var result = await userManager.CreateAsync(user, userData.Password);

            if (!result.Succeeded)
            {
                string message = string.Join(' ', result.Errors.Select(e => e.Description));

                throw new HttpException(HttpStatusCode.BadRequest, message);
            }

            if (!await roleManager.RoleExistsAsync("user"))
                await roleManager.CreateAsync(new IdentityRole("user"));

            await userManager.AddToRoleAsync(user, "user");

        }


        private async Task<string> SaveImageAsync(IFormFile file)
        {
            
            string fileName = Guid.NewGuid().ToString();
            string fileExtension = Path.GetExtension(file.FileName);
            string fileFullName = fileName + fileExtension;

            string filePath = Path.Combine(hostingEnvironment.WebRootPath, imageFolderName, fileFullName);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fs);
            }

            return filePath;
        }

        public DownloadFileDTO GetImage(string userId)
        {
            // if (userId < 0) throw new HttpException(HttpStatusCode.BadRequest, ErrorMessages.WrongId);

            var track = _userRepository.GetByIdAsync(userId).Result;

            if (track == null) throw new HttpException(HttpStatusCode.NotFound, ErrorMessages.NotFound);

            if (track.ImagePath == null) throw new HttpException(HttpStatusCode.NoContent, "User has no image file.");

            var fs = new FileStream(track.ImagePath, FileMode.Open);
            var fileName = Path.GetFileName(track.ImagePath);
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out string contentType);

            return new DownloadFileDTO()
            {
                Stream = fs,
                FileName = Path.GetFileName(track.ImagePath),
                ContentType = contentType
            };
        }

        public async Task RequestResetPassword(string userEmail)
        {
            // send email with reset password token

            var user = await userManager.FindByEmailAsync(userEmail);

            if (user == null) throw new HttpException(HttpStatusCode.NotFound, ErrorMessages.NotFound);

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            // create reset password link
            //string resetPasswordLink = "";

            // send token
            // TODO: use separate email service

            string actionResetPasswordUrl = @$"http://localhost:4200/account/app-reset-password";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("prodoq@gmail.com", "bnicxhamzbjbctto");
            smtpClient.EnableSsl = true;

            var tokenParam = new Dictionary<string, string>() { { "token", token }, { "email", userEmail } };

            var resetUrl = new Uri(QueryHelpers.AddQueryString(actionResetPasswordUrl, tokenParam));

            MailMessage message = new MailMessage("prodoq@gmail.com", userEmail)
            {
                Subject = "Reset Password",
                //Body = $"Your reset password token: {token}",
                Body = @$"Your reset password link: {resetUrl}",
                IsBodyHtml = false
            };

            smtpClient.SendAsync(message, null);
        }

        public async Task ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var user = await userManager.FindByEmailAsync(resetPasswordDTO.Email);

            if (user == null)
                throw new HttpException(HttpStatusCode.NotFound, ErrorMessages.NotFound);

            var result = await userManager.ResetPasswordAsync(user, resetPasswordDTO.Token, resetPasswordDTO.NewPassword);

            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(e => e.Description));

                throw new HttpException(HttpStatusCode.InternalServerError, errors);
            }
        }
    }
}
