

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary> This is the data required for the game to begin - mostly players and words </summary>
public class GameData
{
    public readonly int WORDS_PER_PLAYER = 5;

    private Dictionary<string, HashSet<string>> wordsByPlayer;

    private Dictionary<string, Player> players;

    public GameData()
    {
        this.players = new Dictionary<string, Player>();
        this.wordsByPlayer = new Dictionary<string, HashSet<string>>();
    }

    public void AddPlayer(string name)
    {
        this.players.Add(name, new Player(name));
    }

    public void AddWord(string word, string playerName)
    {
        var player = this.players[playerName];

        if (!this.wordsByPlayer.ContainsKey(player.Name))
            this.wordsByPlayer[player.Name] = new HashSet<string>();

        if (this.wordsByPlayer[player.Name].Count >= WORDS_PER_PLAYER)
            throw new InvalidOperationException($"Player {playerName} already has {WORDS_PER_PLAYER} words!");

        this.wordsByPlayer[player.Name].Add(word);
    }

    public int GetWordsCount(string playerName)
    {
        if (wordsByPlayer.ContainsKey(playerName))
            return this.wordsByPlayer[playerName].Count;
        else
            return 0;
    }

    public IReadOnlyCollection<Player> GetPlayers()
    {
        return this.players.Values;
    }

    public IEnumerable<string> GetWords()
    {
        return this.wordsByPlayer.SelectMany(kvp => kvp.Value);
    }
}