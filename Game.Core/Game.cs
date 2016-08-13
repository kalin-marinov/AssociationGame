using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public class Game
    {
        List<string> players = new List<string>();
        List<string> words = new List<string>();
        IDictionary<string, IList<string>> guessedWords;

        IGameUserInterface gameUI;

        public void Start()
        {
            var playersCount = gameUI.ReadPlayersCount();

            for (int i = 0; i < playersCount; i++)
            {
                var playerInput = gameUI.ReadPlayerWords();

                while (!AreValid(playerInput.Words))
                {
                    // Try again:
                    playerInput = gameUI.ReadPlayerWords();
                }

                words.AddRange(playerInput.Words);
                players.Add(playerInput.PlayerName);
            }

            // Game start:
            while (words.Count > 0)
            {
                var currentPlayer = GetNextPlayer();
                var wordsGuessed = new List<string>();

                // Game session
                var randomWord = ChooseRandomWord();

                while (gameUI.HasGuessedWord(currentPlayer, randomWord))
                {
                    wordsGuessed.Add(randomWord);
                    randomWord = ChooseRandomWord();
                }
            }

        }

        private bool AreValid(IReadOnlyCollection<string> words)
        {
            throw new NotImplementedException();
        }

        private string ChooseRandomWord()
        {
            throw new NotImplementedException();
        }

        private string GetNextPlayer()
        {
            throw new NotImplementedException();
        }
    }
}
