using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace CalculatorProgram
{
    class Calculator
    {
        // --- Basic math operations ---
        public double Add(double a, double b)
        {
            return a + b;
        }

        // --- Overload for adding several numbers using an array ---
        public double Add(double[] numbers)
        {
            double sum = 0;
            foreach (double num in numbers)
            {
                sum += num; // adds up all numbers in the array
            }
            return sum;
        }

        public double Subtract(double a, double b)
        {
            return a - b;
        }

        public double Multiply(double a, double b)
        {
            return a * b;
        }

        public double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            return a / b;
        }

        // --- Handles longer math expressions like "2 + 3 * 4" ---
        public double EvaluateExpression(string expression)
        {
            try
            {
                var table = new DataTable();
                var result = table.Compute(expression, "");
                return Convert.ToDouble(result);
            }
            catch
            {
                throw new InvalidOperationException("Invalid expression format.");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Calculator calc = new Calculator();

            // A list that stores all previous calculations
            List<string> history = new List<string>();

            Console.WriteLine("🧮 Calculator Program");
            Console.WriteLine("Type in your Math Equations here or Type 'exit/quit/stop' anytime to quit.\n");

            bool running = true; // controls the while-loop

            // --- Main loop keeps program running until user quits ---
            while (running)
            {
                Console.Write("> ");
                string? input = Console.ReadLine();

                // skip if user just presses enter
                if (string.IsNullOrWhiteSpace(input))
                    continue;

                input = input.Trim();

                // exit the program
                if (input.Equals("quit", StringComparison.OrdinalIgnoreCase) ||
                    input.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                    input.Equals("stop", StringComparison.OrdinalIgnoreCase))
                {
                    running = false;
                    continue;
                }

                try
                {
                    double result = 0;
                    // Prevent expressions like "+ 11" or "* 5"
                    if (input.StartsWith("+") || input.StartsWith("-") || input.StartsWith("*") || input.StartsWith("/"))
                    {
                        Console.WriteLine("Error: Expression cannot start with an operator.\n");
                        continue;
                    }

                    // Split input into parts (like ["2", "+", "3"])
                    string[] parts = input.Split(' ');

                    // Example: "add 1 2 3 4" -> adds all numbers
                    if (parts[0].ToLower() == "add" && parts.Length > 2)
                    {
                        // create an array with all numbers after "add"
                        double[] numbers = new double[parts.Length - 1];

                        for (int i = 1; i < parts.Length; i++)
                        {
                            // use double.Parse here to convert string to number
                            numbers[i - 1] = double.Parse(parts[i]);
                        }

                        result = calc.Add(numbers); // calls the overloaded Add method
                    }
                    // Example: "3 + 2"
                    else if (parts.Length == 3 && double.TryParse(parts[0], out double a) && double.TryParse(parts[2], out double b))
                    {
                        string op = parts[1];
                        result = DoCalculation(calc, a, op, b);
                    }
                    else
                    {
                        // Example: "2 + 3 * 4"
                        result = calc.EvaluateExpression(input);
                    }

                    // --- Fake loading effect before showing result ---
                    ShowLoading("Calculating");

                    Console.WriteLine($"Result: {result}\n");

                    // Save the calculation to history
                    history.Add($"{input} = {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}\n");
                }
            }

            // --- Display all calculations before exit ---
            Console.WriteLine("\n--- Calculation History ---");
            foreach (string entry in history)
            {
                Console.WriteLine(entry);
            }

            Console.WriteLine("\nGoodbye!");
        }

        // --- Helper method that does basic single-operator math ---
        static double DoCalculation(Calculator calc, double a, string op, double b)
        {
            switch (op)
            {
                case "+": return calc.Add(a, b);
                case "-": return calc.Subtract(a, b);
                case "*": return calc.Multiply(a, b);
                case "/": return calc.Divide(a, b);
                default: throw new InvalidOperationException("Unknown operator.");
            }
        }

        // --- Creates a fake loading effect ---
        static void ShowLoading(string message = "Calculating")
        {
            Console.Write($"{message}");
            for (int i = 0; i < 4; i++)
            {
                Console.Write(".");
                Thread.Sleep(400); // wait 0.4 seconds per dot
            }
            Console.WriteLine("\n");
        }
    }
}
