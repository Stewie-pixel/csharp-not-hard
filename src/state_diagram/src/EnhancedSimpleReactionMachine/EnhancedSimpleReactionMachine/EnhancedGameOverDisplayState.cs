namespace EnhancedSimpleReactionMachine
{
    public class EnhancedGameOverDisplayState : IState
    {
        private EnhancedReactionController _controller;
        private string _messageToDisplay;
        private int _displayTicks; // Display for 1 tick before transitioning

        public EnhancedGameOverDisplayState(EnhancedReactionController controller, string message)
        {
            _controller = controller;
            _messageToDisplay = message;
            _displayTicks = 1;
        }

        public void Init()
        {
            _controller.Gui.SetDisplay(_messageToDisplay);
        }

        public void CoinInserted()
        {
            // Ignore coin during game over display
        }

        public void GoStopPressed()
        {
            // Allow immediate transition to WaitingForCoinState if button is pressed
            _controller.SetState(new EnhancedWaitingForCoinState(_controller));
        }

        public void Tick()
        {
            _displayTicks--;
            if (_displayTicks <= 0)
            {
                _controller.SetState(new EnhancedWaitingForCoinState(_controller));
            }
        }
    }
}
