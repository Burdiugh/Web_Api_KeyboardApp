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

        Task<ScoreDTO> GetScoreByIdAsync(int id);
        Task DeleteByIdAsync(int id);
        Task InsertScoreAsync(ScoreDTO score);



    }
}
