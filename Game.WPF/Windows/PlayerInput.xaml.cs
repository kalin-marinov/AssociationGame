using System.Windows;

namespace AssociationGame.WPF
{
    /// <summary> Dialog for the player to fill in playerName and words </summary>
    public partial class PlayerInputWindow : Window
    {
        public PlayerInputViewModel PlayerVM { get; private set; }

        public PlayerInputWindow() : this(new PlayerInputViewModel { PlayerName = "PlayerName" })
        {

        }

        public PlayerInputWindow(PlayerInputViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = this;
            this.PlayerVM = viewModel;

            PlayerVM.DoneCommand.OnExecuted += DoneCommand_OnExecuted;
        }

        private void DoneCommand_OnExecuted(object sender, System.EventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
