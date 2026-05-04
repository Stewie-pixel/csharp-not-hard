using System;

namespace SimpleReactionMachine
{
    public class DisplayingReactionTimeState : IReactionMachineState
    {
        private SimpleReactionController _controller;
        private int _reactionTimeTicks;
        private int _displayDurationTicks;

        public DisplayingReactionTimeState(SimpleReactionController controller, int reactionTimeTicks)
        {
            _controller = controller;
            _reactionTimeTicks = reactionTimeTicks;
            _displayDurationTicks = 0;
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
            _controller.Gui.SetDisplay("Insert coin");
            _controller.SetState(new WaitingForCoinState(_controller));
        }

        public void Tick()
        {
            _displayDurationTicks++;
            if (_displayDurationTicks >= 300) // 3 seconds
            {
                _controller.Gui.SetDisplay("Insert coin");
                _controller.SetState(new WaitingForCoinState(_controller));
            }
        }

        private string FormatTime(int ticks)
        {
            return (ticks * 0.01).ToString("F2");
        }
    }
}