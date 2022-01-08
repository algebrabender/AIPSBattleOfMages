using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webapi.DataLayer.Models;

namespace webapi.Interfaces.ServiceInterfaces
{
    public interface IMageService
    {
        Task<Mage> CreateMage(Mage mage, int userID);
        Task<Mage> GetMageByType(string type);
        Task<Mage> GetMageByID(int mageID);
    }
}