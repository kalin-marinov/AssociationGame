using Game.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using WhateverApp;

namespace AssociationGame.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IGameUserInterface
    {

        public ObservableCollection<string> CurrentWords { get; set; }

        public Dictionary<string, List<string>> AllWords { get; set; }


        public PlayerInputViewModel PlayerVM { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            CurrentWords = new ObservableCollection<string>();
            AllWords = new Dictionary<string, List<string>>();
            this.DataContext = this;


            var game = new Game.Core.Game(this, new GameManager(), new Game.Core.Validation.GameInputValidator());
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

        public PlayerData ReadPlayerWords()
        {
            var window = new PlayerInput();

            while (window.ShowDialog() != true)
            {
                window = new PlayerInput(window.PlayerVM);
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
          =>  MessageBox.Show(string.Join(Environment.NewLine, errors));
        
    }
}
