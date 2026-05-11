namespace SimpleReactionMachine
{
    public class WaitingForCoinState : StateBase
    {
        public WaitingForCoinState(
            EnhancedReactionController controller
        ) : base(controller)
        {
        }

        public override void CoinInserted()
        {
            controller.gamesPlayed = 0;
            controller.totalReactionTime = 0;
            controller.ticks = 0;

            controller.gui.SetDisplay("Press GO!");

            controller.SetState(
                controller.waitingForGoState
            );
        }
    }
}
