using System;
using System.Linq;
using System.Reflection;
using static System.Console;
using static SampleConsole.Math;

namespace SampleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine($"Max Value Interger is {Int32.MaxValue}");
            RunFactorial();

            // loop through the assemblies that this app references
            foreach (var r in Assembly.GetEntryAssembly()
            .GetReferencedAssemblies())
            {
                // load the assembly so we can read its details
                var a = Assembly.Load(new AssemblyName(r.FullName));
                // declare a variable to count the number of methods
                int methodCount = 0;
                // loop through all the types in the assembly
                foreach (var t in a.DefinedTypes)
                {
                    // add up the counts of methods
                    methodCount += t.GetMethods().Count();
                }
                // output the count of types and their methods
                Console.WriteLine(
                "{0:N0} types with {1:N0} methods in {2} assembly.",
                arg0: a.DefinedTypes.Count(),
                arg1: methodCount,
                arg2: r.Name);
            }
        }

        static void RunFactorial()
        {
            bool isNumber;
            do
            {
                Write("Enter a number: ");
                isNumber = long.TryParse(
                ReadLine(), out long number);
                if (isNumber)
                {
                    WriteLine(
                    $"{number:N0}! = {Factorial(number.ToString()):N0}");
                }
                else
                {
                    WriteLine("You did not enter a valid number!");
                    ReadLine();
                }
            }
            while (isNumber);
        }
    }
}
