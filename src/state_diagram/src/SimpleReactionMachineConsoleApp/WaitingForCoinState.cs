using System;

namespace SimpleReactionMachine
{
    public class WaitingForCoinState : IReactionMachineState
    {
        private SimpleReactionController _controller;

        public WaitingForCoinState(SimpleReactionController controller)
        {
            _controller = controller;
        }

        public void Init()
        {
            _controller.Gui.SetDisplay("Insert coin");
        }

        public void CoinInserted()
        {
            _controller.SetState(new WaitingForGoState(_controller));
            _controller.Gui.SetDisplay("Press GO!");
        }

        public void GoStopPressed()
        {
            // Do nothing
        }

        public void Tick()
        {
            // Do nothing
        }
    }
}