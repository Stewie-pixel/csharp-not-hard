using System;
using System.Collections.Generic;

namespace TheAccountClass
{
    public class Bank
    {
        private List<Account> _accounts;   // Stores all accounts managed by the bank
        private List<Transaction> _transactions; // Stores all transactions

        public Bank() // Initializes a new instance of the Bank class with empty accounts and transactions lists.
        {
            _accounts = new List<Account>();   // Initializes an empty list to store accounts.
            _transactions = new List<Transaction>(); // Initializes an empty list to store transactions.
        }

        public List<Account> Accounts // Gets a read-only list of accounts managed by the bank.
        {
            get { return _accounts; }
        }

        public void AddAccount(Account account) // Adds a new account to the bank's managed accounts.
        {
            _accounts.Add(account);
        }

        public Account GetAccount(string name) // Retrieves an account by its name, or null if not found.
        {
            foreach (var account in _accounts)
            {
                if (account.Name == name)
                {
                    return account;
                }
            }
            return null;
        }

        public void ExecuteTransaction(Transaction transaction) // Adds and executes a given transaction.
        {
            _transactions.Add(transaction);
            transaction.Execute();
        }

        public void RollbackTransaction(Transaction transaction) // Rolls back a previously executed transaction.
        {
            transaction.Rollback();
        }

        public void PrintTransactionHistory() // Prints the details of all recorded transactions to the console.
        {
            Console.WriteLine("\n--- Transaction History ---");
            if (_transactions.Count == 0)
            {
                Console.WriteLine("No transactions yet.");
                return;
            }

            for (int i = 0; i < _transactions.Count; i++)
            {
                Console.WriteLine($"\nTransaction #{i + 1}:");
                _transactions[i].Print();
            }
            Console.WriteLine("---------------------------\n");
        }

        public Transaction GetTransaction(int index) // Retrieves a transaction by its index from the history, or null if invalid.
        {
            if (index >= 0 && index < _transactions.Count)
            {
                return _transactions[index];
            }
            return null;
        }
    }
}
