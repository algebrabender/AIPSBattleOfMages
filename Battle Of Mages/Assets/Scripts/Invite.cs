using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class Invite
    {
        private string userFrom;
        private int gameID;

        public Invite(string userFrom, int gameID)
        {
            this.userFrom = userFrom;
            this.gameID = gameID;
        }
    }
}
