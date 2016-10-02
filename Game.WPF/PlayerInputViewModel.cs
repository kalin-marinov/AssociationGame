using AssociationGame.Commands;
using Game.Core.Validation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentWord)));
                AddWordCommand?.RaiseCanExecuteChanged();
            }
        }

        public AddWordCommand AddWordCommand { get; private set; }

        public ICommand RemoveWordCommand { get; private set; }

        public GameInputValidator Validator { get; private set; }

        public ObservableCollection<string> Words { get; private set; }

        public PlayerInputViewModel(GameInputValidator validator)
        {
            CurrentWord = "Word";
            this.Validator = validator;

            Words = new ObservableCollection<string>();
            AddWordCommand = new AddWordCommand(this, validator);
            RemoveWordCommand = new RemoveItemCommand<string>(Words, AddWordCommand);
        }

        public PlayerInputViewModel() : this(new GameInputValidator())
        {
        }


        /// <summary> Invoked after <see cref="PropertyChanged" /> to determine if any errors have occured </summary>
        /// <param name="columnName"> the name of the property that was changed </param>
        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(PlayerName))
                    Error = string.Join(",", Validator.PlayerNameValidationErrors(PlayerName));

                else if (columnName == nameof(CurrentWord))
                    Error = string.Join(",", Validator.WordValidationErrors(CurrentWord, Words));

                return Error;
            }
        }

        private string error;

        public string Error
        {
            get { return error; }
            set { error = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Error")); }
        }


        /// <summary> Notifies the user interface that a property was changed </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public void ResetCurrentWord() => CurrentWord = "Word";

    }
}
