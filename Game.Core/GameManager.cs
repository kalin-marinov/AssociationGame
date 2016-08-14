using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Core
{
    public class GameManager
    {
        List<string> players = new List<string>();
        List<string> words = new List<string>();
        IDictionary<string, IList<string>> guessedWords = new Dictionary<string, IList<string>>();

        int? currentPlayerIndex;

        public int RemainingWordsCount => words.Count;

        public void StorePlayerData(PlayerData playerData)
        {
            words.AddRange(playerData.Words);
            players.Add(playerData.PlayerName);
            guessedWords.Add(playerData.PlayerName, new List<string>());
        }

        public virtual bool IsValid(PlayerData playerInput)
        {
            return players.Contains(playerInput.PlayerName)
                || playerInput.Words.Any(word => words.Contains(word));
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
            guessedWords[playerWhoGuessed].Add(word);
        }

        public IReadOnlyCollection<PlayerScore> GetScore()
        {
            return guessedWords.Select(i =>
                new PlayerScore { PlayerName = i.Key, WordsGuessedCount = i.Value.Count }).ToArray();
        }
    }
}
