using System;
using System.Threading.Tasks;
using webapi.Interfaces.RepositoryInterfaces;

namespace webapi.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICardRepository CardRepository { get; }
        IDeckRepository DeckRepository { get; }
        IGameRepository GameRepository { get; }
        IMageRepository MageRepository { get; }
        IUserRepository UserRepository { get; }
        ITerrainRepository TerrainRepository { get; }
        ICardDeckRepository CardDeckRepository { get; }
        IPlayerStateRepository PlayerStateRepository { get; }
        Task CompleteAsync();

    }
}