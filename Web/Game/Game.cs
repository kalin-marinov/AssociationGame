using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Game
{
    public int MAX_ROUNDS { get; set; } = 3;

    public TimeSpan TurnTime { get; private set; } = TimeSpan.FromSeconds(60);

    public int CurrentRound { get; private set; } = 0;

    public string CurrentWord { get; private set; }

    public Player CurrentPlayer { get; private set; }


    public PlayersManager Players { get; private set; }

    public WordsManager Words { get; private set; }

    public Scoreboard ScoreBoard { get; private set; }


    public Game(IReadOnlyCollection<string> words, IReadOnlyCollection<Player> players)
    {
        this.Players = new PlayersManager(players);
        this.ScoreBoard = new Scoreboard(this.Players.GetTeams());
        this.Words = new WordsManager(words);
        CurrentPlayer = this.Players.GetCurrentPlayer();
    }

    public void FinishRound()
    {
        this.Words.ResetRoundWords();
        CurrentWord = null;
        CurrentRound++;
    }

    /// <summary> Marks the current word as guessed and prepares a new one </summary>
    public void GuessWord()
    {
        ScoreBoard.AddPoints(CurrentPlayer.Name);
        Words.RemoveFromRound(CurrentWord);
        CurrentWord = Words.GetRandomWord();

        if (CurrentWord == null)
            TurnTimer.Cancel();
    }

    /// <summary> Begins the turn of the current player. The task resolves / finished when the turn is over (has ended) </summary>
    public async Task PlayTurn()
    {
        if (Words.RemainingWords == 0)
            throw new InvalidOperationException("Round is over. Please start the next one to play");

        CurrentWord = Words.GetRandomWord();
        await TurnTimer.Wait(TurnTime);

        CurrentPlayer = Players.Next();
        CurrentWord = null;
    }

    public IEnumerable<TeamScore> GetScores()
         => ScoreBoard.GetTeamScores();

}