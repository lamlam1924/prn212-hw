﻿using System;

namespace CommandLineCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to .NET Core Calculator!");
            Console.WriteLine("Available operations: add, subtract, multiply, divide");
            Console.WriteLine("Example usage: add 5 3");
            Console.WriteLine("Type 'exit' to quit");
            
            bool running = true;
            while (running)
            {
                Console.Write("> ");
                string input = Console.ReadLine(); // Đọc chuỗi nhập từ người dùng
                
                if (string.IsNullOrWhiteSpace(input))
                    continue; // Nếu người dùng không nhập gì, quay lại đầu vòng lặp
                    
                string[] parts = input.Trim().Split(' '); // Tách chuỗi
                
                if (parts[0].ToLower() == "exit")
                {
                    running = false;
                    continue;
                }
                
                if (parts.Length != 3)
                {
                    Console.WriteLine("Invalid input format. Please use: operation number1 number2");
                    continue;
                }
                // TODO: Implement parsing numbers and performing calculations
                // This is where you will add your code
                
                // Example implementation for addition:
                if (parts[0].ToLower() == "add")
                {
                    if (double.TryParse(parts[1], out double num1) && double.TryParse(parts[2], out double num2))
                    {
                        Console.WriteLine($"Result: {num1 + num2}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid numbers provided");
                    }
                }
                // TODO: Implement subtract, multiply, divide operations
                else if (parts[0].ToLower() == "subtract")
                {
                    if (double.TryParse(parts[1], out double num1) && double.TryParse(parts[2], out double num2))
                    {
                        Console.WriteLine($"Result: {num1 - num2}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid numbers provided");
                    }
                }
                else if (parts[0].ToLower() == "multiply")
                {
                    if (double.TryParse(parts[1], out double num1) && double.TryParse(parts[2], out double num2))
                    {
                        Console.WriteLine($"Result: {num1 * num2}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid numbers provided");
                    }
                }
                else if (parts[0].ToLower() == "divide")
                {
                    if (double.TryParse(parts[1], out double num1) && double.TryParse(parts[2], out double num2))
                    {
                        if (num2 != 0)
                        {
                            Console.WriteLine($"Result: {num1 / num2}");
                        }
                        else
                        {
                            Console.WriteLine("Cannot divide by zero");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid numbers provided");
                    }
                }
                else
                {
                    Console.WriteLine("Unknown operation. Please use: add, subtract, multiply, divide");
                }
            } 

            Console.WriteLine("Goodbye!"); 
        }
    }
}
