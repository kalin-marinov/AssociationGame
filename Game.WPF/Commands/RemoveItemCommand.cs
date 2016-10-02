using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AssociationGame.Commands
{
    /// <summary> A simple command that removes an item from a collection </summary>
    /// <typeparam name="TItem"> The type of the items in the <see cref="ICollection{TItem}"/> </typeparam>
    public class RemoveItemCommand<TItem> : ICommand
    {
        private AddItemCommand<TItem> addCommand;
        private ICollection<TItem> collection;

        /// <summary> Creates a remove command by a given collection and optionally add command, that will be notified after execution of remove </summary>
        public RemoveItemCommand(ICollection<TItem> collection, AddItemCommand<TItem> addCommand = null)
        {
            this.collection = collection;
            this.addCommand = addCommand;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return collection.Any() && collection.Contains((TItem)parameter);
        }

        public void Execute(object parameter)
        {
            collection.Remove((TItem)parameter);
            addCommand?.RaiseCanExecuteChanged();
        }
    }
}
