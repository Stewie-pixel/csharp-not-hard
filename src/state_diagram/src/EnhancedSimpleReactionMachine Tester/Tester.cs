using System;

namespace SimpleReactionMachine
{
    class Tester
    {
        private static IController Controller;
        private static IGui Gui;
        private static string DisplayText;
        private static int RandomNumber;
        private static int Passed = 0;

        public static void Main(string[] args)
        {
            EnhancedTest();
            Console.WriteLine("\n=====================================\nSummary: {0} tests passed out of 52", Passed);
            Console.ReadKey();
        }

        private static void EnhancedTest()
        {
            // Construct and wire up the EnhancedReactionController
            Controller = new EnhancedReactionController();
            gui = new DummyGui();
            gui.Connect(Controller);
            Controller.Connect(gui, new RndGenerator());
            gui.Init();

            // SECTION 1: IDLE STATE (WaitingForCoin)
            // Verifies the machine initialises correct and ignores
            // any inputs that are invalid while waiting for a coin.

            // Test A: After Init(), display must show "Insert coin"
            DoReset('A', Controller, "Insert coin");

            // Test B: A tick while idle must have no effect on the display
            DoTicks('B', Controller, 1, "Insert coin");

            // Test C: Pressing GO/STOP while idle must be completely ignored
            DoGoStop('C', Controller, "Insert coin");

            // Test D: Inserting a coin must transition display to "Press GO!"
            DoInsertCoin('D', Controller, "Press GO!");

            // Test E: A tick while waiting for GO must not affect the display
            DoTicks('E', Controller, 1, "Press GO!");

            // Test F: Inserting a second coin while in WaitingForGo must be ignored
            DoInsertCoin('F', Controller, "Press GO!");

            // SECTION 2: WAITINGFORGO TIMEOUT
            // If the player does not press GO within 1000 ticks (10 seconds),
            // the machine must reset to idle.
            // (1 tick was already consumed in Test E, so 998 more = 999 total)

            // Test G: At 999 total ticks the machine must still display "Press GO!"
            DoTicks('G', Controller, 998, "Press GO!");

            // Test H: The 1000th tick must trigger timeout and return to idle
            DoTicks('H', Controller, 1, "Insert coin");

            // SECTION 3: COMPLETE 3-ROUND SESSION — MANUAL STOPS
            // Plays a full session of three rounds, each stopped manually.
            // Verifies per-round timing, result display, auto-advance, and
            // the correct final average.
            //   Round 1: 0.50s | Round 2: 0.75s | Round 3: 1.00s | Average: 0.75s

            // Setup: insert coin and begin session
            DoInsertCoin('I', Controller, "Press GO!");

            RandomNumber = 150; // controlled wait delay: 150 ticks = 1.50s
            DoGoStop('J', controller, "Wait...");

            // Test K: Ticks within the random wait window must not change the display
            DoTicks('K', Controller, randomNumber - 1, "Wait...");

            // Test L: The 150th (final) wait tick must start the reaction timer at "0.00"
            DoTicks('L', Controller, 1, "0.00");

            // Test M: One reaction tick must display "0.01"
            DoTicks('M', Controller, 1, "0.01");

            // Test N: 49 more ticks (50 total) must display "0.50"
            DoTicks('N', Controller, 49, "0.50");

            // Test O: Pressing STOP must freeze the display at the current reaction time
            DoGoStop('O', Controller, "0.50");

            // Test P: 299 ticks into ShowingResult must keep the result on screen
            DoTicks('P', Controller, 299, "0.50");

            // Test Q: The 300th tick must auto-advance to round 2 ("Wait...")
            RandomNumber = 120;
            DoTicks('Q', controller, 1, "Wait...");

            // Round 2 — reaction time target: 0.75s
            DoTicks('R', Controller, randomNumber, "0.00");
            DoTicks('S', Controller, 75, "0.75");

            // Test T: Pressing STOP in round 2 must display "0.75"
            DoGoStop('T', Controller, "0.75");

            // Test U: Pressing GO/STOP in ShowingResult must skip the delay and start round 3
            RandomNumber = 130;
            DoGoStop('U', controller, "Wait...");

            // Round 3 — reaction time target: 1.00s
            DoTicks('V', Controller, randomNumber, "0.00");
            DoTicks('W', Controller, 100, "1.00");

            // Test X: Pressing STOP in round 3 must display "1.00"
            DoGoStop('X', Controller, "1.00");

            // Test Y: After the 3rd round result display, the average must be shown
            // (0.50 + 0.75 + 1.00) / 3 = 0.75 "Average = 0.75"
            DoTicks('Y', Controller, 300, "Average = 0.75");

            // Test Z: At 499 ticks in ShowingAverage the average must still be visible
            DoTicks('Z', Controller, 499, "Average = 0.75");

            // Test a: The 500th tick must auto-reset the machine to idle
            DoTicks('a', Controller, 1, "Insert coin");

            // SECTION 4: FALSE START PENALTY
            // Pressing GO/STOP during the random wait period is a false start.
            // The machine must immediately return to idle (coin is lost).

            DoInsertCoin('b', Controller, "Press GO!");
            RandomNumber = 200;
            DoGoStop('c', controller, "Wait...");

            // Test d: Pressing STOP during the wait period must trigger a false start
            DoGoStop('d', Controller, "Insert coin");

            // Test e: After a false start, a tick must confirm the machine is idle
            DoTicks('e', Controller, 1, "Insert coin");

            // SECTION 5: REACTION TIMER AUTO-TIMEOUT AT 2.00 SECONDS
            // If the player does not stop within 2 seconds, the round must end
            // automatically with the time capped and recorded as 2.00s.

            DoInsertCoin('f', Controller, "Press GO!");
            RandomNumber = 100;
            DoGoStop('g', controller, "Wait...");
            DoTicks('h', controller, RandomNumber, "0.00");

            // Test i: At 199 reaction ticks (1.99s) the timer must still be running
            DoTicks('i', Controller, 199, "1.99");

            // Test j: The 200th tick (2.00s) must auto-finish the round
            DoTicks('j', Controller, 1, "2.00");

            // Test k: Machine must be in ShowingResult state (display unchanged on next tick)
            DoTicks('k', Controller, 1, "2.00");

            // SECTION 6: SKIP AVERAGE DISPLAY VIA GO/STOP
            // Pressing GO/STOP during the average display must reset the machine
            // to idle immediately without waiting for the 500-tick timeout.
            // Rounds: 2.00 (auto) + 0.80 + 0.60 Average = 1.13

            // Round 2 (continuing from section 5's auto-timeout round 1)
            RandomNumber = 110;
            DoGoStop('l', controller, "Wait...");
            DoTicks('m', controller, RandomNumber, "0.00");
            DoTicks('n', Controller, 80, "0.80");
            DoGoStop('o', Controller, "0.80");

            // Test p: Pressing GO/STOP in ShowingResult must immediately start round 3
            RandomNumber = 115;
            DoGoStop('p', controller, "Wait...");

            // Round 3
            DoTicks('q', Controller, randomNumber, "0.00");
            DoTicks('r', Controller, 60, "0.60");
            DoGoStop('s', Controller, "0.60");

            // Test t: After 300 ticks in ShowingResult, average must display correctly
            // (2.00 + 0.80 + 0.60) / 3 = 1.1333... "Average = 1.13"
            DoTicks('t', Controller, 300, "Average = 1.13");

            // Test u: Pressing GO/STOP during average display must reset to idle instantly
            DoGoStop('u', Controller, "Insert coin");

            // SECTION 7: Init() RESETS FROM VARIOUS STATES
            // Calling Init() must unconditionally return the machine to idle,
            // discarding all in-progress session data.

            // Tests v–w: Init() resets from WaitingForGo state
            DoInsertCoin('v', Controller, "Press GO!");
            DoReset('w', Controller, "Insert coin");

            // Tests x–z: Init() resets from WaitingForRandom state
            DoInsertCoin('x', Controller, "Press GO!");
            RandomNumber = 140;
            DoGoStop('y', controller, "Wait...");
            DoReset('z', controller, "Insert coin");
        }

