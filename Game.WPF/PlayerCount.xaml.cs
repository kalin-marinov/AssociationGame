using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            this.DialogResult = true;
            this.Close();
        }
    }
}
