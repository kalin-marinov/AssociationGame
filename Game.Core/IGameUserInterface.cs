namespace Game.Core
{
    internal interface IGameUserInterface
    {
        int ReadPlayersCount();
        PlayerData ReadPlayerWords();
        bool HasGuessedWord(string randomPlayer, string randomWord);
    }
}