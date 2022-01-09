using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Cryptography;
using webapi.DataLayer.Models;
using webapi.Interfaces;
using webapi.Interfaces.ServiceInterfaces;

namespace webapi.Services
{
    public class TerrainService : ITerrainService
    {
        private readonly IUnitOfWork unitOfWork;

        public TerrainService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Terrain> CreateTerrain(Terrain terrain)
        {
            using (unitOfWork)
            {
                terrain.Games = new List<Game>();
                unitOfWork.TerrainRepository.Create(terrain);
                await unitOfWork.CompleteAsync();

                return terrain;
            }
        }

        public async Task<Terrain> GetTerrainByType(string type)
        {
            using (unitOfWork)
            {
                Terrain terrain = await unitOfWork.TerrainRepository.GetTerrainByType(type);
                return terrain;
            }
        }

        public async Task<Terrain> GetTerrainByID(int terrainID)
        {
            using (unitOfWork)
            {
                Terrain terrain = await unitOfWork.TerrainRepository.GetById(terrainID);
                return terrain;
            }
        }
    }
}