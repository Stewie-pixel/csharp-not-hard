using System;

namespace EnhancedSimpleReactionMachine
{
    public class EnhancedReactionController : IController
    {
        private IGui gui;
        private IRandom random;

        private IState state;

        internal int ticks;
        internal int waitTime;

        internal int gamesPlayed;
        internal double totalReactionTime;
        internal double currentReactionTime;

        /// <summary>
        /// Connects the controller to the GUI and random number generator dependencies.
        /// </summary>
        public void Connect(IGui gui, IRandom random)
        {
            this.gui = gui;
            this.random = random;
        }

        /// <summary>
        /// Initialises the controller to its default state, resetting all counters
        /// and transitioning to the WaitingForCoin state.
        /// </summary>
        public void Init()
        {
            gamesPlayed = 0;
            totalReactionTime = 0;
            ticks = 0;

            state = new WaitingForCoinState(this);
            gui.SetDisplay("Insert coin");
        }

        /// <summary>
        /// Delegates the coin insertion event to the current state handler.
        /// </summary>
        public void CoinInserted() => state.CoinInserted();

        /// <summary>
        /// Delegates the GO/STOP button press event to the current state handler.
        /// </summary>
        public void GoStopPressed() => state.GoStopPressed();

        /// <summary>
        /// Delegates each clock tick event to the current state handler.
        /// </summary>
        public void Tick() => state.Tick();

        private interface IState
        {
            void CoinInserted();
            void GoStopPressed();
            void Tick();
        }

        /// <summary>
        /// State: Machine is idle, waiting for the player to insert a coin.
        /// </summary>
        private class WaitingForCoinState : IState
        {
            private EnhancedReactionController cointroller;

            public WaitingForCoinState(EnhancedReactionController cointroller)
            {
                this.cointroller = cointroller;
            }

            /// <summary>
            /// Resets session stats and transitions to WaitingForGo when a coin is inserted.
            /// </summary>
            public void CoinInserted()
            {
                cointroller.gamesPlayed = 0;
                cointroller.totalReactionTime = 0;
                cointroller.ticks = 0;

                cointroller.gui.SetDisplay("Press GO!");
                cointroller.state = new WaitingForGoState(cointroller);
            }

            public void GoStopPressed() { }
            public void Tick() { }
        }

        /// <summary>
        /// State: Coin has been inserted, waiting for the player to press GO.
        /// Times out after 1000 ticks (10 seconds) and returns to WaitingForCoin.
        /// </summary>
        private class WaitingForGoState : IState
        {
            private EnhancedReactionController cointroller;

            public WaitingForGoState(EnhancedReactionController cointroller)
            {
                this.cointroller = cointroller;
            }

            /// <summary>
            /// Increments the tick counter and returns to WaitingForCoin if the timeout is exceeded.
            /// </summary>
            public void Tick()
            {
                cointroller.ticks++;

                if (cointroller.ticks >= 1000)
                {
                    cointroller.gui.SetDisplay("Insert coin");
                    cointroller.state = new WaitingForCoinState(cointroller);
                }
            }

            /// <summary>
            /// Starts a round by generating a random wait time and transitioning to WaitingForRandom.
            /// </summary>
            public void GoStopPressed()
            {
                cointroller.ticks = 0;
                cointroller.waitTime = cointroller.random.GetRandom(100, 251);

                cointroller.gui.SetDisplay("Wait...");
                cointroller.state = new WaitingForRandomState(cointroller);
            }

            public void CoinInserted() { }
        }

        /// <summary>
        /// State: Waiting for the randomly determined delay to expire before starting timing.
        /// Pressing GO/STOP during this state is treated as a false start — resets to WaitingForCoin.
        /// </summary>
        private class WaitingForRandomState : IState
        {
            private EnhancedReactionController cointroller;

            public WaitingForRandomState(EnhancedReactionController cointroller)
            {
                this.cointroller = cointroller;
            }

            /// <summary>
            /// Increments ticks and transitions to TimingState once the random wait time has elapsed.
            /// </summary>
            public void Tick()
            {
                cointroller.ticks++;

                if (cointroller.ticks >= cointroller.waitTime)
                {
                    cointroller.ticks = 0;
                    cointroller.state = new TimingState(cointroller);
                }
            }

            /// <summary>
            /// Handles a false start — penalises the player by returning to WaitingForCoin.
            /// </summary>
            public void GoStopPressed()
            {
                cointroller.gui.SetDisplay("Insert coin");
                cointroller.state = new WaitingForCoinState(cointroller);
            }

