using System.Linq;

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
                var playerInput = gameUI.ReadPlayerWords();
                var validationErrors = manager.GetValidationErrors(playerInput);

                while (validationErrors.Any())
                {
                    gameUI.DisplayErrors(validationErrors);
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
