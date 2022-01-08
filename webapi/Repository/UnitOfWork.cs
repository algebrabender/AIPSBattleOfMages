using System.Threading.Tasks;
using webapi.DataLayer;
using webapi.Interfaces;
using webapi.Interfaces.RepositoryInterfaces;

namespace webapi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BOMContext context;
        public ICardRepository CardRepository { get; private set; }

        public IDeckRepository DeckRepository { get; private set; }

        public IGameRepository GameRepository { get; private set; }

        public IMageRepository MageRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public ITerrainRepository TerrainRepository { get; private set; }

        public ICardDeckRepository CardDeckRepository { get; private set; }

        public IUserMageGameRepository UserMageGameRepository { get; private set; }

        public UnitOfWork(BOMContext context)
        {
            this.context = context;
            
            CardRepository = new CardRepository(context);
            DeckRepository = new DeckRepository(context);
            MageRepository = new MageRepository(context);
            UserRepository = new UserRepository(context);
            TerrainRepository = new TerrainRepository(context);
            GameRepository = new GameRepository(context);
            CardDeckRepository = new CardDeckRepository(context);
            UserMageGameRepository = new UserMageGameRepository(context);
        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose(); //TODO: pogledati da li treba jos nesto
        }
    }
}