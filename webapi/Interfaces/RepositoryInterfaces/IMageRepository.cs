using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webapi.DataLayer.Models;

namespace webapi.Interfaces.RepositoryInterfaces
{
    public interface IMageRepository : IBaseRepository<Mage>
    {
        Task<Mage> GetMageByType(string type);
        
    }
}