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

// Connect the controller with the GUI and the Random Generator
        public void Connect(IGui gui, IRandom rng)
        {
            Gui = gui;
            Rng = rng;
        }

// Method to change the current state of the controller
        public void SetState(IReactionMachineState newState)
        {
            _currentState = newState;
        }

// Initialize the controller to set its initial state
        public void Init()
        {
            SetState(new WaitingForCoinState(this)); // Initial state
            _currentState.Init();
        }

// Methods to handle user actions and timer ticks
        public void CoinInserted()
        {
            _currentState.CoinInserted();
        }

// Method to handle the Go/Stop button press
        public void GoStopPressed()
        {
            _currentState.GoStopPressed();
        }

// Method to handle timer ticks
        public void Tick()
        {
            _currentState.Tick();
        }
    }
}