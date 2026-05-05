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
            // Prevent executing the same transaction twice
            if (_executed)
                throw new InvalidOperationException("Transaction has already been attempted.");

            // Validate deposit amount
            if (_amount <= 0)
                throw new InvalidOperationException("Deposit amount must be positive.");

            _executed = true;
            _dateStamp = DateTime.Now;

            // Attempt the deposit on the account
            if (_account.Deposit(_amount))
            {
                _success = true;
            }
            else
            {
                _success = false;
                // Deposit should normally never fail if amount > 0
                throw new InvalidOperationException("Deposit failed for an unknown reason.");
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

            // Only successful transactions can be rolled back
            if (!_success)
                throw new InvalidOperationException("Cannot rollback a failed transaction.");

            _dateStamp = DateTime.Now; // Update datestamp on rollback

            // Reverse the deposit by withdrawing the same amount
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
