using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Tracker tracker = new Tracker();
        tracker.LoadGoals(); // Load previously saved goals

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Eternal Quest - Goal Tracker");
            Console.WriteLine("1. View Goals");
            Console.WriteLine("2. Add New Goal");
            Console.WriteLine("3. Record Accomplishment");
            Console.WriteLine("4. View Score");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    tracker.DisplayGoals();
                    break;
                case "2":
                    tracker.CreateGoal();
                    break;
                case "3":
                    tracker.RecordAccomplishment();
                    break;
                case "4":
                    Console.WriteLine($"Your current score: {tracker.GetScore()}");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    break;
                case "5":
                    tracker.SaveGoals(); // Save goals before exiting
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number from 1 to 5.");
                    break;
            }
        }
    }
}

abstract class Goal
{
    public string name { get; protected set; }
    protected bool completed; 

    public Goal(string name)
    {
        this.name = name;
        this.completed = false;
    }

    public abstract void DisplayStatus();

    public abstract int RecordAccomplishment();

    public bool IsCompleted()
    {
        return completed;
    }
}

class SimpleGoal : Goal
{
    private int points;

    public SimpleGoal(string name, int points) : base(name)
    {
        this.points = points;
    }

    public SimpleGoal(string name, bool isCompleted) : base(name)
    {
    }

    public override void DisplayStatus()
    {
        Console.WriteLine($"{name}: {(completed ? "[X]" : "[ ]")}");
    }

    public override int RecordAccomplishment()
    {
        completed = true;
        return points;
    }
}

class EternalGoal : Goal
{
    private int points;

    public EternalGoal(string name, int points) : base(name)
    {
        this.points = points;
    }

    public EternalGoal(string name, bool isCompleted) : base(name)
    {
    }

    public override void DisplayStatus()
    {
        Console.WriteLine($"{name}: {(completed ? "[∞]" : "[ ]")}");
    }

    public override int RecordAccomplishment()
    {
        return points;
    }
}

class ChecklistGoal : Goal
{
    private int requiredCount;
    private int pointsPerCheck;
    private int currentCount;

    public ChecklistGoal(string name, int requiredCount, int pointsPerCheck) : base(name)
    {
        this.requiredCount = requiredCount;
        this.pointsPerCheck = pointsPerCheck;
        this.currentCount = 0;
    }

    public ChecklistGoal(string name, bool isCompleted, int v) : base(name)
    {
    }

    public override void DisplayStatus()
    {
        Console.WriteLine($"{name}: {(completed ? $"[X] Completed {currentCount}/{requiredCount} times" : $"[ ] Completed {currentCount}/{requiredCount} times")}");
    }

    public override int RecordAccomplishment()
    {
        currentCount++;
        if (currentCount == requiredCount)
        {
            completed = true;
            return pointsPerCheck * requiredCount + 500; // Bonus points
        }
        else
        {
            return pointsPerCheck;
        }
    }
}

class Tracker
{
    private List<Goal> goals = new List<Goal>();
    private int score = 0;

    public void DisplayGoals()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals set yet.");
            return;
        }

        Console.WriteLine("Your Goals:");
        foreach (Goal goal in goals)
        {
            goal.DisplayStatus();
        }
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    public void CreateGoal()
    {
        Console.Write("Enter the name of the goal: ");
        string name = Console.ReadLine();

        Console.WriteLine("Select the type of goal:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Enter points for completing the goal: ");
                int points = int.Parse(Console.ReadLine());
                goals.Add(new SimpleGoal(name, points));
                break;
            case "2":
                Console.Write("Enter points for each recording: ");
                int eternalPoints = int.Parse(Console.ReadLine());
                goals.Add(new EternalGoal(name, eternalPoints));
                break;
            case "3":
                Console.Write("Enter required count for completion: ");
                int requiredCount = int.Parse(Console.ReadLine());
                Console.Write("Enter points for each completion: ");
                int checklistPoints = int.Parse(Console.ReadLine());
                goals.Add(new ChecklistGoal(name, requiredCount, checklistPoints));
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
        Console.WriteLine("Goal added successfully.");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    public void RecordAccomplishment()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals set yet.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Select the goal you accomplished:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].ToString()}");
        }
        Console.Write("Enter the number corresponding to the goal: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index >= 0 && index < goals.Count)
        {
            int pointsEarned = goals[index].RecordAccomplishment();
            score += pointsEarned;
            Console.WriteLine($"You earned {pointsEarned} points.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Invalid goal selection.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            foreach (Goal goal in goals)
            {
                writer.WriteLine($"{goal.GetType().Name}|{goal.name}|{goal.IsCompleted()}");
            }
        }
    }

    public void LoadGoals()
{
    if (File.Exists("goals.txt"))
    {
        using (StreamReader reader = new StreamReader("goals.txt"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split('|');
                bool isCompleted;
                if (!bool.TryParse(parts[2], out isCompleted)) // Parse completion status as boolean
                {
                    Console.WriteLine($"Error parsing completion status for goal: {parts[1]}");
                    continue; // Skip this goal if completion status cannot be parsed
                }
                switch (parts[0])
                {
                    case "SimpleGoal":
                        goals.Add(new SimpleGoal(parts[1], isCompleted));
                        break;
                    case "EternalGoal":
                        goals.Add(new EternalGoal(parts[1], isCompleted));
                        break;
                    case "ChecklistGoal":
                        if (parts.Length >= 4)
                        {
                            goals.Add(new ChecklistGoal(parts[1], isCompleted, int.Parse(parts[3])));
                        }
                        else
                        {
                            Console.WriteLine($"Error parsing checklist goal: {parts[1]} - Not enough elements.");
                        }
                        //goals.Add(new ChecklistGoal(parts[1], isCompleted, int.Parse(parts[3])));
                        break;
                    default:
                        Console.WriteLine("Unknown goal type in file.");
                        break;
                }
            }
        }
    }
}
}