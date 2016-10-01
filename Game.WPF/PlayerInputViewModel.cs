using AssociationGame.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WhateverApp;
using WhateverApp.Validation;

namespace AssociationGame.WPF
{
    public class PlayerInputViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        private string playerName;

        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PlayerName")); }
        }

        private string currentWord;

        public string CurrentWord
        {
            get { return currentWord; }
            set
            {       
                currentWord = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentWord"));
            }
        }

        public ObservableCollection<string> Words { get; set; }

        public PlayerInputViewModel()
        {
            Words = new ObservableCollection<string>();
            CurrentWord = "Word";
            AddWordCommand = new AddItemCommand<string>(this, _ =>  PlayerInputValidation.ValidateCurrentWord(this) == null);
            RemoveWordCommand = new RemoveItemCommand<string>(Words);
        }

        public ICommand AddWordCommand { get; private set; }

        public ICommand RemoveWordCommand { get; private set; }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "PlayerName")
                    Error = PlayerInputValidation.ValidatePlayerName(this.PlayerName);

                else if (columnName == "CurrentWord")
                    Error = PlayerInputValidation.ValidateCurrentWord(this);

                else
                    Error = null;

                return Error;
            }
        }

        private string error;

        public string Error
        {
            get { return error; }
            set { error = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Error")); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ResetCurrentWord() => this.CurrentWord = "Word";

    }
}
