using System;

namespace Practical_8_1
{
    public class Account : IComparable<Account>
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public Account(string name, decimal balance)
        {
            Name = name;
            Balance = balance;
        }

        public int CompareTo(Account other)
        {
            if (other == null) return 1;
            
            if (this.Balance < other.Balance)
                return -1;
            if (this.Balance > other.Balance)
                return 1;
            
            return 0;
        }

        public override string ToString()
        {
            return $"Account: {Name}, Balance: {Balance:C}";
        }
    }
}
