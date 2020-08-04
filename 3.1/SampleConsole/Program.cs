﻿using System;
using System.Diagnostics;
using System.IO;
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
            Trace.Listeners.Add(new TextWriterTraceListener(File.CreateText("log.txt")));
            Trace.AutoFlush = true;
            WriteLine($"Max Value Interger is {Int32.MaxValue}");
            Debug.WriteLine("Debug says, I am watching!");
            Trace.WriteLine("Trace says, I am watching!");
            RunFactorial();

            // loop through the assemblies that this app references
            foreach (var r in Assembly.GetEntryAssembly()
            .GetReferencedAssemblies())
            {
                // load the assembly so we can read its details
                var assem = Assembly.Load(new AssemblyName(r.FullName));
                // declare a variable to count the number of methods
                int methodCount = 0;
                // loop through all the types in the assembly
                foreach (var t in assem.DefinedTypes)
                {
                    // add up the counts of methods
                    methodCount += t.GetMethods().Count();
                }
                // output the count of types and their methods
                Console.WriteLine(
                "{0:N0} types with {1:N0} methods in {2} assembly.",
                arg0: assem.DefinedTypes.Count(),
                arg1: methodCount,
                arg2: r.Name);
            }

            Console.WriteLine($"int uses {sizeof(int)} bytes and can store numbers in the range {int.MinValue:N0} to {int.MaxValue:N0}.");
            Console.WriteLine($"double uses {sizeof(double)} bytes and can store numbers in the range {double.MinValue:N0} to {double.MaxValue:N0}.");
            Console.WriteLine($"decimal uses {sizeof(decimal)} bytes and can store numbers in the range {decimal.MinValue:N0} to {decimal.MaxValue:N0}.");


            double a = 0.1;
            double b = 0.2;
            if (a + b == 0.3)
            {
                Console.WriteLine($"{a} + {b} equals 0.3");
            }
            else
            {
                Console.WriteLine($"{a} + {b} does NOT equal 0.3, it equals to {a + b}");
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
