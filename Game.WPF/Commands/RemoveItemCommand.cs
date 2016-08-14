using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WhateverApp
{
    public class RemoveItemCommand<TItem> : ICommand
    {
        private Collection<TItem> collection;

        public RemoveItemCommand(Collection<TItem> collection)
        {
            this.collection = collection;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return collection.Contains((TItem)parameter);
        }

        public void Execute(object parameter)
        {
            collection.Remove((TItem)parameter);
        }
    }
}
