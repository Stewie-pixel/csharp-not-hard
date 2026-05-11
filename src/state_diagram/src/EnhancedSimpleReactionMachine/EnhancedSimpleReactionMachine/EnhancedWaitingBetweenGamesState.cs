namespace EnhancedSimpleReactionMachine
{
    public class EnhancedWaitingBetweenGamesState : IState
    {
        private EnhancedReactionController _controller;
        private int _randomDelayTicks;
        private int _currentDelayTicks;

        public EnhancedWaitingBetweenGamesState(EnhancedReactionController controller)
        {
            _controller = controller;
            _randomDelayTicks = _controller.Rng.GetRandom(50, 150); // 0.5 to 1.5 seconds (50-150 ticks)
            _currentDelayTicks = 0;
            _controller.Gui.SetDisplay("Wait...");
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
            // Do nothing, or perhaps immediately move to next game if desired (not specified)
            // For now, it just waits.
        }

        public void Tick()
        {
            _currentDelayTicks++;

            if (_currentDelayTicks >= _randomDelayTicks)
            {
                _controller.SetState(new EnhancedWaitingForGoState(_controller));
                _controller.Gui.SetDisplay("Press GO!"); // Set initial display for the next state
            }
        }
    }
}
