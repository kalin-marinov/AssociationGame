using AssociationGame.WPF;

namespace WhateverApp.Validation
{
    public static class PlayerInputValidation
    {
        public static string ValidatePlayerName(string playerName)
        {
            return string.IsNullOrWhiteSpace(playerName)? "Player name must not be empty" : null;
        }

        public static string ValidateCurrentWord(PlayerInputViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.CurrentWord))
            {
                return "Word cannot be empty";
            }
            if (vm.Words.Contains(vm.CurrentWord))
            {
                return "Words cannot be duplicated";
            }
            return null;
        }
    }
}
