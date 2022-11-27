using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITextService
    {
        Task<AppTextDTO> GetTextByIdAsync(int id);
        Task<AppTextDTO> GetAllAsync();
        Task DeleteTextByIdAsync(int id);
        Task InsertTextAsync(AppTextDTO text);
        Task UpdateTextAsync(AppTextDTO text);

    }
}
