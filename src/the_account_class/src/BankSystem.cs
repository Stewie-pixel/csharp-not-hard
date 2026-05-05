using System;

namespace TheAccountClass
{
    public enum MenuOption // Enumerates the menu options available for the banking system.
    {
        Withdraw,
        Deposit,
        Print,
        Transfer,
        AddAccount,
        PrintTransactionHistory,
        RollbackTransaction,
        Exit
    }

    public class BankSystem // Handles user interaction and orchestrates banking operations.
    {
        public static void Main(string[] args) // Initializes bank accounts and runs the main menu loop for user interaction.
        {
            Bank bank = new Bank();
            bank.AddAccount(new Account("Jake's Account", 200000.00m));
            bank.AddAccount(new Account("Alice's Account", 50000.00m));

            MenuOption option;

            do
            {
                option = ReadUserOption();
                switch (option)
                {
                    case MenuOption.Withdraw:
                        DoWithdraw(bank);
                        break;
                    case MenuOption.Deposit:
                        DoDeposit(bank);
                        break;
                    case MenuOption.Print:
                        DoPrint(bank);
                        break;
                    case MenuOption.Transfer:
                        DoTransfer(bank);
                        break;
                    case MenuOption.AddAccount:
                        DoAddAccount(bank);
                        break;
                    case MenuOption.PrintTransactionHistory:
                        DoPrintTransactionHistory(bank);
                        break;
                    case MenuOption.RollbackTransaction:
                        DoRollbackTransaction(bank);
                        break;
                    case MenuOption.Exit:
                        Console.WriteLine("Exiting application. Goodbye!");
                        break;
                }
            } while (option != MenuOption.Exit);
        }

        private static MenuOption ReadUserOption() // Displays the menu and reads the user's choice, ensuring a valid selection.
        {
            int optionInt = 0;
            bool isValid = false;

            do
            {
                Console.WriteLine("\n--- Banking Menu ---");
                Console.WriteLine("1. Withdraw");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Print Account Details");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Add new account");
                Console.WriteLine("6. Print Transaction History");
                Console.WriteLine("7. Rollback Transaction");
                Console.WriteLine("8. Exit");
                Console.Write("Select an option (1-8): ");

                try
                {
                    string? input = Console.ReadLine();
                    optionInt = Convert.ToInt32(input);

                    // Validate choice is within range
                    if (optionInt >= 1 && optionInt <= 8) // Updated validation range
                    {
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Error: Please select a valid option (1-8).");
                    }
                }
                catch
                {
                    // Handles non-integer input
                    Console.WriteLine("Error: Please enter a numeric value.");
                }
            } while (!isValid);

            // Adjust to 0-based index for Enum mapping
            return (MenuOption)(optionInt - 1);
        }

