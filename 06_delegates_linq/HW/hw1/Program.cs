using System;

namespace DelegatesLinQ.Homework
{
    // Event delegates for different operations

    // Định nghĩa delegate cho sự kiện tính toán
    // Delegate này sẽ được gọi khi một phép tính được thực hiện thành công
    public delegate void CalculationEventHandler(string operation, double operand1, double operand2, double result);

    // Định nghĩa delegate cho sự kiện lỗi
    // Delegate này sẽ được gọi khi xảy ra lỗi trong quá trình tính toán
    public delegate void ErrorEventHandler(string operation, string errorMessage);

    /// <summary>
    /// Homework 1: Event Calculator
    /// Create a calculator class with events for each operation.
    /// 
    /// Requirements:
    /// 1. Create events for each mathematical operation (Add, Subtract, Multiply, Divide)
    /// 2. Create events for errors (like division by zero)
    /// 3. Create subscriber classes that handle these events:
    ///    - Logger: Logs all operations to console
    ///    - Auditor: Keeps track of operation count
    ///    - ErrorHandler: Handles and displays errors
    /// 4. Demonstrate all operations and error handling
    /// 
    /// Techniques used: Similar to 6_5_EventApp
    /// - Event declaration and raising
    /// - Multiple subscribers
    /// - Event handler methods
    /// </summary>
    public class EventCalculator
    {
        // TODO: Declare events for calculation operations
        // Khai báo hai sự kiện chính của calculator
         public event CalculationEventHandler? OperationPerformed;
         public event ErrorEventHandler ErrorOccurred;

        public double Add(double a, double b)
        {
           double result = a + b;
            OnOperationPerformed("Addition", a, b, result);
            return result;
        }

        public double Subtract(double a, double b)
        {
            double result = a - b;
            OnOperationPerformed("Subtraction", a, b, result);
            return result;
        }

        public double Multiply(double a, double b)
        {
            double result = a * b;
            OnOperationPerformed("Multiplication", a, b, result);
            return result;
        }

        public double? Divide(double a, double b)
        {
            if (b == 0)
            {
                OnErrorOccurred("Division", "Cannot divide by zero");
                return null; // Cho phép chương trình tiếp tục chạy
            }

            double result = a / b;
            OnOperationPerformed("Division", a, b, result);
            return result;
        }

        // TODO: Create protected methods to raise events

        // Phương thức protected để kích hoạt sự kiện OperationPerformed
        protected virtual void OnOperationPerformed(string operation, double operand1, double operand2, double result)
        {
            OperationPerformed?.Invoke(operation, operand1, operand2, result);
        }

        // Phương thức protected để kích hoạt sự kiện ErrorOccurred
        protected virtual void OnErrorOccurred(string operation, string errorMessage)
        {
            ErrorOccurred?.Invoke(operation, errorMessage);
        }
    }

    // TODO: Create subscriber classes
    // <summary>
    /// Lớp ghi log các phép tính và lỗi
    /// </summary>
    public class CalculationLogger
    {
        // TODO: Implement event handlers for logging operations and errors

        // Xử lý sự kiện khi phép tính được thực hiện
        public void OnOperationPerformed(string operation, double operand1, double operand2, double result)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"LOG: {operation} of {operand1} and {operand2} = {result}");
            Console.ResetColor();
        }

        // Xử lý sự kiện khi có lỗi xảy ra
        public void OnErrorOccurred(string operation, string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"LOG ERROR: {operation} - {errorMessage}");
            Console.ResetColor();
        }
    }
    
    /// <summary>
    /// Lớp thống kê các phép tính đã thực hiện
    /// </summary>
    public class CalculationAuditor
    {
        // TODO: Keep track of operation counts
        private int _operationCount = 0;

        // Thống kê số lần thực hiện mỗi loại phép tính
        private Dictionary<string, int> _operationTypes = new Dictionary<string, int>();

        // Xử lý sự kiện khi phép tính được thực hiện
        public void OnOperationPerformed(string operation, double operand1, double operand2, double result)
        {
            _operationCount++;// Tăng tổng số phép tính
            if (!_operationTypes.ContainsKey(operation))
                _operationTypes[operation] = 0;// Cập nhật số lần thực hiện cho loại phép tính cụ thể
            _operationTypes[operation]++;
        }
        // Hiển thị thống kê các phép tính
        public void DisplayStatistics()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=== Operation Statistics ===");
            Console.WriteLine($"Total operations performed: {_operationCount}");
            Console.WriteLine("\nBreakdown by operation type:");
            foreach (var op in _operationTypes)
            {
                Console.WriteLine($"{op.Key}: {op.Value} time(s)");
            }
            Console.ResetColor();
        }
    }
    
    /// <summary>
    /// Lớp xử lý các lỗi xảy ra trong quá trình tính toán
    /// </summary>
    public class ErrorHandler
    {
        // TODO: Handle errors with special formatting
        public void OnErrorOccurred(string operation, string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR HANDLER: {operation} operation failed!");
            Console.WriteLine($"Reason: {errorMessage}");
            Console.WriteLine("Please check your inputs and try again.");
            Console.ResetColor();
        }
    }

    public class HW1_EventCalculator
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== HOMEWORK 1: EVENT CALCULATOR ===");
            Console.WriteLine("Instructions:");
            Console.WriteLine("1. Implement the EventCalculator class with events for each operation");
            Console.WriteLine("2. Implement subscriber classes: CalculationLogger, CalculationAuditor, ErrorHandler");
            Console.WriteLine("3. Subscribe to events and test all operations including error cases");
            Console.WriteLine();

            // TODO: You should implement the following:
            
            EventCalculator calculator = new EventCalculator();
            CalculationLogger logger = new CalculationLogger();
            CalculationAuditor auditor = new CalculationAuditor();
            ErrorHandler errorHandler = new ErrorHandler();

            // Subscribe to events
            calculator.OperationPerformed += logger.OnOperationPerformed;
            calculator.OperationPerformed += auditor.OnOperationPerformed;
            calculator.ErrorOccurred += logger.OnErrorOccurred;
            calculator.ErrorOccurred += errorHandler.OnErrorOccurred;

            // Test operations
            calculator.Add(10, 5);
            calculator.Subtract(10, 3);
            calculator.Multiply(4, 7);
            calculator.Divide(15, 3);
            calculator.Divide(10, 0); // Should trigger error

            // Display statistics
            auditor.DisplayStatistics();
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}