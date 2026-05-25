using System;

namespace Practical_8_1
{
    class Tester
    {
        static void Main(string[] args)
        {
            // Create and populate a stack of accounts
            MyStack<Account> accountStack = new MyStack<Account>(10);
            
            accountStack.Push(new Account("Everyday Account", 50m));
            accountStack.Push(new Account("Savings Account", 1500m));
            accountStack.Push(new Account("Credit Card", -200m));
            accountStack.Push(new Account("Short Term Deposit", 5000m));
            accountStack.Push(new Account("Investment Account", 100m));

            Console.WriteLine($"Stack populated with {accountStack.Count} accounts.\n");

            // Test Find method
            Console.WriteLine("Testing Find");
            var found1 = accountStack.Find(a => a.Balance > 0);
            Console.WriteLine($"First account with strictly positive balance: {(found1 != null ? found1.ToString() : "Not found")}");

            var found2 = accountStack.Find(a => a.Balance > 0 && a.Balance <= 100);
            Console.WriteLine($"First account with positive balance <= $100: {(found2 != null ? found2.ToString() : "Not found")}");

            var found3 = accountStack.Find(a => a.Balance > 10000);
            Console.WriteLine($"First account with balance > $10000: {(found3 != null ? found3.ToString() : "Not found")}\n");

            // Test FindAll method
            Console.WriteLine("Testing FindAll");
            var allFound1 = accountStack.FindAll(a => a.Balance > 1000);
            if (allFound1 != null)
            {
                Console.WriteLine($"Found {allFound1.Length} accounts with balance > $1000:");
                foreach (var acc in allFound1) Console.WriteLine("  " + acc);
            }
            else
            {
                Console.WriteLine("No accounts with balance > $1000 found.");
            }

            var allFound2 = accountStack.FindAll(a => a.Name.Contains("Credit Card") || a.Name.Contains("Short Term"));
            if (allFound2 != null)
            {
                Console.WriteLine($"Found {allFound2.Length} accounts related to credit cards and short term deposits:");
                foreach (var acc in allFound2) Console.WriteLine("  " + acc);
            }
            else
            {
                Console.WriteLine("No accounts related to credit cards or short term deposits found.");
            }
            Console.WriteLine();

            // Test Max and Min
            Console.WriteLine("Testing Max and Min");
            var maxAccount = accountStack.Max();
            Console.WriteLine($"Account with maximum balance: {(maxAccount != null ? maxAccount.ToString() : "Stack is empty")}");

            var minAccount = accountStack.Min();
            Console.WriteLine($"Account with minimum balance: {(minAccount != null ? minAccount.ToString() : "Stack is empty")}\n");

            // Test RemoveAll
            Console.WriteLine("Testing RemoveAll");
            int removedCount = accountStack.RemoveAll(a => a.Balance < 0);
            Console.WriteLine($"Removed {removedCount} accounts with negative balance. Remaining accounts in stack: {accountStack.Count}\n");

            // Test Exception
            Console.WriteLine("Testing ArgumentNullException");
            try
            {
                accountStack.Find(null);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Expected exception caught for passing null to Find(): " + ex.Message);
            }
        }
    }
}
