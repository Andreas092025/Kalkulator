🧮 Calculator Program - C# Console Application

This program is a simple calculator that supports basic math operations and expression evaluation.
It demonstrates key C# programming concepts such as:

- While-loops (for continuous program execution)
- Method overloads (multiple Add methods for different inputs)
- Input parsing using .Split()
- Number conversion with double.Parse() and double.TryParse()
- Use of List<T> to store previous calculations
- Adding a fun "loading effect" after User input

How it works:

1. The program runs in a loop until the user types 'quit' or 'exit'.
2. The user can type a simple operation (e.g., "3 + 2") or a full expression (e.g., "2 + 3 * 4").
3. The input is split into parts using input.Split(' ').
4. If the command starts with 'add' followed by several numbers (e.g., "add 1 2 3 4"),
   the program uses the overloaded Add(double[] numbers) method to calculate the sum.
5. If the input contains a single operation (like "5 * 6"), the program parses the numbers
   using double.TryParse() and performs the corresponding operation.
6. Each result is shown and stored in a list (history) that is printed when the user exits.

Example inputs:
> 3 + 2
> add 1 2 3 4
> 5 * 6
> 2 + 3 * 4
> quit or exit to close the program