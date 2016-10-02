using AssociationGame.WPF;
using Game.Core.Validation;

namespace AssociationGame.Commands
{
    public class AddWordCommand : AddItemCommand<string>
    {
        private GameInputValidator validator;
        private PlayerInputViewModel viewModel;

        public AddWordCommand(PlayerInputViewModel viewModel, GameInputValidator validator) :
            base(viewModel.Words, word => validator.IsValid(word, viewModel.Words), validator.WordLimit)
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
