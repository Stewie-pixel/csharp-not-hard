using System;

namespace TheAccountClass
{
    public class WithdrawTransaction : Transaction
    {
        private Account _account;      // Account the money will be withdrawn from

        public WithdrawTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
        }

        public override void Print()
        {
            base.Print(); // Call base Print to show common transaction details
            Console.WriteLine($"Account Name: {_account.Name}");

            // Human‑readable status messages
            if (Executed)
            {
                if (Success)
                    Console.WriteLine("Status: Withdrawal successfully completed.");
                else
                    Console.WriteLine("Status: Withdrawal failed.");
            }

            if (Reversed)
                Console.WriteLine("Status: Transaction successfully reversed.");
        }

        public override void Execute()
        {
            base.Execute(); // sets _executed, _dateStamp; guards double-execution

            if (_account.Withdraw(_amount))
            {
                _success = true;
            }
            else
            {
                _success = false;
                throw new InvalidOperationException("Insufficient funds or invalid amount for withdrawal.");
            }
        }

        public override void Rollback()
        {
            base.Rollback(); // guards not-executed, double-rollback, failed; sets _dateStamp

            if (_account.Deposit(_amount))
            {
                _reversed = true;
            }
            else
            {
                throw new InvalidOperationException(
                    "Failed to deposit funds back to the account during rollback."
                );
            }
        }
    }
}
