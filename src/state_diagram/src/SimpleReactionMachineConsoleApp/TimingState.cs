using System;

namespace SimpleReactionMachine
{
    public class TimingState : IReactionMachineState
    {
        private SimpleReactionController _controller;
        private int _reactionTimeTicks;

        public TimingState(SimpleReactionController controller)
        {
            _controller = controller;
            _reactionTimeTicks = 0;
            _controller.Gui.SetDisplay(FormatTime(_reactionTimeTicks));
        }

        public void Init()
        {
            // Do nothing
        }

        public void CoinInserted()
        {
            // Do nothing
        }

        public void GoStopPressed()
        {
            _controller.SetState(new DisplayingReactionTimeState(_controller, _reactionTimeTicks));
        }

        public void Tick()
        {
            _reactionTimeTicks++;
            _controller.Gui.SetDisplay(FormatTime(_reactionTimeTicks));

            if (_reactionTimeTicks >= 200) // 2 seconds
            {
                _controller.SetState(new DisplayingReactionTimeState(_controller, _reactionTimeTicks));
            }
        }

        private string FormatTime(int ticks)
        {
            return (ticks * 0.01).ToString("F2");
        }
    }
}