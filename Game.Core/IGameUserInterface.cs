using System.Collections.Generic;

namespace Game.Core
{
    public interface IGameUserInterface
    {
        int ReadPlayersCount();

        PlayerData ReadPlayerWords(int playerIndex);

        bool HasGuessedWord(string randomPlayer, string randomWord);

        void DisplayErrors(IEnumerable<string> errors);
    }
}