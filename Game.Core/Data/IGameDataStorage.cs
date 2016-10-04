using System.Collections.Generic;

namespace Game.Core
{
    public interface IGameDataStorage
    {
        int RemainingWordsCount { get; }

        IReadOnlyCollection<PlayerScore> Score { get; }

        string ChooseRandomWord();

        string GetNextPlayer();

        void MarkAsGuessed(string word, string playerWhoGuessed);

        /// <summary> Reset all player guesses (score) </summary>
        void ResetData();

        /// <summary> Validates and stores the given player input </summary>
        /// <exception cref="Exceptions.InputValidationException" />
        /// <exception cref="System.NullReferenceException" />
        void StorePlayerData(PlayerData playerData);
    }
}