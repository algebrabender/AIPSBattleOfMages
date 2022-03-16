using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public interface ICardStrategy
    {
        void Turn(int gameID, int turnUserID, int attackedUserID, int nextUserID, CardData cardData); //TODO: parametri
    }
}
