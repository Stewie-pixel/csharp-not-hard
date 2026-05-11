using System;

namespace TheAccountClass
{
    public class DepositTransaction : Transaction
    {
        private Account _account;

        public DepositTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
        }

        public override void Print()
        {
            base.Print(); // Call base Print to show common transaction details
            Console.WriteLine($"Account Name: {_account.Name}");

            // Print human‑readable status
            if (Executed)
            {
                if (Success)
                    Console.WriteLine("Status: Deposit successfully completed.");
                else
                    Console.WriteLine("Status: Deposit failed.");
            }

            if (Reversed)
                Console.WriteLine("Status: Transaction successfully reversed.");
        }

        public override void Execute()
        {
            if (_amount <= 0)
                throw new InvalidOperationException("Deposit amount must be positive.");

            base.Execute(); // sets _executed, _dateStamp; guards double-execution

            if (_account.Deposit(_amount))
            {
                _success = true;
            }
            else
            {
                _success = false;
                throw new InvalidOperationException("Deposit failed for an unknown reason.");
            }
        }

        public override void Rollback()
        {
            base.Rollback(); // guards not-executed, double-rollback, failed; sets _dateStamp

            if (_account.Withdraw(_amount))
            {
                _reversed = true;
            }
            else
            {
                throw new InvalidOperationException(
                    "Failed to withdraw funds back from the account during rollback (insufficient funds?)."
                );
            }
        }
    }
}
