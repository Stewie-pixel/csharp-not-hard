namespace SimpleReactionMachine
{
    public interface IReactionMachineState
    {
        void Init();
        void CoinInserted();
        void GoStopPressed();
        void Tick();
    }
}