using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using System.Net;


namespace Core
{
    public class AppUserService : IAppUserService
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        const string imageFolderName = "images";

        public AppUserService(IRepository<AppUser> _userRepository, IMapper _mapper, UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            this._userRepository = _userRepository;
            this._mapper = _mapper;
            this._userManager = _userManager;
            this._roleManager = _roleManager;
        }


        public async Task<IEnumerable<AppUserDTO>> GetAllAsync()
        {
            // to do roles
            var users  = _mapper.Map<IEnumerable<AppUserDTO>>(await _userRepository.GetAllAsync());

            var roles = new List<string>();
            foreach (var user in users)
            {
                user.Roles = await _userManager.GetRolesAsync(_mapper.Map<AppUser>(await _userRepository.GetByIdAsync(user.Id)));
            }

            return users;
        }

        public async Task<AppUserDTO> GetByIdAsync(string id)
        {

            var user = await _userRepository.GetByIdAsync(id);

            if (user == null) throw new HttpException(HttpStatusCode.NotFound, ErrorMessages.NotFound);

            // to do roles

            return _mapper.Map<AppUserDTO>(await _userRepository.GetByIdAsync(id));
        }

        public async Task InsertAsync(AppUserDTO entity)
        {

           await  _userRepository.InsertAsync(_mapper.Map<AppUser>(entity));
            await _userRepository.SaveChanges();
            
        }

        public async Task UpdateAsync(AppUserDTO entity)
        {
            var user = await _userManager.FindByIdAsync(entity.Id);

            if (user == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, ErrorMessages.NotFound);
            }


            // _userManager.ChangeEmailAsync(user,entity.Email,);
            await _userManager.SetEmailAsync(user, entity.Email);
            await _userManager.SetUserNameAsync(user,entity.UserName);

            // await  _userRepository.UpdateAsync(_mapper.Map<AppUser>(entity));
            // await _userRepository.SaveChanges();
        }

        public async Task DeleteByIdAsync(string id)
        {

           

            var user = await _userRepository.GetByIdAsync(id);

            if (user == null) throw new HttpException(HttpStatusCode.NotFound, ErrorMessages.NotFound);


            await _userRepository.DeleteByIdAsync(id);
            await _userRepository.SaveChanges();
        }

        public async Task  AddScore(ScoreDTO score)
        {
            var user = await _userRepository.GetByIdAsync(score.AppUserId);

            if (user == null) throw new HttpException(HttpStatusCode.NotFound, ErrorMessages.NotFound);

            user.Scores.Add(_mapper.Map<AppScore>(score));
            await _userRepository.SaveChanges();
        }

    }
}
