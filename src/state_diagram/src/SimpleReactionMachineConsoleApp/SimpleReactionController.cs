using System;

namespace SimpleReactionMachine
{
    public class SimpleReactionController : IController
    {
        private IGui gui;
        private IRandom random;

        private IState state;

        internal int ticks;
        internal int waitTime;

        public void Connect(IGui gui, IRandom random)
        {
            this.gui = gui;
            this.random = random;
        }

        public void Init()
        {
            state = new WaitingForCoinState(this);
            gui.SetDisplay("Insert coin");
        }

        public void CoinInserted() => state.CoinInserted();
        public void GoStopPressed() => state.GoStopPressed();
        public void Tick() => state.Tick();

        private interface IState
        {
            void CoinInserted();
            void GoStopPressed();
            void Tick();
        }
/// <summary>
/// Represents the state where the machine is waiting for a coin to be inserted.
/// </summary>
        private class WaitingForCoinState : IState
        {
            private SimpleReactionController coin;

            public WaitingForCoinState(SimpleReactionController coin)
            {
                this.coin = coin;
            }

            public void CoinInserted()
            {
                coin.gui.SetDisplay("Lets GO!");
                coin.state = new WaitingForGoState(coin);
            }

            public void GoStopPressed() { }
            public void Tick() { }
        }
/// <summary>
/// Represents the state where the machine is waiting for the player to press the Go/Stop button after a coin has been inserted.
/// </summary>
        private class WaitingForGoState : IState
        {
            private SimpleReactionController coin;

            public WaitingForGoState(SimpleReactionController coin)
            {
                this.coin = coin;
            }

            public void GoStopPressed()
            {
                coin.ticks = 0;
                coin.waitTime = coin.random.GetRandom(100, 251);
                coin.gui.SetDisplay("Wait...");
                coin.state = new WaitingForRandomState(coin);
            }

            public void CoinInserted() { }
            public void Tick() { }
        }
/// <summary>
/// Represents the state where the machine is waiting for a random amount of time before allowing the player to press the Go/Stop button again.
/// </summary>
        private class WaitingForRandomState : IState
        {
            private SimpleReactionController coin;

            public WaitingForRandomState(SimpleReactionController coin)
            {
                this.coin = coin;
            }

            public void Tick()
            {
                coin.ticks++;
                if (coin.ticks >= coin.waitTime)
                {
                    coin.ticks = 0;
                    coin.state = new TimingState(coin);
                }
            }

            public void GoStopPressed()
            {
                coin.gui.SetDisplay("Insert coin");
                coin.state = new WaitingForCoinState(coin);
            }

            public void CoinInserted() { }
        }
/// <summary>
/// Represents the state where the machine is timing how long it takes for the player to press the Go/Stop button after the random wait time has elapsed.
/// </summary>
        private class TimingState : IState
        {
            private SimpleReactionController coin;

            public TimingState(SimpleReactionController coin)
            {
                this.coin = coin;
            }

            public void Tick()
            {
                coin.ticks++;
                double time = coin.ticks * 0.01;
                coin.gui.SetDisplay(time.ToString("F2"));

                if (time >= 2.0)
                {
                    coin.ticks = 0;
                    coin.state = new ShowingResultState(coin);
                }
            }

            public void GoStopPressed()
            {
                coin.ticks = 0;
                coin.state = new ShowingResultState(coin);
            }

            public void CoinInserted() { }
        }
/// <summary>
/// Represents the state where the machine is showing the player's reaction time result after they have pressed the Go/Stop button or after 2 seconds have elapsed in the TimingState.
/// </summary>
        private class ShowingResultState : IState
        {
            private SimpleReactionController coin;

            public ShowingResultState(SimpleReactionController coin)
            {
                this.coin = coin;
            }

            public void Tick()
            {
                coin.ticks++;
                if (coin.ticks >= 300)
                {
                    coin.gui.SetDisplay("Insert coin");
                    coin.state = new WaitingForCoinState(coin);
                }
            }

            public void GoStopPressed()
            {
                coin.gui.SetDisplay("Insert coin");
                coin.state = new WaitingForCoinState(coin);
            }

            public void CoinInserted() { }
        }
    }
}