        private static void DoDeposit(Bank bank) // Handles the deposit process, prompting the user for an amount and account.
        {
            Account? account = FindAccount(bank);
            if (account == null)
            {
                return;
            }

            Console.Write("Enter the amount to deposit: ");
            try
            {
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                DepositTransaction transaction = new DepositTransaction(account, amount);
                bank.ExecuteTransaction(transaction); // Use the bank's execute transaction
                if (transaction.Success)
                {
                    Console.WriteLine("Deposit successful.");
                }
                else
                {
                    Console.WriteLine("Deposit failed.");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch
            {
                Console.WriteLine("Error: Invalid numeric input for amount.");
            }
        }

        private static void DoWithdraw(Bank bank) // Handles the withdrawal process, prompting the user for an amount and account.
        {
            Account? account = FindAccount(bank);
            if (account == null)
            {
                return;
            }

            Console.Write("Enter the amount to withdraw: ");
            try
            {
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                WithdrawTransaction transaction = new WithdrawTransaction(account, amount);
                bank.ExecuteTransaction(transaction); // Use the bank's execute transaction
                if (transaction.Success)
                {
                    Console.WriteLine("Withdrawal successful.");
                }
                else
                {
                    Console.WriteLine("Withdrawal failed.");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch
            {
                Console.WriteLine("Error: Invalid numeric input for amount.");
            }
        }

        private static void DoTransfer(Bank bank) // Handles the transfer process, prompting for source, destination accounts, and amount.
        {
            Console.WriteLine("--- Transfer From ---");
            Account? fromAccount = FindAccount(bank);
            if (fromAccount == null)
            {
                return;
            }

            Console.WriteLine("--- Transfer To ---");
            Account? toAccount = FindAccount(bank);
            if (toAccount == null)
            {
                return;
            }

            if (fromAccount == toAccount)
            {
                Console.WriteLine("Error: Cannot transfer money to the same account.");
                return;
            }

            Console.Write($"Enter the amount to transfer from {fromAccount.Name} to {toAccount.Name}: ");
            try
            {
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                TransferTransaction transaction = new TransferTransaction(fromAccount, toAccount, amount);
                bank.ExecuteTransaction(transaction); // Use the bank's execute transaction
                if (transaction.Success)
                {
                    Console.WriteLine("Transfer successful.");
                }
                else
                {
                    Console.WriteLine("Transfer failed.");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch
            {
                Console.WriteLine("Error: Invalid numeric input for amount.");
            }
        }

        private static void DoPrint(Bank bank) // Prints the details of all accounts currently in the bank.
        {
            if (bank.Accounts.Count == 0)
            {
                Console.WriteLine("No accounts in the bank to print.");
                return;
            }
            Console.WriteLine("\n--- All Bank Accounts ---");
            foreach (var account in bank.Accounts)
            {
                account.Print();
            }
            Console.WriteLine("-------------------------\n");
        }

        private static void DoAddAccount(Bank bank) // Prompts the user for details and creates a new bank account.
        {
            Console.Write("Enter the name for the new account: ");
            string? accountName = Console.ReadLine();

            Console.Write("Enter the initial balance for the new account: ");
            decimal initialBalance = 0;
            try
            {
                initialBalance = Convert.ToDecimal(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Error: Invalid numeric input for initial balance. Account not created.");
                return;
            }

            if (string.IsNullOrWhiteSpace(accountName))
            {
                Console.WriteLine("Error: Account name cannot be empty. Account not created.");
                return;
            }

            Account newAccount = new Account(accountName, initialBalance);
            bank.AddAccount(newAccount);
            Console.WriteLine($"Account '{accountName}' created with balance {initialBalance:C}.");
        }

        private static void DoPrintTransactionHistory(Bank bank) // Prints the complete transaction history of the bank.
        {
            bank.PrintTransactionHistory();
        }

        private static void DoRollbackTransaction(Bank bank)
        {
            bank.PrintTransactionHistory(); // Show the transactions first

            Console.Write("Enter the number of the transaction to rollback (e.g., 1 for the first transaction): ");
            try
            {
                int transactionNumber = Convert.ToInt32(Console.ReadLine());
                // Adjust to 0-based index
                Transaction? transactionToRollback = bank.GetTransaction(transactionNumber - 1);

                if (transactionToRollback == null)
                {
                    Console.WriteLine("Error: Invalid transaction number.");
                    return;
                }

                bank.RollbackTransaction(transactionToRollback);
                Console.WriteLine("Transaction rolled back successfully.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error rolling back transaction: {ex.Message}");
            }
            catch
            {
                Console.WriteLine("Error: Invalid input. Please enter a numeric value for the transaction number.");
            }
        }

        private static Account? FindAccount(Bank bank) // Prompts the user for an account name and retrieves the corresponding account from the bank
        {
            Console.Write("Enter account name: ");
            string? accountName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(accountName))
            {
                Console.WriteLine("Error: Account name cannot be empty.");
                return null;
            }

            Account? account = bank.GetAccount(accountName);

            if (account == null)
            {
                Console.WriteLine($"Error: Account '{accountName}' not found.");
            }
            return account;
        }
    }
}
