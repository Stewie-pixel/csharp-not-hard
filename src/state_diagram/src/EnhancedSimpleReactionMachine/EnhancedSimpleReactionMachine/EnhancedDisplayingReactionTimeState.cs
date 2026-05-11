namespace EnhancedSimpleReactionMachine
{
    public class EnhancedDisplayingReactionTimeState : IState
    {
        private EnhancedReactionController _controller;
        private int _reactionTimeTicks;
        private int _displayCountdown; // 3 seconds = 300 ticks

        public EnhancedDisplayingReactionTimeState(EnhancedReactionController controller, int reactionTimeTicks)
        {
            _controller = controller;
            _reactionTimeTicks = reactionTimeTicks;
            _displayCountdown = 300;
            _controller.Gui.SetDisplay(FormatTime(_reactionTimeTicks));
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
            // If Go/Stop pressed during display, immediately move to next stage
            TransitionAfterDisplay();
        }

        public void Tick()
        {
            _displayCountdown--;

            if (_displayCountdown <= 0)
            {
                TransitionAfterDisplay();
            }
        }

        private void TransitionAfterDisplay()
        {
            if (_controller.GetGamesPlayed() < 3)
            {
                _controller.SetState(new EnhancedWaitingBetweenGamesState(_controller));
            }
            else
            {
                _controller.SetState(new EnhancedDisplayingAverageState(_controller));
            }
        }

        private string FormatTime(int ticks)
        {
            return (ticks * 0.01).ToString("F2");
        }
    }
}
