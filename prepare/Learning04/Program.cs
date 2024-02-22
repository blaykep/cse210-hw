using System;

class Program
{
    static void Main(string[] args)
    {
        Assignment a1 = new("Blayke Peapealalo", "Multiplication");
        Console.WriteLine(a1.GetSummary());

        MathAssignment a2 = new("Paige LaRocco", "Fractions", "7.3", "8-19");
        Console.WriteLine(a2.GetSummary());
        Console.WriteLine(a2.GetHomeworkList());

        WritingAssignment a3 = new("Ty Wells", "European history", "The Causes of World War");
        Console.WriteLine(a3.GetSummary());
        Console.WriteLine(a3.GetWritingInformation());
    }
}