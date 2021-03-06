using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webapi.DataLayer.Models;

namespace webapi.Interfaces.RepositoryInterfaces
{
    public interface ITerrainRepository : IBaseRepository<Terrain>
    {
        Task<Terrain> GetTerrainByType(string type);
    }
}