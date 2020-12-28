using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace AssociationGame.Commands
{
    /// <summary> A command that becomes available when the collection gets filled with the desired amount of items </summary>
    public class CollectionDoneCommand : ICommand
    {
        private IReadOnlyCollection<object> collection;
        private int minItems;

        public event EventHandler CanExecuteChanged;
        public event EventHandler OnExecuted;

        public CollectionDoneCommand(IReadOnlyCollection<object> collection, int minItems)
        {
            this.collection = collection;
            this.minItems = minItems;
        }

        public bool CanExecute(object parameter)
           => this.collection.Count >= minItems;

        public void Execute(object parameter)
          => OnExecuted?.Invoke(this, EventArgs.Empty);
        

        /// <summary> Notified the UI to re-check if the done command can be executed </summary>
        public void RaiseCanExecuteChanged()
          => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
