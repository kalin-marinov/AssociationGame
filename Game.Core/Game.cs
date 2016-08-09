using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public class Game
    {

        IGameUserInterface gameUI;

        public void Start()
        {
           int playersCount = gameUI.ReadPlayersCount();

            for (int i = 0; i < playersCount; i++)
            {

                var wordsData = gameUI.ReadPlayerWords();
                // Store words somewhere
            }

            

        }

    }
}
