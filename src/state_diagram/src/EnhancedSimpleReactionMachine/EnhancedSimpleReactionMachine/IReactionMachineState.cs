namespace EnhancedSimpleReactionMachine
{
    public interface IState
    {
        void Init();
        void CoinInserted();
        void GoStopPressed();
        void Tick();
    }
}
