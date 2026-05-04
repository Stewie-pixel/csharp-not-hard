using System;

namespace SimpleReactionMachine
{
    public class WaitingForGoState : IReactionMachineState
    {
        private SimpleReactionController _controller;

        public WaitingForGoState(SimpleReactionController controller)
        {
            _controller = controller;
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
            _controller.SetState(new WaitingForReleaseState(_controller));
            _controller.Gui.SetDisplay("Wait...");
        }

        public void Tick()
        {
            // Do nothing
        }
    }
}