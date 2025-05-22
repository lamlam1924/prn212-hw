// A utility to analyze text files and provide statistics
using System;
using System.IO; // xử lý file
using System.Collections.Generic; //hỗ trợ cho List
using System.Linq; //xử lí dữ liệu kiểu danh sáchsách

namespace FileAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("File Analyzer - .NET Core");
            Console.WriteLine("This tool analyzes text files and provides statistics.");
            
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a file path as a command-line argument.");
                Console.WriteLine("Example: dotnet run myfile.txt");
                return;
            }
            
            string filePath = args[0];
            
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File '{filePath}' does not exist.");
                return;
            }
            
            try
            {
                Console.WriteLine($"Analyzing file: {filePath}");
                
                // Read the file content
                string content = File.ReadAllText(filePath); //đọc file thành 1 chuỗi string
                
                // TODO: Implement analysis functionality
                // 1. Count words
                // 2. Count characters (with and without whitespace)
                // 3. Count sentences
                // 4. Identify most common words
                // 5. Average word length
                
                // Example implementation for counting lines:
                int lineCount = File.ReadAllLines(filePath).Length; //return 1 mảng, đếm số dòng
                Console.WriteLine($"Number of lines: {lineCount}");
                
                // TODO: Additional analysis to be implemented
                // 1. Count words
                string[] words = content.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                int wordCount = words.Length;
                Console.WriteLine($"Number of words: {wordCount}");

                // 2. Count characters (with and without whitespace)
                int charCount = content.Length;
                int charCountWithoutWhitespace = content.Replace(" ", "").Replace("\n", "").Replace("\r", "").Length;
                Console.WriteLine($"Number of characters (with whitespace): {charCount}");
                Console.WriteLine($"Number of characters (without whitespace): {charCountWithoutWhitespace}");

                // 3. Count sentences
                char[] sentenceDelimiters = { '.', '!', '?' };
                int sentenceCount = content.Split(sentenceDelimiters, StringSplitOptions.RemoveEmptyEntries).Length;
                Console.WriteLine($"Number of sentences: {sentenceCount}");

                // 4. Identify most common words
                var wordGroups = words.GroupBy(w => w.ToLower())
                                      .Select(g => new { Word = g.Key, Count = g.Count() })
                                      .OrderByDescending(g => g.Count)
                                      .Take(5); // Top 5 most common words
                Console.WriteLine("Most common words:");
                foreach (var wordGroup in wordGroups)
                {
                    Console.WriteLine($"- {wordGroup.Word}: {wordGroup.Count}");
                }

                // 5. Average word length
                double averageWordLength = words.Average(w => w.Length);
                Console.WriteLine($"Average word length: {averageWordLength:F2} characters"); //F2: định dạng số thập phân với 2 chữ số sau dấu phẩy                          
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during file analysis: {ex.Message}");
            }
        }
    }
}