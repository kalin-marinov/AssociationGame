using Game.Core;
using Game.Core.Validation;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AssociationGame.WPF
{

    public partial class App : Application, IGameUserInterface
    {
        private void Game_Startup(object sender, StartupEventArgs e)
        {
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var game = new GameEngine(this, new GameDataStorage(new GameInputValidator()));
            game.Start();
        }

        public int ReadPlayersCount()
        {
            var countWindow = new PlayerCountWindow();

            if (countWindow.ShowDialog() == true)
                return countWindow.Count;
            else
                Environment.Exit(0);

            throw new ArgumentException();
        }


        PlayerInputViewModel playerInput = new PlayerInputViewModel();
        int lastPlayerIndex = 0;

        public PlayerData ReadPlayerWords(int playerIndex)
        {
            // If it's a new player (different than the last one), prepare a brand new view model
            if (playerIndex != lastPlayerIndex)
            {
                lastPlayerIndex = playerIndex;
                playerInput = new PlayerInputViewModel();
            }

            var window = new PlayerInputWindow(playerInput);

            while (window.ShowDialog() != true)
            {
                window = new PlayerInputWindow(window.PlayerVM);
                window.ShowDialog();
            }

            var data = new PlayerData { PlayerName = window.PlayerVM.PlayerName, Words = new HashSet<string>(window.PlayerVM.Words) };
            return data;
        }

        public bool HasGuessedWord(string randomPlayer, string randomWord)
        {
            var window = new RandomWordWindow(randomWord, randomPlayer);
            return window.ShowDialog() == true;
        }

        public void DisplayErrors(IEnumerable<string> errors)
          => MessageBox.Show(string.Join(Environment.NewLine, errors));


        public void DisplayScore(IReadOnlyCollection<PlayerScore> playerGuesses)
        {
            throw new NotImplementedException();
        }
    }
}
