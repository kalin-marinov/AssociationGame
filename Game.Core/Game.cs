using Game.Core.Exceptions;
using System;

namespace Game.Core
{
    public class Game
    {
        IGameUserInterface gameUI;
        GameManager manager;

        public Game(IGameUserInterface gameUI, GameManager manager)
        {
            this.gameUI = gameUI;
            this.manager = manager;
        }
        
        public void Start()
        {
            var playersCount = gameUI.ReadPlayersCount();

            // Read input from all players:
            for (int i = 0; i < playersCount; i++)
            {
                TryUntilSuccess(gameUI, () =>
                {
                    var playerInput = gameUI.ReadPlayerWords(i);
                    manager.StorePlayerData(playerInput);
                });
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

        /// <summary> Executes an action until it succeeds and reports errors to the given user interface </summary>
        private void TryUntilSuccess(IGameUserInterface ui, Action action)
        {
            while (true)
            {
                try
                {
                    action();
                    return;
                }
                catch (InputValidationException ex)
                {
                    ui.DisplayErrors(ex.Errors);
                }
                catch (Exception ex)
                {
                    ui.DisplayErrors(new[] { ex.Message });
                }
            }
        }
    }
}
