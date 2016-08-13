using System.Collections.Generic;

namespace Game.Core
{
    public class PlayerData
    {
        public string PlayerName { get; private set; }

        public IReadOnlyCollection<string> Words { get; private set; }

    }
}
