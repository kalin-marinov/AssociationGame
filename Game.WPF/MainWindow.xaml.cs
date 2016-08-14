using Game.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AssociationGame.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IGameUserInterface
    {

        public ObservableCollection<string> CurrentWords { get; set; }

        public Dictionary<string, List<string>> AllWords { get; set; }

        private int playersCount;

        public PlayerInputViewModel PlayerVM { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            CurrentWords = new ObservableCollection<string>();
            AllWords = new Dictionary<string, List<string>>();
            this.DataContext = this;



            PlayerVM = new PlayerInputViewModel { PlayerName = "Pesho" };



        }


        private void addBtn_Click(object sender, RoutedEventArgs e)
        {


            var word = wordBox.Text;
            CurrentWords.Add(word);

            if (CurrentWords.Count >= 5)
                this.addBtn.IsEnabled = false;
        }

        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            PlayerVM.Error = "Something's wrong";
            return;

            if (CurrentWords.Count < 5)
                MessageBox.Show("You need five words to finish");

            else if (CurrentWords.Count % 5 == 0)
            {
                addBtn.IsEnabled = true;
                wordBox.Text = string.Empty;
                AllWords.Add(nameBox.Text, new List<string>(CurrentWords));
                CurrentWords.Clear();
            }

            if (AllWords.Count == playersCount)
                StartGame();

        }

        private void StartGame()
        {
            var words = AllWords.SelectMany(x => x.Value).ToList();
            var allPlayers = AllWords.Keys.ToList();
            var playerIndex = new Random().Next(0, playersCount);
            var player = allPlayers[playerIndex];

            MessageBox.Show($"Starting player is {player}");

            while (words.Count > 0)
            {
                var randomIndex = new Random().Next(0, words.Count);
                var guessWindow = new RandomWordWindow(words[randomIndex], player);
                var isGuessed = guessWindow.ShowDialog().GetValueOrDefault(false);

                while (isGuessed)
                {
                    words.Remove(words[randomIndex]);
                    randomIndex = new Random().Next(0, words.Count);
                    guessWindow = new RandomWordWindow(words[randomIndex], player, guessWindow.RemainingTime);
                    isGuessed = guessWindow.ShowDialog().GetValueOrDefault(false);
                }

                playerIndex = (playerIndex + 1) % playersCount;
                player = allPlayers[playerIndex];
            }
        }

        private void removeWord_Click(object sender, RoutedEventArgs e)
        {
            CurrentWords.Remove((sender as Button).CommandParameter as string);
            this.addBtn.IsEnabled = true;
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
            PlayerVM = new PlayerInputViewModel();

            return new PlayerData();
        }

        public bool HasGuessedWord(string randomPlayer, string randomWord)
        {
            throw new NotImplementedException();
        }
    }
}
