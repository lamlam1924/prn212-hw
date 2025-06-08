using System;
using System.Collections.Generic;
using System.Text;

namespace DelegatesLinQ.Homework
{
    // Delegate types for processing pipeline
    public delegate string DataProcessor(string input);
    public delegate void ProcessingEventHandler(string stage, string input, string output);

    /// <summary>
    /// Homework 2: Custom Delegate Chain
    /// Create a data processing pipeline using multicast delegates.
    /// 
    /// Requirements:
    /// 1. Create a processing pipeline that transforms text data through multiple steps
    /// 2. Use multicast delegates to chain processing operations
    /// 3. Add logging/monitoring capabilities using events
    /// 4. Demonstrate adding and removing processors from the chain
    /// 5. Handle errors in the processing chain
    /// 
    /// Techniques used: Similar to 6_2_MulticastDelegate
    /// - Multicast delegate chaining
    /// - Delegate combination and removal
    /// - Error handling in delegate chains
    /// </summary>
    public class DataProcessingPipeline
    {
        // TODO: Declare events for monitoring the processing

        // Khai báo event để theo dõi quá trình xử lý
        public event ProcessingEventHandler ProcessingStageCompleted;

        // Individual processing methods that students need to implement

        // Xóa khoảng trắng
        public static string RemoveSpaces(string input)
        {
            // TODO: Remove all spaces from input
            return input.Replace(" ", "");
        }

        public static string ToUpperCase(string input)
        {
            // TODO: Convert input to uppercase
            return input.ToUpper();
        }

        public static string AddTimestamp(string input)
        {
            // TODO: Add current timestamp to the beginning of input
           return $"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] {input}";
        }

        public static string ReverseString(string input)
        {
            // TODO: Reverse the characters in the input string
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string EncodeBase64(string input)
        {
            // TODO: Encode the input string to Base64
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }

        public static string ValidateInput(string input)
        {
            // TODO: Validate input (throw exception if null or empty)
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input cannot be null or empty");
            }
            return input;
        }

        // Method to process data through the pipeline
        public string ProcessData(string input, DataProcessor pipeline)
        {
            // TODO: Process input through the pipeline and raise events
            // Handle any exceptions that occur during processing
            if (pipeline == null)
            {
                throw new ArgumentNullException(nameof(pipeline), "Processing pipeline cannot be null");
            }
            try
            {
                string currentInput = input;
                // Lấy danh sách các delegates trong chain
                Delegate[] processors = pipeline.GetInvocationList();

                foreach (DataProcessor processor in processors)
                {
                    string stage = processor.Method.Name;
                    string output = processor(currentInput);

                    // Kích hoạt event cho mỗi bước xử lý
                    OnProcessingStageCompleted(stage, currentInput, output);

                    currentInput = output;
                }

                return currentInput;
            }
            catch (Exception ex)
            {
                OnProcessingStageCompleted("Error", input, ex.Message);
                throw;
            }
        }

        // TODO: Add method to raise processing events
        protected virtual void OnProcessingStageCompleted(string stage, string input, string output)
        {
            ProcessingStageCompleted?.Invoke(stage, input, output);
        }
    }

    // Logger class to monitor processing
    public class ProcessingLogger
    {
        // TODO: Implement event handler to log processing stages
        public void OnProcessingStageCompleted(string stage, string input, string output)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"LOG: {stage}");
            Console.WriteLine($"  Input:  {input}");
            Console.WriteLine($"  Output: {output}");
            Console.WriteLine();
            Console.ResetColor();
        }
    }

    // Performance monitor class
    public class PerformanceMonitor
    {
        private Dictionary<string, List<TimeSpan>> _processingTimes = new Dictionary<string, List<TimeSpan>>();
        private DateTime _lastStageStart = DateTime.UtcNow;
        // TODO: Track processing times and performance metrics
        public void OnProcessingStageCompleted(string stage, string input, string output)
        {
            TimeSpan duration = DateTime.UtcNow - _lastStageStart;

            if (!_processingTimes.ContainsKey(stage))
            {
                _processingTimes[stage] = new List<TimeSpan>();
            }
            _processingTimes[stage].Add(duration);

            _lastStageStart = DateTime.UtcNow;
        }
        public void DisplayStatistics()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=== Performance Statistics ===");
            
            foreach (var stage in _processingTimes)
            {
                double avgMs = stage.Value.Average(t => t.TotalMilliseconds);
                Console.WriteLine($"{stage.Key}:");
                Console.WriteLine($"  Average processing time: {avgMs:F2}ms");
                Console.WriteLine($"  Number of executions: {stage.Value.Count}");
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }

    public class DelegateChain
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== HOMEWORK 2: CUSTOM DELEGATE CHAIN ===");
            Console.WriteLine("Instructions:");
            Console.WriteLine("1. Implement the DataProcessingPipeline class");
            Console.WriteLine("2. Implement all processing methods (RemoveSpaces, ToUpperCase, etc.)");
            Console.WriteLine("3. Create a multicast delegate chain for processing");
            Console.WriteLine("4. Add logging and monitoring capabilities");
            Console.WriteLine("5. Demonstrate adding/removing processors from the chain");
            Console.WriteLine();

            // TODO: Students should implement the following:
            
            DataProcessingPipeline pipeline = new DataProcessingPipeline();
            ProcessingLogger logger = new ProcessingLogger();
            PerformanceMonitor monitor = new PerformanceMonitor();

            // Subscribe to events
            pipeline.ProcessingStageCompleted += logger.OnProcessingStageCompleted;
            pipeline.ProcessingStageCompleted += monitor.OnProcessingStageCompleted;

            // Create processing chain
            DataProcessor processingChain = DataProcessingPipeline.ValidateInput;
            processingChain += DataProcessingPipeline.RemoveSpaces;
            processingChain += DataProcessingPipeline.ToUpperCase;
            processingChain += DataProcessingPipeline.AddTimestamp;

            // Test the pipeline
            string testInput = "Hello World from C#";
            Console.WriteLine($"Input: {testInput}");
            
            string result = pipeline.ProcessData(testInput, processingChain);
            Console.WriteLine($"Output: {result}");

            // Demonstrate adding more processors
            processingChain += DataProcessingPipeline.ReverseString;
            processingChain += DataProcessingPipeline.EncodeBase64;

            // Test again with extended pipeline
            result = pipeline.ProcessData("Extended Pipeline Test", processingChain);
            Console.WriteLine($"Extended Output: {result}");

            // Demonstrate removing a processor
            processingChain -= DataProcessingPipeline.ReverseString;
            result = pipeline.ProcessData("Without Reverse", processingChain);
            Console.WriteLine($"Modified Output: {result}");

            // Display performance statistics
            monitor.DisplayStatistics();

            // Error handling test
            try
            {
                result = pipeline.ProcessData(null, processingChain); // Should handle null input
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handled: {ex.Message}");
            }
            

            Console.WriteLine("Please implement the missing code to complete this homework!");
            
            // Example of what the complete implementation should demonstrate:
            Console.WriteLine("\nExpected behavior:");
            Console.WriteLine("1. Chain multiple text processing operations");
            Console.WriteLine("2. Log each processing stage");
            Console.WriteLine("3. Monitor performance of each operation");
            Console.WriteLine("4. Handle errors gracefully");
            Console.WriteLine("5. Allow dynamic modification of the processing chain");

            Console.ReadKey();
        }
    }
}