using System.Collections.Generic;

namespace Game.Core
{
    public class PlayerData
    {
        public string PlayerName { get; private set; }

        public ISet<string> Words { get;  set; }

    }
}
