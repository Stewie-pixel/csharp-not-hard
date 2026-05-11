namespace SimpleReactionMachine
{
    public class WaitingForGoState : StateBase
    {
        public WaitingForGoState(
            EnhancedReactionController controller
        ) : base(controller)
        {
        }

        public override void Tick()
        {
            controller.ticks++;

            if (controller.ticks >= 1000)
            {
                controller.gui.SetDisplay("Insert coin");

                controller.SetState(
                    controller.waitingForCoinState
                );
            }
        }

        public override void GoStopPressed()
        {
            controller.ticks = 0;

            controller.waitTime =
                controller.random.GetRandom(100, 251);

            controller.gui.SetDisplay("Wait...");

            controller.SetState(
                controller.waitingForRandomState
            );
        }
    }
}
