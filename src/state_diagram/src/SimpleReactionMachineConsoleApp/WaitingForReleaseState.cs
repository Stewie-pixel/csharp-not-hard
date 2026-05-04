using System;

namespace SimpleReactionMachine
{
    public class WaitingForReleaseState : IReactionMachineState
    {
        private SimpleReactionController _controller;
        private int _randomDelayTicks;
        private int _currentTickCount;

        public WaitingForReleaseState(SimpleReactionController controller)
        {
            _controller = controller;
            _currentTickCount = 0;
            // Generate a random delay between 1.0 and 2.5 seconds (100 to 250 ticks, as each tick is 10ms)
            _randomDelayTicks = _controller.Rng.GetRandom(100, 250);
        }

        public void Init()
        {
            // Display "Wait..." is handled when entering this state from GoStopPressed in WaitingForGoState
        }

        public void CoinInserted()
        {
            // Do nothing
        }

        public void GoStopPressed()
        {
            // Player "cheated" by pressing too early
            _controller.Gui.SetDisplay("Insert coin");
            _controller.SetState(new WaitingForCoinState(_controller));
        }

        public void Tick()
        {
            _currentTickCount++;
            if (_currentTickCount >= _randomDelayTicks)
            {
                _controller.SetState(new TimingState(_controller));
            }
        }
    }
}