        // Calls Init() and checks the resulting display
        private static void DoReset(char ch, IController controller, string msg)
        {
            try
            {
                controller.Init();
                GetMessage(ch, msg);
            }
            catch (Exception exception)
            {
                Console.WriteLine("test {0}: failed with exception ({1})", ch, exception.Message);
            }
        }

        // Calls GoStopPressed() and checks the resulting display
        private static void DoGoStop(char ch, IController controller, string msg)
        {
            try
            {
                controller.GoStopPressed();
                GetMessage(ch, msg);
            }
            catch (Exception exception)
            {
                Console.WriteLine("test {0}: failed with exception ({1})", ch, exception.Message);
            }
        }

        // Calls CoinInserted() and checks the resulting display
        private static void DoInsertCoin(char ch, IController controller, string msg)
        {
            try
            {
                controller.CoinInserted();
                GetMessage(ch, msg);
            }
            catch (Exception exception)
            {
                Console.WriteLine("test {0}: failed with exception ({1})", ch, exception.Message);
            }
        }

        // Fires n ticks and checks the resulting display after the last one
        private static void DoTicks(char ch, IController controller, int n, string msg)
        {
            try
            {
                for (int t = 0; t < n; t++)
                {
                    controller.Tick();
                }
                GetMessage(ch, msg);
            }
            catch (Exception exception)
            {
                Console.WriteLine("test {0}: failed with exception ({1})", ch, exception.Message);
            }
        }

        // Compares the current display text against the expected message (case-insensitive)
        private static void GetMessage(char ch, string msg)
        {
            if (string.Equals(msg, DisplayText, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("test {0}: passed successfully", ch);
                Passed++;
            }
            else
            {
                Console.WriteLine("test {0}: failed (expected '{1}' | received '{2}')", ch, msg, DisplayText);
            }
        }

        // Minimal GUI stub that captures whatever SetDisplay() is given
        private class DummyGui : IGui
        {
            private IController controller;

            public void Connect(IController controller)
            {
                this.controller = controller;
            }

            public void Init()
            {
                DisplayText = "Insert coin";
            }

            public void SetDisplay(string msg)
            {
                DisplayText = msg;
            }
        }

        // Deterministic random stub — always returns the current value of randomNumber
        private class RndGenerator : IRandom
        {
            public int GetRandom(int from, int to)
            {
                return RandomNumber;
            }
        }
    }
}
