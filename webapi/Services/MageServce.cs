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
    public class MageService : IMageService
    {
        private readonly IUnitOfWork unitOfWork;

        public MageService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Mage> CreateMage(Mage mage)
        {
            using (unitOfWork)
            {
                unitOfWork.MageRepository.Create(mage);
                await unitOfWork.CompleteAsync();

                return mage;
            }
        }
        public async Task<Mage> GetMageByType(string type)
        {
            using (unitOfWork)
            {
                Mage mage = await this.unitOfWork.MageRepository.GetMageByType(type);
                return mage;
            }
        }

        public async Task<Mage> GetMageByID(int mageID)
        {
            using (unitOfWork)
            {
                Mage mage = await this.unitOfWork.MageRepository.GetById(mageID);
                return mage;
            }
        }
    
    }
}