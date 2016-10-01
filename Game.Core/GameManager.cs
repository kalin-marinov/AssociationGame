using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Core
{
    /// <summary> Stores information about players in the game, words that are guessed or yet to be guessed </summary>
    public class GameManager
    {
        List<string> players = new List<string>();
        List<string> words = new List<string>();
        IDictionary<string, ISet<string>> guesses = new Dictionary<string, ISet<string>>();
        int? currentPlayerIndex;

        public int RemainingWordsCount => words.Count;

        public IReadOnlyCollection<PlayerScore> Score
        {
            get
            {
                return guesses.Select(g =>
                    new PlayerScore { PlayerName = g.Key, WordsGuessedCount = g.Value.Count }).ToArray();
            }
        }



        public void StorePlayerData(PlayerData playerData)
        {
            words.AddRange(playerData.Words);
            players.Add(playerData.PlayerName);
            guesses.Add(playerData.PlayerName, new HashSet<string>());
        }

        public virtual IEnumerable<string> GetValidationErrors(PlayerData playerInput)
        {
            if (players.Contains(playerInput.PlayerName))
                yield return "Player with that name already exists";

            var duplicatedWords = playerInput.Words.Intersect(words);

            if (duplicatedWords.Any())
                yield return $"duplicated words: {string.Join(",", duplicatedWords)}";
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
                currentPlayerIndex++;

            else
                currentPlayerIndex = new Random().Next(0, players.Count);

            return players[currentPlayerIndex.Value];
        }

        public void MarkAsGuessed(string word, string playerWhoGuessed)
        {
            guesses[playerWhoGuessed].Add(word);
            words.Remove(word);
        }

        public void Restart()
        {
            words = guesses.SelectMany(g => g.Value).Union(words).ToList();
            currentPlayerIndex = null;

            foreach (var guess in guesses)
                guess.Value.Clear();
        }
    }
}
