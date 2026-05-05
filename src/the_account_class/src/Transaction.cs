using System;

namespace TheAccountClass
{
    public abstract class Transaction
    {
        protected decimal _amount;
        protected bool _executed;
        protected bool _success;
        protected bool _reversed;
        protected DateTime _dateStamp;

        public bool Executed => _executed;
        public virtual bool Success => _success;
        public bool Reversed => _reversed;
        public DateTime DateStamp => _dateStamp;

        // Constructor initializes the transaction with an amount
        public Transaction(decimal amount)
        {
            _amount = amount;
            _executed = false;
            _success = false;
            _reversed = false;
            // _dateStamp will be set in Execute and Rollback
        }

        public virtual void Print() // Prints transaction details to the console
        {
            Console.WriteLine("Transaction Details:");
            Console.WriteLine($"Amount: {_amount:C}");
            Console.WriteLine($"Executed: {Executed}");
            Console.WriteLine($"Success: {Success}");
            Console.WriteLine($"Reversed: {Reversed}");
            Console.WriteLine($"Date: {DateStamp}");
        }

        public abstract void Execute(); // Abstract method to execute the transaction
        public abstract void Rollback(); // Abstract method to rollback the transaction

    }
}
