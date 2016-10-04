using System.Collections.Generic;

namespace Game.Core.Validation
{
    public interface IGameInputValidator
    {
        int WordsRequired { get; }

        bool IsValid(string playerName);


        /// <summary> Determines whether there are any validation errors for a given word based on the rest of the player's words </summary>
        bool IsValid(string word, IEnumerable<string> otherPlayerWord);


        /// <summary> Determines whether there are any valdation errors for a playerinput, based on other player names and words </summary>
        bool IsValid(PlayerData playerInput, IEnumerable<string> existingWords, IEnumerable<string> existingPlayer);


        /// <summary> Validates the given player input based on the other player names and words | returns all errors as a <see cref="string"/> collection </summary>
        /// <returns> a collection of errors represented as <see cref="string"/> </returns>
        IEnumerable<string> PlayerInputValidationErrors(PlayerData playerInput, IEnumerable<string> existingWords, IEnumerable<string> existingPlayers);


        /// <summary> Validates player name and returns errors as a <see cref="string"/> collection </summary>
        IEnumerable<string> PlayerNameValidationErrors(string playerName);


        /// <summary> Validates the given word based on the existing words for the player and returns all errors as a <see cref="string"/> collection </summary>
        /// <returns> a collection of errors represented as <see cref="string"/> </returns>
        IEnumerable<string> WordValidationErrors(string word, IEnumerable<string> otherPlayerWords);
    }
}