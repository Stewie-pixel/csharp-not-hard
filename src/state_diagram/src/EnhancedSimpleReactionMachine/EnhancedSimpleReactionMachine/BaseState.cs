namespace EnhancedSimpleReactionMachine
{
    public abstract class StateBase : IState
    {
        protected EnhancedReactionController controller;

        public StateBase(EnhancedReactionController controller)
        {
            this.controller = controller;
        }

        public virtual void CoinInserted() { }

        public virtual void GoStopPressed() { }

        public virtual void Tick() { }
    }
}
