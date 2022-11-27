using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class TextService : ITextService
    {
        private readonly IRepository<AppText> _textRepository;
        private readonly IMapper _mapper;

        public TextService(IRepository<AppText> textRepository, IMapper mapper)
        {
            this._textRepository = textRepository;
            this._mapper = mapper;
        }


        public async Task DeleteTextByIdAsync(int id)
        {
            var text = await _textRepository.GetByIdAsync(id);

            if (text == null) throw new HttpException(HttpStatusCode.NotFound, ErrorMessages.NotFound);


            await _textRepository.DeleteByIdAsync(id);
            await _textRepository.SaveChanges();

        }

        public async Task<AppTextDTO> GetAllAsync()
        {
            return _mapper.Map<AppTextDTO>(await _textRepository.GetAllAsync());
        }

 

        public async Task<AppTextDTO> GetTextByIdAsync(int id)
        {
            var text = await _textRepository.GetByIdAsync(id);

            if (text == null) throw new HttpException(HttpStatusCode.NotFound, ErrorMessages.NotFound);

            return  _mapper.Map<AppTextDTO>(await _textRepository.GetByIdAsync(id));
        }

       

        public async Task InsertTextAsync(AppTextDTO text)
        {
            await _textRepository.InsertAsync(_mapper.Map<AppText>(text));
            await _textRepository.SaveChanges();
        }

        public async Task UpdateTextAsync(AppTextDTO text)
        {
            var user = await _textRepository.GetByIdAsync(text.Id);

            if (user == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, ErrorMessages.NotFound);
            }

            await _textRepository.UpdateAsync(_mapper.Map<AppText>(text));
            await _textRepository.SaveChanges();

        }
    }
}
