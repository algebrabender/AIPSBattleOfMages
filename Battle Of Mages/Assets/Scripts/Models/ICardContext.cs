using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public interface ICardContext
    {
        void SetStrategy(ICardStrategy cardStrategy);
        void Turn(int gameID, int turnUserID, int attackedUserID, int nextUserID, CardData cardData); //TODO: parametri

    }
}
