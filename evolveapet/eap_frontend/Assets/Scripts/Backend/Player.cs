using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolveAPet
{
    public class Player
    {

        public string NickName { set; get; }

        public string UserName { set; get; }
        public int Points { get; set; }
        private readonly Stable _stable;

        public Stable Stable { get { return _stable; } }

        public Player(Stable s, string username)
        {
            Points = 0;
            UserName = username;
            NickName = username;
            _stable = s;

        }

        // probably we want to serialize him to to save the game ...


        //Will be adding function for the KnownTraits but not boolean to boolean.
    }
}
