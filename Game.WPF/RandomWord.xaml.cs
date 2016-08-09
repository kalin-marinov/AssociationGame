using System;
using System.ComponentModel;
using System.Media;
using System.Timers;
using System.Windows;

namespace AssociationGame.WPF
{
    /// <summary>
    /// Interaction logic for RandomWord.xaml
    /// </summary>
    public partial class RandomWordWindow : Window
    {
        Timer timer;
        private string word;

        public TimeSpan RemainingTime { get; private set; }
        
        public RandomWordWindow(string word, string playerName) : this(word, playerName, TimeSpan.FromSeconds(15), false)
        {
        
        }

        public RandomWordWindow(string word, string playerName, TimeSpan remainingTime, bool autoStart = true)
        {
            InitializeComponent();
            this.word = word;
            this.RemainingTime = remainingTime;
            this.timerLabel.Content = RemainingTime.ToString();
            this.playerLabel.Content = playerName;
            this.DataContext = this;
            this.timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
          

            if (autoStart)
                startBtn_Click(this, new RoutedEventArgs());
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RemainingTime = RemainingTime.Add(TimeSpan.FromSeconds(-1));
            Dispatcher.Invoke(() => timerLabel.Content = RemainingTime.ToString());

            if (RemainingTime.Seconds == 0)
                this.Dispatcher.Invoke(Close);
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            this.wordLabel.Content = word;
            guessBtn.IsEnabled = true;
            startBtn.IsEnabled = false;

            timer.Start();
        }

        private void guessBtn_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Beep.Play();
            this.DialogResult = true;
            timer.Dispose();
            this.Close();
        }
    }
}
