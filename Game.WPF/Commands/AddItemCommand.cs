using AssociationGame.WPF;
using System;
using System.Windows.Input;

namespace AssociationGame.Commands
{
    public class AddItemCommand<TItem> : ICommand
    {
        private Func<TItem, bool> validationFunc;
        private PlayerInputViewModel viewModel;

        public AddItemCommand(PlayerInputViewModel viewModel, Func<TItem, bool> validationFunc)
        {
            this.viewModel = viewModel;
            this.validationFunc = validationFunc;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => validationFunc((TItem)parameter);


        public void Execute(object parameter)
        {
            viewModel.Words.Add((string)parameter);
            viewModel.ResetCurrentWord();
        }
    }
}
