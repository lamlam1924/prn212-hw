using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DelegatesLinQ.Homework
{
    // Data models for the homework
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
        public double GPA { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
        public DateTime EnrollmentDate { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }

    public class Course
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public double Grade { get; set; } // 0-4.0 scale
        public string Semester { get; set; }
        public string Instructor { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    /// <summary>
    /// Homework 3: LINQ Data Processor
    /// 
    /// This is the most challenging homework requiring students to:
    /// 1. Use complex LINQ operations with multiple data sources
    /// 2. Implement custom extension methods
    /// 3. Create generic delegates and expressions
    /// 4. Handle advanced scenarios like pivot operations, statistical analysis
    /// 5. Implement caching and performance optimization
    /// 
    /// Advanced Requirements:
    /// - Custom LINQ extension methods
    /// - Expression trees and dynamic queries
    /// - Performance comparison between different approaches
    /// - Statistical calculations and reporting
    /// - Data transformation and pivot operations
    /// </summary>
    public class LinqDataProcessor
    {
        private List<Student> _students;

        public LinqDataProcessor()
        {
            _students = GenerateSampleData();
        }
        private double CalculateMedian(List<double> numbers)
        {
            var sorted = numbers.OrderBy(n => n).ToList();
            int mid = sorted.Count / 2;
            return sorted.Count % 2 == 0
                ? (sorted[mid - 1] + sorted[mid]) / 2
                : sorted[mid];
        }

        private double CalculateStandardDeviation(List<double> numbers)
        {
            double mean = numbers.Average();
            double sumOfSquares = numbers.Sum(x => Math.Pow(x - mean, 2));
            return Math.Sqrt(sumOfSquares / (numbers.Count - 1));
        }
        //Main
        public static void Main(string[] args)
        {
            Console.WriteLine("=== HOMEWORK 3: LINQ DATA PROCESSOR ===");
            Console.WriteLine();

            Console.WriteLine("BASIC REQUIREMENTS:");
            Console.WriteLine("1. Implement basic LINQ queries (filtering, grouping, sorting)");
            Console.WriteLine("2. Use SelectMany for course data");
            Console.WriteLine("3. Calculate averages and aggregations");
            Console.WriteLine();

            Console.WriteLine("ADVANCED REQUIREMENTS:");
            Console.WriteLine("1. Create custom LINQ extension methods");
            Console.WriteLine("2. Implement dynamic queries using Expression Trees");
            Console.WriteLine("3. Perform statistical analysis (median, std dev, correlation)");
            Console.WriteLine("4. Create pivot table operations");
            Console.WriteLine();

            LinqDataProcessor processor = new LinqDataProcessor();

            // Students should implement all these methods
            processor.BasicQueries();
            processor.CustomExtensionMethods();
            processor.DynamicQueries();
            processor.StatisticalAnalysis();
            processor.PivotOperations();

            Console.ReadKey();
        }


        // BASIC REQUIREMENTS (using techniques from sample codes)

        public void BasicQueries()
        {
            // TODO: Implement basic LINQ queries similar to 6_8_LinQObject

            Console.WriteLine("=== BASIC LINQ QUERIES ===");
            // Implementation needed by students

            // 1. Find all students with GPA > 3.5
            Console.WriteLine("1. Students with GPA > 3.5:");
            var highPerformers = _students
                .Where(s => s.GPA > 3.5)
                .OrderByDescending(s => s.GPA);

            foreach (var student in highPerformers)
            {
                Console.WriteLine($"- {student.Name}: GPA {student.GPA:F2}");
            }
            // 2. Group students by major
            Console.WriteLine("\n2. Students grouped by major:");
            var byMajor = _students.GroupBy(s => s.Major);

            foreach (var group in byMajor)
            {
                Console.WriteLine($"\n{group.Key}:");
                foreach (var student in group)
                {
                    Console.WriteLine($"- {student.Name}");
                }
            }
            // 3. Calculate average GPA per major
            Console.WriteLine("\n3. Average GPA by major:");
            var avgGPAByMajor = _students
                .GroupBy(s => s.Major)
                .Select(g => new { Major = g.Key, AvgGPA = g.Average(s => s.GPA) });

            foreach (var avg in avgGPAByMajor)
            {
                Console.WriteLine($"- {avg.Major}: {avg.AvgGPA:F2}");
            }
            // 4. Find students enrolled in specific courses
            Console.WriteLine("\n4. Students enrolled in CS101:");
            var studentsInCS101 = _students
                .Where(s => s.Courses.Any(c => c.Code == "CS101"));

            foreach (var student in studentsInCS101)
            {
                Console.WriteLine($"- {student.Name}");
            }
            // 5. Sort students by enrollment date
            Console.WriteLine("\n5. Students sorted by enrollment date:");
            var sortedByEnrollment = _students
                .OrderBy(s => s.EnrollmentDate)
                .Select(s => new { s.Name, EnrollDate = s.EnrollmentDate.ToString("yyyy-MM-dd") });

            foreach (var student in sortedByEnrollment)
            {
                Console.WriteLine($"- {student.Name}: {student.EnrollDate}");
            }
        }

        // Challenge 1: Create custom extension methods
        public void CustomExtensionMethods()
        {
            Console.WriteLine("\n=== CUSTOM EXTENSION METHODS ===");

            // TODO: Implement extension methods
            // 1. CreateAverageGPAByMajor() extension method
            // 2. FilterByAgeRange(int min, int max) extension method  
            // 3. ToGradeReport() extension method that creates formatted output
            // 4. CalculateStatistics() extension method

            // Example usage (students need to implement the extensions):
            // var highPerformers = _students.FilterByAgeRange(20, 25).Where(s => s.GPA > 3.5);
            // var gradeReport = _students.ToGradeReport();
            // var stats = _students.CalculateStatistics();

            // 1. Sử dụng FilterByAgeRange và kết hợp với điều kiện GPA
            Console.WriteLine("1. High Performers (Age 20-25, GPA > 3.5):");
            var highPerformers = _students
                .FilterByAgeRange(20, 25)
                .Where(s => s.GPA > 3.5);

            foreach (var student in highPerformers)
            {
                Console.WriteLine($"- {student.Name}: Age {student.Age}, GPA {student.GPA:F2}");
            }

            // 2. Sử dụng ToGradeReport cho một học sinh
            Console.WriteLine("\n2. Sample Grade Report:");
            var sampleStudent = _students.First();
            Console.WriteLine(sampleStudent.ToGradeReport());

            // 3. Sử dụng CalculateStatistics
            Console.WriteLine("3. Class Statistics:");
            var stats = _students.CalculateStatistics();
            Console.WriteLine($"Mean GPA: {stats.Mean:F2}");
            Console.WriteLine($"Median GPA: {stats.Median:F2}");
            Console.WriteLine($"Standard Deviation: {stats.StandardDeviation:F2}");
            Console.WriteLine($"Range: {stats.Min:F2} - {stats.Max:F2}");
            Console.WriteLine($"Total Students: {stats.Count}");
        }

        // Challenge 2: Dynamic queries using Expression Trees
        public void DynamicQueries()
        {
            Console.WriteLine("\n=== DYNAMIC QUERIES ===");

            // TODO: Research Expression Trees
            // Implement a method that builds LINQ queries dynamically based on user input
            // Example: BuildDynamicFilter("GPA", ">", "3.5")
            // This requires understanding of Expression<Func<T, bool>>

            // Students should implement:
            // 1. Dynamic filtering based on property name and value
            Console.WriteLine("1. Dynamic Filtering (GPA > 3.5):");
            var highGPAStudents = BuildDynamicFilter("GPA", ">", 3.5);
            foreach (var student in highGPAStudents)
            {
                Console.WriteLine($"- {student.Name}: {student.GPA:F2}");
            }
            // 2. Dynamic sorting by any property
            Console.WriteLine("\n2. Dynamic Sorting (by Age):");
            var sortedByAge = DynamicSort("Age");
            foreach (var student in sortedByAge)
            {
                Console.WriteLine($"- {student.Name}: Age {student.Age}");
            }
            // 3. Dynamic grouping operations
            Console.WriteLine("\n3. Dynamic Grouping (by Major):");
            var groupedByMajor = DynamicGroup("Major");
            foreach (var group in groupedByMajor)
            {
                Console.WriteLine($"\n{group.Key}:");
                foreach (var student in group)
                {
                    Console.WriteLine($"- {student.Name}");
                }
            }
        }

        private IEnumerable<Student> BuildDynamicFilter(string propertyName, string operation, object value)
        {
            var parameter = Expression.Parameter(typeof(Student), "s");
            var property = Expression.Property(parameter, propertyName);
            var convertedValue = Expression.Constant(Convert.ChangeType(value, property.Type));

            BinaryExpression condition;
            switch (operation)
            {
                case ">": condition = Expression.GreaterThan(property, convertedValue); break;
                case ">=": condition = Expression.GreaterThanOrEqual(property, convertedValue); break;
                case "<": condition = Expression.LessThan(property, convertedValue); break;
                case "<=": condition = Expression.LessThanOrEqual(property, convertedValue); break;
                case "==": condition = Expression.Equal(property, convertedValue); break;
                default: throw new ArgumentException("Unsupported operation");
            }

            var lambda = Expression.Lambda<Func<Student, bool>>(condition, parameter);
            return _students.Where(lambda.Compile());
        }

        private IEnumerable<Student> DynamicSort(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(Student), "s");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda<Func<Student, object>>(
                Expression.Convert(property, typeof(object)),
                parameter
            );
            return _students.OrderBy(lambda.Compile());
        }

        private IEnumerable<IGrouping<object, Student>> DynamicGroup(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(Student), "s");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda<Func<Student, object>>(
                Expression.Convert(property, typeof(object)),
                parameter
            );
            return _students.GroupBy(lambda.Compile());
        }

        
        // Challenge 3: Statistical Analysis with Complex Aggregations
        public void StatisticalAnalysis()
        {
            Console.WriteLine("\n=== STATISTICAL ANALYSIS ===");

            // TODO: Implement complex statistical calculations
            // 1. Calculate median GPA (requires custom logic)
            var gpas = _students.Select(s => s.GPA).ToList();
            Console.WriteLine("1. Basic Statistics:");
            Console.WriteLine($"Mean GPA: {gpas.Average():F2}");
            Console.WriteLine($"Median GPA: {CalculateMedian(gpas):F2}");
            Console.WriteLine($"Standard Deviation: {CalculateStandardDeviation(gpas):F2}");
            // 2. Calculate standard deviation of GPAs
            var sortedGPAs = gpas.OrderBy(g => g).ToList();
            Console.WriteLine("\n2. Percentile Rankings:");
            Console.WriteLine($"25th Percentile: {sortedGPAs[(int)(sortedGPAs.Count * 0.25)]:F2}");
            Console.WriteLine($"75th Percentile: {sortedGPAs[(int)(sortedGPAs.Count * 0.75)]:F2}");
            // 3. Find correlation between age and GPA
            var correlation = CalculateCorrelation(
                _students.Select(s => (double)s.Age),
                _students.Select(s => s.GPA)
            );
            Console.WriteLine($"\n3. Age-GPA Correlation: {correlation:F2}");
            // 4. Identify outliers using statistical methods
            var q1 = sortedGPAs[(int)(sortedGPAs.Count * 0.25)];
            var q3 = sortedGPAs[(int)(sortedGPAs.Count * 0.75)];
            var iqr = q3 - q1;
            var lowerBound = q1 - (1.5 * iqr);
            var upperBound = q3 + (1.5 * iqr);

            Console.WriteLine("\n4. GPA Outliers:");
            foreach (var student in _students.Where(s => s.GPA < lowerBound || s.GPA > upperBound))
            {
                Console.WriteLine($"- {student.Name}: {student.GPA:F2}");
            }
            // 5. Create percentile rankings
            Console.WriteLine("\n5. Percentile Rankings:");
            var sortedStudents = _students
                .OrderByDescending(s => s.GPA)
                .Select((student, index) => new
                {
                    student.Name,
                    student.GPA,
                    Percentile = 100 * (1 - (double)index / _students.Count())
                });

            Console.WriteLine("Student Rankings by GPA:");
            Console.WriteLine("Name            | GPA  | Percentile Rank");
            Console.WriteLine("----------------|------|----------------");
            foreach (var student in sortedStudents)
            {
                Console.WriteLine(
                    $"{student.Name,-15} | {student.GPA,4:F2} | {student.Percentile,7:F1}th");
            }
            // This requires research into statistical formulas and advanced LINQ usage
        }

        private double CalculateCorrelation(IEnumerable<double> x, IEnumerable<double> y)
        {
            var xArray = x.ToArray();
            var yArray = y.ToArray();
            var meanX = xArray.Average();
            var meanY = yArray.Average();

            var sumXY = xArray.Zip(yArray, (a, b) => (a - meanX) * (b - meanY)).Sum();
            var sumXX = xArray.Sum(a => Math.Pow(a - meanX, 2));
            var sumYY = yArray.Sum(b => Math.Pow(b - meanY, 2));

            return sumXY / Math.Sqrt(sumXX * sumYY);
        }
        // Challenge 4: Data Pivot Operations
        public void PivotOperations()
        {
            Console.WriteLine("=== PIVOT OPERATIONS ===");

            // TODO: Research pivot table concepts
            // Create pivot tables showing:
            // 1. Students by Major vs GPA ranges (3.0-3.5, 3.5-4.0, etc.)
            Console.WriteLine("1. GPA Distribution by Major:");
            var gpaPivot = _students
                .GroupBy(s => s.Major)
                .Select(g => new
                {
                    Major = g.Key,
                    LowGPA = g.Count(s => s.GPA < 3.0),
                    MediumGPA = g.Count(s => s.GPA >= 3.0 && s.GPA < 3.5),
                    HighGPA = g.Count(s => s.GPA >= 3.5)
                });

            Console.WriteLine("Major      | <3.0 | 3.0-3.5 | >3.5");
            Console.WriteLine("------------|-------|----------|------");
            foreach (var row in gpaPivot)
            {
                Console.WriteLine(
                    $"{row.Major,-10} | {row.LowGPA,4} | {row.MediumGPA,8} | {row.HighGPA,4}");
            }
            // 2. Course enrollment by semester and major
            Console.WriteLine("\n2. Course Enrollment by Semester and Major:");
            var enrollmentPivot = _students
                .SelectMany(s => s.Courses.Select(c => new { s.Major, c.Semester }))
                .GroupBy(x => new { x.Major, x.Semester })
                .Select(g => new
                {
                    g.Key.Major,
                    g.Key.Semester,
                    Count = g.Count()
                })
                .OrderBy(x => x.Major)
                .ThenBy(x => x.Semester);

            var currentMajor = "";
            foreach (var row in enrollmentPivot)
            {
                if (row.Major != currentMajor)
                {
                    Console.WriteLine($"\n{row.Major}:");
                    currentMajor = row.Major;
                }
                Console.WriteLine($"  {row.Semester}: {row.Count} students");
            }
            // 3. Grade distribution across instructors
            Console.WriteLine("\n3. Grade Distribution by Instructor:");
            var gradeDistribution = _students
                .SelectMany(s => s.Courses.Select(c => new { c.Instructor, c.Grade }))
                .GroupBy(x => x.Instructor)
                .Select(g => new
                {
                    Instructor = g.Key,
                    AverageGrade = g.Average(x => x.Grade),
                    Count = g.Count()
                })
                .OrderByDescending(x => x.AverageGrade);

            foreach (var row in gradeDistribution)
            {
                Console.WriteLine($"{row.Instructor,-15} | Avg Grade: {row.AverageGrade:F2} | Students: {row.Count}");
            }
            // This requires understanding of GroupBy with multiple keys and complex projections
        }

        // Sample data generator
        private List<Student> GenerateSampleData()
        {
            return new List<Student>
            {
                new Student
                {
                    Id = 1, Name = "Alice Johnson", Age = 20, Major = "Computer Science",
                    GPA = 3.8, EnrollmentDate = new DateTime(2022, 9, 1),
                    Email = "alice.j@university.edu",
                    Address = new Address { City = "Seattle", State = "WA", ZipCode = "98101" },
                    Courses = new List<Course>
                    {
                        new Course { Code = "CS101", Name = "Intro to Programming", Credits = 3, Grade = 3.7, Semester = "Fall 2022", Instructor = "Dr. Smith" },
                        new Course { Code = "MATH201", Name = "Calculus II", Credits = 4, Grade = 3.9, Semester = "Fall 2022", Instructor = "Prof. Johnson" }
                    }
                },
                new Student
                {
                    Id = 2, Name = "Bob Wilson", Age = 22, Major = "Mathematics",
                    GPA = 3.2, EnrollmentDate = new DateTime(2021, 9, 1),
                    Email = "bob.w@university.edu",
                    Address = new Address { City = "Portland", State = "OR", ZipCode = "97201" },
                    Courses = new List<Course>
                    {
                        new Course { Code = "MATH301", Name = "Linear Algebra", Credits = 3, Grade = 3.3, Semester = "Spring 2023", Instructor = "Dr. Brown" },
                        new Course { Code = "STAT101", Name = "Statistics", Credits = 3, Grade = 3.1, Semester = "Spring 2023", Instructor = "Prof. Davis" }
                    }
                },
                // Add more sample students...
                new Student
                {
                    Id = 3, Name = "Carol Davis", Age = 19, Major = "Computer Science",
                    GPA = 3.9, EnrollmentDate = new DateTime(2023, 9, 1),
                    Email = "carol.d@university.edu",
                    Address = new Address { City = "San Francisco", State = "CA", ZipCode = "94101" },
                    Courses = new List<Course>
                    {
                        new Course { Code = "CS102", Name = "Data Structures", Credits = 4, Grade = 4.0, Semester = "Fall 2023", Instructor = "Dr. Smith" },
                        new Course { Code = "CS201", Name = "Algorithms", Credits = 3, Grade = 3.8, Semester = "Fall 2023", Instructor = "Prof. Lee" }
                    }
                }
            };
        }
    }

    // TODO: Implement these extension methods
    public static class StudentExtensions
    {
        // Challenge: Implement custom extension methods
        public static IEnumerable<Student> FilterByAgeRange(this IEnumerable<Student> students, int minAge, int maxAge)
        {
            return students.Where(s => s.Age >= minAge && s.Age <= maxAge);
        }
        public static Dictionary<string, double> AverageGPAByMajor(this IEnumerable<Student> students)
        {
            return students
            .GroupBy(s => s.Major)
            .ToDictionary(
                g => g.Key,
                g => g.Average(s => s.GPA)
            );
        }
        public static string ToGradeReport(this Student student)
        {
            var report = $"Grade Report for {student.Name} (ID: {student.Id})\n";
            report += $"Major: {student.Major}, GPA: {student.GPA:F2}\n";
            report += "Courses:\n";

            foreach (var course in student.Courses)
            {
                report += $"- {course.Code}: {course.Name}\n";
                report += $"  Grade: {course.Grade:F1}, Credits: {course.Credits}\n";
            }

            return report;
        }
        public static StudentStatistics CalculateStatistics(this IEnumerable<Student> students)
        {
            var gpas = students.Select(s => s.GPA).ToList();
            return new StudentStatistics
            {
                Mean = gpas.Average(),
                Median = CalculateMedian(gpas),
                StandardDeviation = CalculateStandardDeviation(gpas),
                Count = gpas.Count,
                Min = gpas.Min(),
                Max = gpas.Max()
            };
        }

        private static double CalculateMedian(List<double> numbers)
        {
            var sorted = numbers.OrderBy(n => n).ToList();
            int mid = sorted.Count / 2;
            return sorted.Count % 2 == 0
                ? (sorted[mid - 1] + sorted[mid]) / 2
                : sorted[mid];
        }

        private static double CalculateStandardDeviation(List<double> numbers)
        {
            double mean = numbers.Average();
            double sumOfSquares = numbers.Sum(x => Math.Pow(x - mean, 2));
            return Math.Sqrt(sumOfSquares / (numbers.Count - 1));
        }
    }

    // TODO: Define this class for statistical operations
    public class StudentStatistics
    {
        // Properties for mean, median, mode, standard deviation, etc.
        public double Mean { get; set; }          // Giá trị trung bình
        public double Median { get; set; }        // Giá trị trung vị
        public double StandardDeviation { get; set; }  // Độ lệch chuẩn
        public int Count { get; set; }            // Số lượng mẫu
        public double Min { get; set; }           // Giá trị nhỏ nhất
        public double Max { get; set; }           // Giá trị lớn nhất
    }
}
