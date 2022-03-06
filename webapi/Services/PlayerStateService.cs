using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using webapi.DataLayer.Models;
using webapi.Interfaces;
using webapi.Interfaces.ServiceInterfaces;

namespace webapi.Services
{
    public class PlayerStateService : IPlayerStateService
    {
        private readonly IUnitOfWork unitOfWork;
        
        public PlayerStateService(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<PlayerState> GetPlayerStateByGameID(int gameID)
        {
            using (unitOfWork)
            {
                PlayerState ps = await unitOfWork.PlayerStateRepository.GetByGameID(gameID);

                return ps;
            }
        }
    }
}