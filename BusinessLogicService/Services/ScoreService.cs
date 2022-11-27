using Core.DTOs;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using AutoMapper;

namespace Core.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IRepository<AppScore> _scoreRepository;
        private readonly IMapper _mapper;
       private readonly IRepository<AppUser> _userRepository;
        private readonly IAppUserService _userService;


        public ScoreService(IRepository<AppScore> scoreRepository, IMapper mapper, IRepository<AppUser> userRepository, IAppUserService userService)
        {
            this._scoreRepository = scoreRepository;
            this._mapper = mapper; 
            this._userRepository = userRepository;
            this._userService = userService;    
        }


        public async Task DeleteByIdAsync(int id)
        {
            var score = await _scoreRepository.GetByIdAsync(id);

            if (score == null) throw new HttpException(HttpStatusCode.NotFound,ErrorMessages.NotFound);

            await _scoreRepository.DeleteByIdAsync(id);
            await _scoreRepository.SaveChanges();
        }

        public async Task<ScoreDTO> GetScoreByIdAsync(int id)
        {
            var score = await _scoreRepository.GetByIdAsync(id);

            var score2 = score.Score;

            if (score == null) throw new HttpException(HttpStatusCode.NotFound, ErrorMessages.NotFound);

            return _mapper.Map<ScoreDTO>(await _scoreRepository.GetByIdAsync(id));
        }

        public async Task InsertScoreAsync(ScoreDTO score)
        {
            var user = await _userRepository.GetByIdAsync(score.AppUserId);

            if (user==null){ throw new HttpException(HttpStatusCode.NotFound, ErrorMessages.NotFound); }

            _userService.AddScore(score);

            await _scoreRepository.InsertAsync(_mapper.Map<AppScore>(score));
            await _scoreRepository.SaveChanges();
        }
    }
}
