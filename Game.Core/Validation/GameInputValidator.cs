using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Core.Validation
{
    public class GameInputValidator
    {
        public int WordLimit { get; protected set; } = 5;

        private StringComparer IgnoreCase => StringComparer.CurrentCultureIgnoreCase;

        /// <summary> Validates the given word based on the existing words for the player and returns all errors as a <see cref="string"/> collection </summary>
        /// <returns> a collection of errors represented as <see cref="string"/> </returns>
        public virtual IEnumerable<string> WordValidationErrors(string word, IEnumerable<string> otherPlayerWords)
        {
            if (string.IsNullOrWhiteSpace(word))
                yield return "Word cannot be empty";

            if (word.Any(c => !char.IsLetter(c)))
                yield return "Words must contain only letters";

            if (otherPlayerWords.Contains(word, IgnoreCase))
                yield return $@"You already have ""{word}"" in the list";
        }


        /// <summary> Validates player name and returns errors as a <see cref="string"/> collection </summary>
        public virtual IEnumerable<string> PlayerNameValidationErrors(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                yield return "Player name cannot be empty";

            if (playerName.Any(c=> !char.IsLetter(c) && !char.IsWhiteSpace(c)))
                yield return "Player should consists only letters and whitespaces";
        }


        /// <summary> Validates the given player input based on the other player names and words | returns all errors as a <see cref="string"/> collection </summary>
        /// <returns> a collection of errors represented as <see cref="string"/> </returns>
        public virtual IEnumerable<string> PlayerInputValidationErrors(PlayerData playerInput, IEnumerable<string> existingWords, IEnumerable<string> existingPlayers)
        {
            if (playerInput.Words.Count > WordLimit)
                yield return $"You cannot have more than {WordLimit} words";

            if (existingPlayers.Contains(playerInput.PlayerName, IgnoreCase))
                yield return "Player with that name already exists";

            var duplicatedWords = playerInput.Words.Intersect(existingWords, IgnoreCase);

            if (duplicatedWords.Any())
                yield return $"duplicated words: {string.Join(",", duplicatedWords)}";
        }

        /// <summary> Determines whether there are any validation errors for a given word based on the rest of the player's words </summary>
        public virtual bool IsValid(string word, IEnumerable<string> otherPlayerWord)
            => !WordValidationErrors(word, otherPlayerWord).Any();


        /// <summary> Determines whether there are any valdation errors for a playerinput, based on other player names and words </summary>
        public virtual bool IsValid(PlayerData playerInput, IEnumerable<string> existingWords, IEnumerable<string> existingPlayer)
            => !PlayerInputValidationErrors(playerInput, existingWords, existingPlayer).Any();

        public virtual bool IsValid(string playerName)
           => !PlayerNameValidationErrors(playerName).Any();
    }
}
