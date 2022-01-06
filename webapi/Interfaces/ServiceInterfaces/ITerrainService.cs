using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webapi.DataLayer.Models;

namespace webapi.Interfaces.ServiceInterfaces
{
    public interface ITerrainService
    {
        Task<Terrain> CreateTerrain(Terrain terrain);
        Task<Terrain> GetTerrainByType(string type);
        Task<Terrain> GetTerrainByID(int terrainID);
    }
}