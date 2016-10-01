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

        public PlayerInput()
        {
            InitializeComponent();
            this.DataContext = this;
            PlayerVM = new PlayerInputViewModel { PlayerName = "Pesho" };
        }

        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
