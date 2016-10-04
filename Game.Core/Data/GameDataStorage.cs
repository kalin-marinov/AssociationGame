using Game.Core.Exceptions;
using Game.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Core
{
    /// <summary> Stores information about players in the game, words that are guessed or yet to be guessed </summary>
    public class GameDataStorage : IGameDataStorage
    {
        private IGameInputValidator validator;

        List<string> players;
        List<string> words;
        IDictionary<string, ISet<string>> playersGuesses;
        int? currentPlayerIndex;

        public int RemainingWordsCount => words.Count;

        public IReadOnlyCollection<PlayerScore> Score
        {
            get
            {
                return playersGuesses.Select(g =>
                    new PlayerScore { PlayerName = g.Key, WordsGuessedCount = g.Value.Count }).ToArray();
            }
        }

        public GameDataStorage(IGameInputValidator validator)
        {
            this.validator = validator;
            this.playersGuesses = new Dictionary<string, ISet<string>>();
            this.words = new List<string>(capacity: 10);
            this.players = new List<string>();
        }


        public void StorePlayerData(PlayerData playerData)
        {
            var validationErrors = validator.PlayerInputValidationErrors(playerData, words, players);
            if (validationErrors.Any())
                throw new InputValidationException(validationErrors);


            words.AddRange(playerData.Words);
            players.Add(playerData.PlayerName);
            playersGuesses.Add(playerData.PlayerName, new HashSet<string>());
        }


        public string ChooseRandomWord()
        {
            var random = new Random();
            var randIndex = random.Next(0, words.Count);
            return words[randIndex];
        }

        public string GetNextPlayer()
        {
            if (currentPlayerIndex.HasValue)
                currentPlayerIndex = currentPlayerIndex++ % players.Count;

            else
                currentPlayerIndex = new Random().Next(0, players.Count);

            return players[currentPlayerIndex.Value];
        }

        public void MarkAsGuessed(string word, string playerWhoGuessed)
        {
            playersGuesses[playerWhoGuessed].Add(word);
            words.Remove(word);
        }

        public void ResetData()
        {
            // Transfer all words back to the initial words collection
            words = words.Union(playersGuesses.SelectMany(g => g.Value)).ToList();

            foreach (var playerGuesses in playersGuesses)
                playerGuesses.Value.Clear();
        }
    }
}
