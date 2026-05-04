using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleReactionMachine
{
    public class SimpleReactionController : IController
    {
        public IGui Gui { get; private set; }
        public IRandom Rng { get; private set; }

        private IReactionMachineState _currentState;

        public void Connect(IGui gui, IRandom rng)
        {
            Gui = gui;
            Rng = rng;
        }

        public void SetState(IReactionMachineState newState)
        {
            _currentState = newState;
        }

        public void Init()
        {
            SetState(new WaitingForCoinState(this)); // Initial state
            _currentState.Init();
        }

        public void CoinInserted()
        {
            _currentState.CoinInserted();
        }

        public void GoStopPressed()
        {
            _currentState.GoStopPressed();
        }

        public void Tick()
        {
            _currentState.Tick();
        }
    }
}