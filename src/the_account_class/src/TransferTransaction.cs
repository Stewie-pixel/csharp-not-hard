using System;

namespace TheAccountClass
{
    public class TransferTransaction : Transaction
    {
        private Account _fromAccount;
        private Account _toAccount;
        private DepositTransaction _deposit;
        private WithdrawTransaction _withdraw;

        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
        {
            _fromAccount = fromAccount;
            _toAccount = toAccount;
            _withdraw = new WithdrawTransaction(fromAccount, amount);
            _deposit = new DepositTransaction(toAccount, amount);
            // _executed and _reversed are handled by base class
        }

        public override bool Success
        {
            get
            {
                // Success is true only if the transfer has been executed, not reversed,
                // and both withdraw and deposit legs succeeded.
                return _executed && !_reversed && _withdraw.Success && _deposit.Success;
            }
        }

        public override void Print()
        {
            base.Print(); // Call base Print to show common transaction details
            Console.WriteLine($"From Account: {_fromAccount.Name}");
            Console.WriteLine($"To Account: {_toAccount.Name}");

            if (Executed)
            {
                if (Success)
                {
                    Console.WriteLine("Status: Transfer successfully completed.");
                }
                else
                {
                    Console.WriteLine("Status: Transfer failed.");
                }
            }
            if (Reversed)
            {
                Console.WriteLine("Status: Transaction successfully reversed.");
            }

            Console.WriteLine(""); // Add an empty line for spacing
            Console.WriteLine("Withdrawal Leg Details:");
            _withdraw.Print();
            Console.WriteLine(""); // Add an empty line for spacing
            Console.WriteLine("Deposit Leg Details:");
            _deposit.Print();
        }

        public override void Execute()
        {
            if (_executed)
            {
                throw new InvalidOperationException("Transfer transaction has already been attempted.");
            }

            _executed = true;
            _dateStamp = DateTime.Now;

            try
            {
                _withdraw.Execute();
                _deposit.Execute();
            }
            catch (InvalidOperationException ex)
            {
                // If either withdraw or deposit fails, the transfer fails.
                // Re-throw the exception after attempting to rollback the withdraw if it succeeded partially.
                if (_withdraw.Success)
                {
                    try
                    {
                        _withdraw.Rollback();
                    }
                    catch (InvalidOperationException rollbackEx)
                    {
                        Console.WriteLine($"Warning: Failed to rollback partial withdrawal during transfer failure: {rollbackEx.Message}");
                    }
                }
                throw new InvalidOperationException($"Transfer failed: {ex.Message}", ex);
            }
        }

        public override void Rollback()
        {
            if (!_executed)
            {
                throw new InvalidOperationException("Transfer transaction has not been executed yet.");
            }
            if (_reversed)
            {
                throw new InvalidOperationException("Transfer transaction has already been reversed.");
            }
            if (!Success)
            {
                throw new InvalidOperationException("Cannot rollback a failed transfer transaction.");
            }

            _dateStamp = DateTime.Now; // Update datestamp on rollback

            try
            {
                _deposit.Rollback();
                _withdraw.Rollback();
                _reversed = true;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Rollback of transfer failed: {ex.Message}", ex);
            }
        }
    }
}
