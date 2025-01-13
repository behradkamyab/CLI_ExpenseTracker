using ExpenseTracker.Interfaces;


namespace ExpenseTracker
{
    public class StateManager
    {
        private IState _initialState;

        public StateManager()
        {

        }

        public void Run(IState initialState)
        {
            _initialState = initialState;
            while (true)
            {
                _initialState.Render();
                var command = _initialState.GetCommand();
                command.Execute();

            }
        }

        public void SwitchState(IState state)
        {
            _initialState = state;
        }
    }
}
