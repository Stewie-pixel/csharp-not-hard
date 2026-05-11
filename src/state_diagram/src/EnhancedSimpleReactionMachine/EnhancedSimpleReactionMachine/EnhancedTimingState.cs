namespace EnhancedSimpleReactionMachine
{
    public class EnhancedTimingState : IState
    {
        private EnhancedReactionController _controller;
        private int _reactionTimeTicks;

        public EnhancedTimingState(EnhancedReactionController controller)
        {
            _controller = controller;
            _reactionTimeTicks = 0;
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
            _controller.RecordReactionTime(_reactionTimeTicks);
            _controller.IncrementGamesPlayed();
            _controller.SetState(new EnhancedDisplayingReactionTimeState(_controller, _reactionTimeTicks));
        }

        public void Tick()
        {
            _reactionTimeTicks++;
            _controller.Gui.SetDisplay(FormatTime(_reactionTimeTicks));

            // Max reaction time, effectively a timeout if not pressed
            if (_reactionTimeTicks >= 200) // 2 seconds
            {
                _controller.RecordReactionTime(_reactionTimeTicks);
                _controller.IncrementGamesPlayed();
                _controller.SetState(new EnhancedDisplayingReactionTimeState(_controller, _reactionTimeTicks));
            }
        }

        private string FormatTime(int ticks)
        {
            return (ticks * 0.01).ToString("F2");
        }
    }
}
