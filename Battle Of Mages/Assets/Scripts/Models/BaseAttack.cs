using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class BaseAttack : MonoBehaviour, ICardStrategy
    {
        public void Turn(int gameID, int turnUserID, int attackedUserID, int nextUserID, CardData cardData)
        {
            StartCoroutine(GameController.instance.apiHelper.Turn(gameID, turnUserID, cardData.manaCost, attackedUserID, cardData.damage, nextUserID, cardData.id));
        }
    }
}
