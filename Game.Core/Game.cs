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
        GameManager manager;

        public void Start()
        {
            var playersCount = gameUI.ReadPlayersCount();

            for (int i = 0; i < playersCount; i++)
            {
                var playerInput = gameUI.ReadPlayerWords();

                while (!manager.IsValid(playerInput))
                {
                    //gameUI.ShowValidationErrors();
                    playerInput = gameUI.ReadPlayerWords();
                }

                manager.StorePlayerData(playerInput);
            }

            // Game start:
            while (manager.RemainingWordsCount > 0)
            {
                var currentPlayer = manager.GetNextPlayer();
                var randomWord = manager.ChooseRandomWord();

                while (gameUI.HasGuessedWord(currentPlayer, randomWord))
                {
                    manager.MarkAsGuessed(randomWord, currentPlayer);
                    randomWord = manager.ChooseRandomWord();
                }
            }
        }

    }
}
