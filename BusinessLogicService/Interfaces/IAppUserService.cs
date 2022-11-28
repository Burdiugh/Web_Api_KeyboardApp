
using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using WebAPI_ASP.DTOs;

namespace Core.Interfaces
{
    public interface IAppUserService
    {
        Task<IEnumerable<AppUserDTO>> GetAllAsync();
        Task<AppUserDTO> GetByIdAsync(string id);
        Task DeleteByIdAsync(string id);
        Task UpdateAsync(AppUserDTO entity);
        Task InsertAsync(AppUserDTO entity);
       // Task AddScore(ScoreDTO score);
    }
}
