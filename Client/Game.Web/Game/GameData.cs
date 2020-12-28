

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

/// <summary> This is the data required for the game to begin - mostly players and words </summary>
public class GameData
{
    public readonly int WORDS_PER_PLAYER = 5;

    private Dictionary<string, HashSet<string>> wordsByPlayer;

    private Dictionary<string, Player> players;

    public IReadOnlyCollection<Player> Players => this.players.Values;

    public GameData()
    {
        this.players = new Dictionary<string, Player>();
        this.wordsByPlayer = new Dictionary<string, HashSet<string>>();
    }

    public GameData(IEnumerable<Player> players)
    {
        this.players = players.ToDictionary(p => p.Name);
        this.wordsByPlayer = new Dictionary<string, HashSet<string>>();
    }

    public void AddPlayer(string name)
    {
        name = name.Trim();

        if (!Regex.IsMatch(name, "^[a-zA-Zа-яА-Я\\s]{2,30}$"))
            throw new ArgumentException($"{name} is not a valid player name!");


        this.players.Add(name, new Player(name));
    }

    public void RemovePlayer(string name)
    {
        this.players.Remove(name);
    }

    public void AddWord(string word, string playerName)
    {
        word = word.ToLower().Trim();
        var player = this.players[playerName];

        if (!Regex.IsMatch(word, "^[а-я]{2,40}$"))
            throw new ArgumentException($"{word} is not a valid word. Do not use symbols or whitespaces");

        if (!this.wordsByPlayer.ContainsKey(player.Name))
            this.wordsByPlayer[player.Name] = new HashSet<string>();

        if (this.wordsByPlayer[player.Name].Count >= WORDS_PER_PLAYER)
            throw new InvalidOperationException($"Player {playerName} already has {WORDS_PER_PLAYER} words!");

        this.wordsByPlayer[player.Name].Add(word);
    }


    public void ClearWords(string name)
    {
        this.wordsByPlayer[name].Clear();
    }


    public int GetWordsCount(string playerName)
    {
        if (wordsByPlayer.ContainsKey(playerName))
            return this.wordsByPlayer[playerName].Count;
        else
            return 0;
    }


    public IEnumerable<string> GetWords()
    {
        return this.wordsByPlayer.SelectMany(kvp => kvp.Value);
    }

    public bool PlayersValid => players.Count >= 2 && players.Count % 2 == 0;

    public bool WordsValid => wordsByPlayer.Values.Count == players.Count
             && wordsByPlayer.Values.All(words => words.Count == WORDS_PER_PLAYER);
}