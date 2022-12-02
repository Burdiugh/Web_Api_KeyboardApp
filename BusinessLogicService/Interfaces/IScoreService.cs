using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IScoreService
    {
        Task<IEnumerable<ScoreDTO>> GetAllAsync();
        Task<ScoreDTO> GetScoreByIdAsync(int id);
        Task DeleteByIdAsync(int id);
        Task InsertScoreAsync(ScoreDTO score);

        Task<IEnumerable<ScoreDTO>> GetAllByUserId(string id);



    }
}
