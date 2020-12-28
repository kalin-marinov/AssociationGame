

using System.Collections.Generic;
using System.Linq;

public class Scoreboard
{

    private Dictionary<string, int> totalPlayerScores;

    private IReadOnlyCollection<(Player player1, Player player2)> teams;

    public Scoreboard(IReadOnlyCollection<(Player player1, Player player2)> teams)
    {
        this.teams = teams;
        this.totalPlayerScores = new Dictionary<string, int>(teams.Count * 2);

        foreach (var team in teams)
        {
            this.totalPlayerScores[team.player1.Name] = 0;
            this.totalPlayerScores[team.player2.Name] = 0;
        }
    }

    public void AddPoint(string playerName)
    {
        this.totalPlayerScores[playerName]++;
    }

    public void RemovePoint(string playerName)
    {
        this.totalPlayerScores[playerName]--;
    }

    public int GetPlayerScore(string playerName)
    {
        return totalPlayerScores[playerName];
    }

    public IEnumerable<TeamScore> GetTeamScores()
    {
        return teams.Select(t => new TeamScore
        {
            Player1 = t.player1,
            Player2 = t.player2,
            Score = GetPlayerScore(t.player1.Name) + GetPlayerScore(t.player2.Name)
        });
    }
}