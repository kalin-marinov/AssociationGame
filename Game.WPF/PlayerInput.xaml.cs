using AssociationGame.WPF;
using System.Windows;

namespace WhateverApp
{
    /// <summary>
    /// Interaction logic for PlayerInput.xaml
    /// </summary>
    public partial class PlayerInput : Window
    {
        public PlayerInputViewModel PlayerVM { get; private set; }

        public PlayerInput() : this(new PlayerInputViewModel { PlayerName = "PlayerName" })
        {
        }

        public PlayerInput(PlayerInputViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = this;
            this.PlayerVM = viewModel;
        }

        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerVM.Words.Count < PlayerVM.Validator.WordLimit)
                MessageBox.Show("You need five words to finish");

            else if (!PlayerVM.Validator.IsValid(PlayerVM.PlayerName))
                MessageBox.Show("You need five words to finish");

            else
            {
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
