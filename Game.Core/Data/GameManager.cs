using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Core
{
    /// <summary> Stores information about players in the game, words that are guessed or yet to be guessed </summary>
    public class GameManager
    {
        public List<string> Players { get; private set; } = new List<string>();

        public List<string> Words { get; private set; } = new List<string>();

        IDictionary<string, ISet<string>> playersGuesses = new Dictionary<string, ISet<string>>();
        int? currentPlayerIndex;

        public int RemainingWordsCount => Words.Count;

        public IReadOnlyCollection<PlayerScore> Score
        {
            get
            {
                return playersGuesses.Select(g =>
                    new PlayerScore { PlayerName = g.Key, WordsGuessedCount = g.Value.Count }).ToArray();
            }
        }


        public void StorePlayerData(PlayerData playerData)
        {
            Words.AddRange(playerData.Words);
            Players.Add(playerData.PlayerName);
            playersGuesses.Add(playerData.PlayerName, new HashSet<string>());
        }


        public string ChooseRandomWord()
        {
            var random = new Random();
            var randIndex = random.Next(0, Words.Count);
            return Words[randIndex];
        }

        public string GetNextPlayer()
        {
            if (currentPlayerIndex.HasValue)
                currentPlayerIndex = currentPlayerIndex++ % Players.Count;

            else
                currentPlayerIndex = new Random().Next(0, Players.Count);

            return Players[currentPlayerIndex.Value];
        }

        public void MarkAsGuessed(string word, string playerWhoGuessed)
        {
            playersGuesses[playerWhoGuessed].Add(word);
            Words.Remove(word);
        }

        public void Restart()
        {
            // Transfer all words back to the initial words collection
            Words = Words.Union(playersGuesses.SelectMany(g => g.Value)).ToList();
            currentPlayerIndex = null;

            foreach (var playerGuesses in playersGuesses)
                playerGuesses.Value.Clear();
        }
    }
}
