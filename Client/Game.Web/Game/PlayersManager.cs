using System;
using System.Collections.Generic;

public class PlayersManager
{
    private IReadOnlyCollection<Player> allPlayers;

    private List<Player> orderedPlayers;

    private List<(Player player1, Player player2)> teams;

    private int playerIndex;

    public PlayersManager(IReadOnlyCollection<Player> allPlayers)
    {
        this.allPlayers = allPlayers;
        this.playerIndex = 0;

        PrepareOrder();
        TeamUp();
    }

    public Player Next()
    {
        playerIndex = (playerIndex + 1) % allPlayers.Count;
        return GetCurrentPlayer();
    }

    public Player GetCurrentPlayer()
    {
        return this.orderedPlayers[playerIndex];
    }

    public IReadOnlyCollection<(Player player1, Player player2)> GetTeams()
    {
        return this.teams;
    }

    public IReadOnlyList<Player> GetPlayerOrder()
    {
        return this.orderedPlayers;
    }

    private void PrepareOrder()
    {
        var rng = new Random();
        var unAssignedPlayers = new HashSet<Player>(allPlayers);
        orderedPlayers = new List<Player>(allPlayers.Count);
       
        while (unAssignedPlayers.Count > 0)
        {
            var nextPlayer = unAssignedPlayers.GetRandomElement(rng);
            orderedPlayers.Add(nextPlayer);
            unAssignedPlayers.Remove(nextPlayer);
        }
    }

    private void TeamUp()
    {
        var teamsCount = orderedPlayers.Count / 2;
        this.teams = new List<(Player player1, Player player2)>(teamsCount);

        for (int i = 0; i < teamsCount; i++)
        {
            var player1 = orderedPlayers[i];
            var player2 = orderedPlayers[teamsCount + i];
            player1.TeamMate = player2.Name;
            player2.TeamMate = player1.Name;
            teams.Add((player1, player2));
        }
    }
}