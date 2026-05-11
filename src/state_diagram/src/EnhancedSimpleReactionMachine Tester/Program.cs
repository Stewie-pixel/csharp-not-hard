using System;
using EnhancedSimpleReactionMachine_Tester;

namespace EnhancedSimpleReactionMachine_Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tester tester = new Tester();
                tester.RunAllTests();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{Environment.NewLine}An error occurred during testing: {ex.Message}");
                Console.ResetColor();
            }
            Console.WriteLine($"{Environment.NewLine}Press any key to exit.");
            Console.ReadKey();
        }
    }
}
