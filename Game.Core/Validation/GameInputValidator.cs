using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Core.Validation
{
    public class GameInputValidator : IGameInputValidator
    {
        public virtual int WordsRequired { get; protected set; } = 5;

        private StringComparer IgnoreCase => StringComparer.CurrentCultureIgnoreCase;


        public virtual IEnumerable<string> WordValidationErrors(string word, IEnumerable<string> otherPlayerWords)
        {
            if (string.IsNullOrWhiteSpace(word))
                yield return "Word cannot be empty";

            if (word.Any(c => !char.IsLetter(c)))
                yield return "Words must contain only letters";

            if (otherPlayerWords.Contains(word, IgnoreCase))
                yield return $@"You already have ""{word}"" in the list";
        }


        public virtual IEnumerable<string> PlayerNameValidationErrors(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                yield return "Player name cannot be empty";

            else if (playerName.Any(c=> !char.IsLetter(c) && !char.IsWhiteSpace(c)))
                yield return "Player should consists only letters and whitespaces";
        }



        public virtual IEnumerable<string> PlayerInputValidationErrors(PlayerData playerInput, IEnumerable<string> existingWords, IEnumerable<string> existingPlayers)
        {
            if (playerInput.Words.Count > WordsRequired)
                yield return $"You cannot have more than {WordsRequired} words";

            if (existingPlayers.Contains(playerInput.PlayerName, IgnoreCase))
                yield return "Player with that name already exists";

            var duplicatedWords = playerInput.Words.Intersect(existingWords, IgnoreCase);

            if (duplicatedWords.Any())
                yield return $"duplicated words: {string.Join(",", duplicatedWords)}";
        }

        public virtual bool IsValid(string word, IEnumerable<string> otherPlayerWord)
            => !WordValidationErrors(word, otherPlayerWord).Any();



        public virtual bool IsValid(PlayerData playerInput, IEnumerable<string> existingWords, IEnumerable<string> existingPlayer)
            => !PlayerInputValidationErrors(playerInput, existingWords, existingPlayer).Any();

        public virtual bool IsValid(string playerName)
           => !PlayerNameValidationErrors(playerName).Any();
    }
}
