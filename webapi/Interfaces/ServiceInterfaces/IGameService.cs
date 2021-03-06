using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webapi.DataLayer.Models;

namespace webapi.Interfaces.ServiceInterfaces
{
    public interface IGameService
    {
        Task<Game> CreateGame(Game game, string terrainType, int userID, string mageType, int numOfSpellCards, int numOfAttackCards, int numOfBuffCards);
        Task<Game> AddUserToGame(int gameID, int userID, string mageType, int numOfSpellCards, int numbOfAttackCards, int numOfBuffCards);
        Task<Game> RemoveUserFromGame(int gameID, int userID);
        Task<IEnumerable<Game>> GetAllGames();
        Task<Game> GetGameByID(int gameID);
        Task<string> GetGameTerrainType(int gameID);
        Task<Game> SetWinnerUserID(int gameID, int userID);
        Task<Game> Turn(int gameID, int turnUserID, int attackedUserID, int nextUserID, int cardID);
        Task<bool> SendInvite(int gameID, string username, string tag, int userFrom);
        Task<Game> JoinRandomGame(int userID, string mageType, int numOfSpellCards, int numOfAttackCards, int numOfBuffCards);
        Task<Game> SkipTurn(int gameID, int turnUserID, int nextUserID);
    }
}