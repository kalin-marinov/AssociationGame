using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Game.RestServer.Data
{
    public class GameDataService
    {
        ConcurrentDictionary<string, ConcurrentDictionary<string, object>> playersPerSession;
        ConcurrentDictionary<string, ConcurrentBag<string>> wordsPerSession;

        public GameDataService()
        {
            this.playersPerSession = new ConcurrentDictionary<string, ConcurrentDictionary<string, object>>();
            this.wordsPerSession = new ConcurrentDictionary<string, ConcurrentBag<string>>();
        }

        public void Initialize(string gameId)
        {
            this.playersPerSession.TryAdd(gameId, new ConcurrentDictionary<string, object>());
            this.wordsPerSession.TryAdd(gameId, new ConcurrentBag<string>());
        }

        public void AddPlayers(string gameId, string playerName, object[] players)
        {
            foreach (var player in players)
                this.playersPerSession[gameId].TryAdd(playerName, player);
        }

        public void AddWords(string gameId, string[] words)
        {
            foreach (var word in words)
                this.wordsPerSession[gameId].Add(word);
        }

        public IEnumerable<object> GetPlayers(string gameId)
        {
            return this.playersPerSession[gameId].Values;
        }

        public IEnumerable<string> GetWords(string gameId)
        {
            return this.wordsPerSession[gameId];
        }
    }
}