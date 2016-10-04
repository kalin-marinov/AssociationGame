using System.Collections.Generic;

namespace Game.Core
{
    public interface IGameUserInterface
    {
        /// <summary> Prepares input form for the user to fill in the number of players </summary>
        int ReadPlayersCount();

        /// <summary> Prepares input form for one of the players to enter the words </summary>
        /// <param name="playerIndex"> index of the player - used to distinguish between players </param>
        PlayerData ReadPlayerWords(int playerIndex);

        /// <summary> Prepares input form for the player to try to guess the words </summary>
        bool HasGuessedWord(string randomPlayer, string randomWord);

        /// <summary> Show erros to the user  </summary>
        void DisplayErrors(IEnumerable<string> errors);

        /// <summary> Display score for all players </summary>
        void DisplayScore(IReadOnlyCollection<PlayerScore> playerGuesses);
    }
}