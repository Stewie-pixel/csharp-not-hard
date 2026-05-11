namespace EnhancedSimpleReactionMachine
{
    public class EnhancedDisplayingAverageState : IState
    {
        private EnhancedReactionController _controller;
        private int _displayCountdown; // 5 seconds = 500 ticks

        public EnhancedDisplayingAverageState(EnhancedReactionController controller)
        {
            _controller = controller;
            _displayCountdown = 500;
            // Cheating is handled earlier, so this state only displays average
            _controller.Gui.SetDisplay($"Average = {_controller.CalculateAverageReactionTime():F2}");
        }

        public void Init()
        {
            // Nothing specific to init beyond constructor
        }

        public void CoinInserted()
        {
            // Do nothing
        }

        public void GoStopPressed()
        {
            // If Go/Stop pressed during display, immediately move to game over
            _controller.SetState(new EnhancedGameOverDisplayState(_controller, "Game Over"));
        }

        public void Tick()
        {
            _displayCountdown--;

            if (_displayCountdown <= 0)
            {
                _controller.SetState(new EnhancedGameOverDisplayState(_controller, "Game Over"));
            }
        }
    }
}