            public void CoinInserted() { }
        }

        /// <summary>
        /// State: The reaction timer is actively running. The display updates each tick
        /// and the round ends either when the player presses STOP or the 2-second limit is reached.
        /// </summary>
        private class TimingState : IState
        {
            private EnhancedReactionController cointroller;

            public TimingState(EnhancedReactionController cointroller)
            {
                this.cointroller = cointroller;
            }

            /// <summary>
            /// Increments the timer display each tick (1 tick = 0.01s) and auto-finishes if 2.0s is exceeded.
            /// </summary>
            public void Tick()
            {
                cointroller.ticks++;

                cointroller.currentReactionTime = cointroller.ticks * 0.01;

                cointroller.gui.SetDisplay(cointroller.currentReactionTime.ToString("F2"));

                if (cointroller.currentReactionTime >= 2.0)
                {
                    FinishRound();
                }
            }

            /// <summary>
            /// Records the player's reaction time and ends the round when STOP is pressed.
            /// </summary>
            public void GoStopPressed()
            {
                FinishRound();
            }

            /// <summary>
            /// Accumulates the reaction time, increments the games-played counter,
            /// and transitions to ShowingResult.
            /// </summary>
            private void FinishRound()
            {
                cointroller.totalReactionTime += cointroller.currentReactionTime;
                cointroller.gamesPlayed++;

                cointroller.ticks = 0;

                cointroller.state = new ShowingResultState(cointroller);
            }

            public void CoinInserted() { }
        }

        /// <summary>
        /// State: Displays the result of the most recent round for 3 seconds (300 ticks).
        /// After 3 rounds, transitions to ShowingAverage; otherwise starts a new round.
        /// </summary>
        private class ShowingResultState : IState
        {
            private EnhancedReactionController cointroller;

            /// <summary>
            /// Advances to the next round or to the average display, depending on games played.
            /// </summary>
            private void MoveNext()
            {
                cointroller.ticks = 0;

                if (cointroller.gamesPlayed >= 3)
                {
                    double avg = cointroller.totalReactionTime / 3.0;

                    cointroller.gui.SetDisplay($"Average = {avg:F2}");
                    cointroller.state = new ShowingAverageState(cointroller);
                }
                else
                {
                    cointroller.waitTime = cointroller.random.GetRandom(100, 251);
                    cointroller.gui.SetDisplay("Wait...");
                    cointroller.state = new WaitingForRandomState(cointroller);
                }
            }

            public ShowingResultState(EnhancedReactionController cointroller)
            {
                this.cointroller = cointroller;
            }

            /// <summary>
            /// Automatically advances after 300 ticks (3 seconds) if the player doesn't press GO/STOP.
            /// </summary>
            public void Tick()
            {
                cointroller.ticks++;

                if (cointroller.ticks >= 300)
                {
                    MoveNext();
                }
            }

            /// <summary>
            /// Allows the player to manually skip the result display by pressing GO/STOP.
            /// </summary>
            public void GoStopPressed()
            {
                MoveNext();
            }

            public void CoinInserted() { }
        }

        /// <summary>
        /// State: Displays the average reaction time across all 3 rounds for 5 seconds (500 ticks),
        /// then resets the machine back to WaitingForCoin.
        /// </summary>
        private class ShowingAverageState : IState
        {
            private EnhancedReactionController cointroller;

            public ShowingAverageState(EnhancedReactionController cointroller)
            {
                this.cointroller = cointroller;
            }

            /// <summary>
            /// Automatically resets the game after 500 ticks (5 seconds).
            /// </summary>
            public void Tick()
            {
                cointroller.ticks++;

                if (cointroller.ticks >= 500)
                {
                    ResetGame();
                }
            }

            /// <summary>
            /// Allows the player to manually skip the average display and reset immediately.
            /// </summary>
            public void GoStopPressed()
            {
                ResetGame();
            }

            /// <summary>
            /// Resets all session data and returns the machine to its initial WaitingForCoin state.
            /// </summary>
            private void ResetGame()
            {
                cointroller.ticks = 0;
                cointroller.gamesPlayed = 0;
                cointroller.totalReactionTime = 0;

                cointroller.gui.SetDisplay("Insert coin");
                cointroller.state = new WaitingForCoinState(cointroller);
            }

            public void CoinInserted() { }
        }
    }
}
