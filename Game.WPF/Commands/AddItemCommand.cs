using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace AssociationGame.Commands
{
    public class AddItemCommand<TItem> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ICollection<TItem> collection;
        private int limit;
        private Func<TItem, bool> validationFunction;

        public AddItemCommand(ICollection<TItem> collection, Func<TItem, bool> validationFunction = null, int limit = int.MaxValue)
        {
            this.collection = collection;
            this.limit = limit;
            this.validationFunction = validationFunction;
        }

        public virtual bool CanExecute(object parameter)
        {
            var validItem = validationFunction?.Invoke((TItem)parameter) ?? true;
            return validItem && collection.Count < limit;
        }

        public virtual void Execute(object parameter)
           => collection.Add((TItem)parameter);
        

        /// <summary> Notifies the UI to re-check if the command can be executed </summary>
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
