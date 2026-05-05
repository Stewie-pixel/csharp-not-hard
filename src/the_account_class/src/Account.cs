using System;

namespace TheAccountClass
{
   
    public class Account
    {
        private decimal _balance;
        private string _name;

        public Account(string name, decimal balance) // Initializes a new instance of the Account class with a name and initial balance.
        {
            _name = name;
            _balance = balance;
        }

        public bool Deposit(decimal amount) // Deposits a specified amount into the account, returning true if successful.
        {
            if (amount > 0)
            {
                _balance += amount;
                return true;
            }
            // Deposit fails if amount is 0 or negative
            return false;
        }

        public bool Withdraw(decimal amount) // Withdraws a specified amount from the account, returning true if successful.
        {
            // Ensure amount is positive and the account has enough funds
            if (amount > 0 && amount <= _balance)
            {
                _balance -= amount;
                return true;
            }
            // Withdrawal fails if amount is invalid or funds are insufficient
            return false;
        }

        public void Print() // Prints the account name and current balance to the console.
        {
            Console.WriteLine("Account Name: " + _name);
            Console.WriteLine("Balance: " + _balance.ToString("C"));
        }

        public string Name // Gets the name of the account holder.
        {
            get { return _name; }
        }
    }
}
