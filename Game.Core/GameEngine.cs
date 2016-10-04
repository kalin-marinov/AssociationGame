using Game.Core.Exceptions;
using System;

namespace Game.Core
{
    public class GameEngine
    {
        IGameUserInterface gameUI;
        IGameDataStorage manager;

        const int NumberOfRounds = 3;

        public GameEngine(IGameUserInterface gameUI, IGameDataStorage manager)
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


            // Begin Game:
            for (int i = 0; i < NumberOfRounds; i++)
            {
                PlayRound();
            }
        }

        private void PlayRound()
        {
            // Round {i} - fight

            while (manager.RemainingWordsCount > 0)
            {
                var currentPlayer = manager.GetNextPlayer();
                var randomWord = manager.ChooseRandomWord();

                while (gameUI.HasGuessedWord(currentPlayer, randomWord))
                {
                    manager.MarkAsGuessed(randomWord, currentPlayer);

                    if (manager.RemainingWordsCount <= 0) break;

                    randomWord = manager.ChooseRandomWord();
                }
            }

            gameUI.DisplayScore(manager.Score);
            manager.ResetData();
        }

        /// <summary> Executes an action until it succeeds and reports errors encountered to the given user interface </summary>
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
