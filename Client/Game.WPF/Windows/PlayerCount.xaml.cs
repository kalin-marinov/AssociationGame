using System.Windows;

namespace AssociationGame.WPF
{
    /// <summary>
    /// Interaction logic for PlayerCount.xaml
    /// </summary>
    public partial class PlayerCountWindow : Window
    {

        public int Count { get; set; }

        public PlayerCountWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Count % 2 != 0)
                MessageBox.Show("The number of players must be an even number");

            else
            {
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
