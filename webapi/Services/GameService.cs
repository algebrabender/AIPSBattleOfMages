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
    public class GameService : IGameService
    {
        private readonly IUnitOfWork unitOfWork;

        public GameService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Game> CreateGame(Game game)
        {
            using(unitOfWork)
            {
                //provera?
                unitOfWork.GameRepository.Create(game);
                await unitOfWork.CompleteAsync();

                var g = await unitOfWork.GameRepository.GetById(game.ID);

                //nesto za player??

                return game;
            }
        }
        public async Task<IEnumerable<Game>> GetAllGames()
        {
            using (unitOfWork)
            {
                IEnumerable<Game> games = await unitOfWork.GameRepository.GetAll();

                return games;
            }
        }
        public async Task<Game> GetGameByID(int gameID)
        {
            Game game = await unitOfWork.GameRepository.GetById(gameID);

            return game;
        }
        public async Task<string> GetGameTerrainType(int gameID)
        {
            Game game = await unitOfWork.GameRepository.GetById(gameID);

            return game.Terrain.Type;
        }
    }
}