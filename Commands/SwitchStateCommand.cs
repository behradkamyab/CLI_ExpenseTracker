using ExpenseTracker.Interfaces;


namespace ExpenseTracker.Commands
{
    public class SwitchStateCommand : ICommand
    {
        private readonly StateManager _stateManager;
        private IState _newState;


        public SwitchStateCommand(StateManager stateManager, IState newState)
        {
            _stateManager = stateManager;
            _newState = newState;
        }
        public void Execute()
        {
            _stateManager.SwitchState(_newState);
        }
    }
}
