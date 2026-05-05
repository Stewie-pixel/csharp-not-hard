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
            // Prevent executing the same transaction twice
            if (_executed)
                throw new InvalidOperationException("Transaction has already been attempted.");

            _executed = true;
            _dateStamp = DateTime.Now;

            // Attempt the withdrawal on the account
            if (_account.Withdraw(_amount))
            {
                _success = true;
            }
            else
            {
                _success = false;
                // Withdrawal fails if insufficient funds or invalid amount
                throw new InvalidOperationException("Insufficient funds or invalid amount for withdrawal.");
            }
        }

        public override void Rollback()
        {
            // Cannot rollback before execution
            if (!_executed)
                throw new InvalidOperationException("Transaction has not been executed yet.");

            // Cannot rollback twice
            if (_reversed)
                throw new InvalidOperationException("Transaction has already been reversed.");

            // Only successful withdrawals can be rolled back
            if (!_success)
                throw new InvalidOperationException("Cannot rollback a failed transaction.");

            _dateStamp = DateTime.Now; // Update datestamp on rollback

            // Reverse the withdrawal by depositing the same amount
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
