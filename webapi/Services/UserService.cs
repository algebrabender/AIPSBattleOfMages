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
    public class UserService : IUserService
    {
        #region HelpAttributes
        private const string alphaUppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string alphaLowercase = "abcdefghijklmnopqrstuvwxyz";
        private Random random;
        #endregion

        #region HelpMethodes
        private char GetRandomUppercase()
        {
            return alphaUppercase[random.Next(0, 26)];
        }
        private char GetRandomLowercase()
        {
            return alphaLowercase[random.Next(0, 26)];
        }
        private string SALTGenerator()
        {
            string salt = "";

            for (int i =0; i < 12; i++) //neka za sad bude po 11 random karaktera
            {
                if(i%3 == 0)
                    salt += GetRandomUppercase();
                else if (i%3 == 1)
                    salt += GetRandomLowercase();
                else
                    salt += random.Next(0, 10).ToString();
            }
            
            return salt;
        }
        private string PasswordHashing(string password)
        {
            using(SHA256 sha256 = SHA256.Create())
            {
                var hashedPassword = new System.Text.StringBuilder();
                var bytePassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                foreach (var b in bytePassword)
                {
                    hashedPassword.Append(b.ToString("x2"));
                }

                return hashedPassword.ToString();
            }
        }
        #endregion
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.random = new Random();
        }

        public async Task<User> CreateUser(User user)
        {
            using (unitOfWork)
            {
                user.Salt = this.SALTGenerator();
                string passAndSalt = user.Password + user.Salt;
                user.Password = this.PasswordHashing(passAndSalt);

                int minNum = 0;
                int maxNum = 9999;

                String generatedTag = random.Next(minNum, maxNum).ToString("D4");
            
                //TODO: SREDITI
                // while (this.usedTags.Contains(generatedTag))
                // {
                //     //TODO: CONTEXT -> SERVICE
                //     var sameTagUser = (User)Context.Users.Where(u => u.Tag == generatedTag);
                
                //     if(sameTagUser.Username == user.Username)
                //         generatedTag = random.Next(minNum, maxNum).ToString("D4");
                // }

                // usedTags.Add(generatedTag);
            
                user.Tag = generatedTag;

                /*await*/ unitOfWork.UserRepository.Create(user);
                await unitOfWork.CompleteAsync();

                user.Salt = null;
                user.Password = null;

                return user;
            }
        }
        public async Task<User> UserValidating(User user)
        {
            using (unitOfWork)
            {
                string un = user.Username.Substring(0, user.Username.Length-5); //samo username
                string tag = user.Username.Substring(user.Username.Length-4); //samo tag

                User u = await unitOfWork.UserRepository.GetUserByUsernameAndTag(un, tag);

                if (u != null)
                    return null;
                
                string passAndSalt = user.Password + u.Salt;
                string hashedPassword = this.PasswordHashing(passAndSalt);

                if (user.Password != hashedPassword) //hashedPassword
                    return null;
                else
                {
                    u.Password = null;
                    u.Salt = null;
                    return u;
                }
            }
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            using (unitOfWork)
            {
                IEnumerable<User> users = await unitOfWork.UserRepository.GetAll();

                foreach (var u in users)
                {
                    u.Password = null;
                    u.Salt = null;
                }
                
                return users;
            }
        }
        public async Task<User> GetUserByID(int userID)
        {
            using (unitOfWork)
            {
                User user = await unitOfWork.UserRepository.GetById(userID);

                if (user != null)
                {
                    user.Password = null;
                    user.Salt = null;
                }

                return user;
            }
        }
        public async Task<User> GetUserByUsername(string username)
        {
            using (unitOfWork)
            {
                User user = await unitOfWork.UserRepository.GetUserByUsername(username);

                if (user != null)
                {
                    user.Password = null;
                    user.Salt = null;
                }

                return user;
            }
        }
        public async Task<string> GetUserMageType(int userID)
        {
            using (unitOfWork)
            {
                User user = await unitOfWork.UserRepository.GetById(userID);

                return user.Mage.Type;
            }
        }
        public async Task<int> GetUserGameID(int userID)
        {
            using (unitOfWork)
            {
                User user = await unitOfWork.UserRepository.GetById(userID);

                return user.Game.ID;
            }
        }
    }
}