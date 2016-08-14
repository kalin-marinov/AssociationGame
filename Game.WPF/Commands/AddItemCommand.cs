using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WhateverApp
{
    public class AddItemCommand<TItem> : ICommand
    {
        private Collection<string> collection;
        private Func<TItem, bool> validationFunc;

        public AddItemCommand(Collection<string> collection, Func<TItem, bool> validationFunc)
        {
            this.collection = collection;
            this.validationFunc = validationFunc;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => validationFunc((TItem)parameter);


        public void Execute(object parameter)
        {
            collection.Add((string)parameter);
        }
    }
}
