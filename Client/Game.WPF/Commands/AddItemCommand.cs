using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace AssociationGame.Commands
{
    /// <summary> Generic command that adds an item in a collection </summary>
    public class AddItemCommand<TItem> : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event EventHandler OnExecuted;

        private ICollection<TItem> collection;
        private int limit;
        private Func<TItem, bool> validationFunction;

        public AddItemCommand(ICollection<TItem> collection, Func<TItem, bool> validationFunction = null, int limit = int.MaxValue)
        {
            this.collection = collection;
            this.limit = limit;
            this.validationFunction = validationFunction;
        }

        /// <summary> Can be executed if the item is valid and the limit is not reached. If the validation function is null - it is considered valid </summary>
        public virtual bool CanExecute(object parameter)
        {
            var validItem = validationFunction?.Invoke((TItem)parameter) ?? true;
            return validItem && collection.Count < limit;
        }

        public virtual void Execute(object parameter)
        {
            collection.Add((TItem)parameter);
            OnExecuted?.Invoke(this, EventArgs.Empty);
        }

        /// <summary> Notifies the UI to re-check if the add command can be executed see: <see cref="CanExecute(object)"/> </summary>
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
