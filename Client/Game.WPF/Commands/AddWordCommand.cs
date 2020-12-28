using AssociationGame.WPF;
using Game.Core.Validation;

namespace AssociationGame.Commands
{
    public class AddWordCommand : AddItemCommand<string>
    {
        private IGameInputValidator validator;
        private PlayerInputViewModel viewModel;

        public AddWordCommand(PlayerInputViewModel viewModel, IGameInputValidator validator) :
            base(viewModel.Words, word => validator.IsValid(word, viewModel.Words), validator.WordsRequired)
        {
            this.viewModel = viewModel;
            this.validator = validator;
        }

        public override void Execute(object parameter)
        {
            base.Execute(parameter);
            viewModel.ResetCurrentWord();
        }
    }
}
