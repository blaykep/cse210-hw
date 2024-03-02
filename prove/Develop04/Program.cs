using System;
using System.Data;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices.Marshalling;
using System.Threading;

abstract class Mindfulness
{
    protected int duration;

    public Mindfulness(int duration)
    {
        this.duration = duration;
    }

    public virtual void Start()
    {
        Console.WriteLine($"Starting {this.GetType().Name} activity...");
        CommonStartMessage();
        PerformActivity();
        CommonEndMessage();
        ShowActivityData();
    }

    protected virtual void CommonStartMessage()
    {
        Console.WriteLine("This activity will help you relax and focus.");
    }

    protected virtual void CommonEndMessage()
    {
        Console.WriteLine($"Good job! You have completed the {this.GetType().Name} activity.");
    }

    protected abstract void PerformActivity();
    protected virtual void ShowActivityData() {
        Console.WriteLine($"You participated for {duration} seconds. Thank you for playing!");
    }
}

class Breathing : Mindfulness
{
    public Breathing(int duration) : base(duration) { }

    protected override void CommonStartMessage()
    {
        Console.WriteLine("This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");
    }

    protected override void PerformActivity()
    {
        for (int i = 0; i < duration; i++)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(1000);
            Console.WriteLine("Breathe out...");
            Thread.Sleep(1000);
        }
    }

    protected override void CommonEndMessage()
    {
        Console.Clear();
        base.CommonEndMessage();
        Console.WriteLine($"You calmed down for {duration} seconds.");
        Console.WriteLine($"Thank you for playing {GetType().Name} activity!");
        Thread.Sleep(2000);
    }
    
}

class Reflection : Mindfulness
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public Reflection(int duration) : base(duration) { }

    protected override void CommonStartMessage()
    {
        Console.WriteLine("This activity will help you reflect on times in your life when you have shown strength and resilience.");
    }

    protected override void PerformActivity()
    {
        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Length)];
        Console.WriteLine(prompt);
        Thread.Sleep(1000);
        foreach (string question in questions)
        {
            Console.WriteLine(question);
            Thread.Sleep(4000);
        }
    }
     protected override void CommonEndMessage()
    {
        Console.Clear();
        base.CommonEndMessage();
        Console.WriteLine($"You reflected for {duration} seconds.");
        Console.WriteLine($"Thank you for playing {GetType().Name} activity!");
        Thread.Sleep(3000);
    }
    
}


class Listing : Mindfulness
{
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public Listing(int duration) : base(duration) { }

    protected override void CommonStartMessage()
    {
        Console.WriteLine("This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
    }

    protected override void PerformActivity()
    {
        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Length)];
        Console.WriteLine(prompt);
        Console.WriteLine($"You have {duration} seconds to list items...");
        //Thread.Sleep(duration * 1000);

        Console.WriteLine("Begin listing and hit enter after each item:");
        for (int i = 1; i <= duration; i++) {
            //Console.Write($"Item {i}: ");
            string item = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(item)) {
                Console.WriteLine("Invalid input. Try again.");
            }
            else {
                Console.WriteLine($"Item {i}: {item}");
            }
        }
    }

    protected override void ShowActivityData()
    {
        base.ShowActivityData();
    }
     protected override void CommonEndMessage()
    {
        Console.Clear();
        base.CommonEndMessage();
        Console.WriteLine($"You listed for {duration} seconds.");
        Console.WriteLine($"Thank you for playing {GetType().Name} activity!");
        Thread.Sleep(3000);
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");

            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    LoadingMessage(() =>{

                    Console.Write("Enter duration (in seconds): ");
                    int durationBreathing = int.Parse(Console.ReadLine());
                    Breathing breathingActivity = new Breathing(durationBreathing);
                    breathingActivity.Start();
                    });
                    break;
                case "2":
                    LoadingMessage(() => {

                    Console.Write("Enter duration (in seconds): ");
                    int durationReflection = int.Parse(Console.ReadLine());
                    Reflection reflectionActivity = new Reflection(durationReflection);
                    reflectionActivity.Start();
                    });
                    break;
                case "3":
                    LoadingMessage(() => {
                    Console.Write("Enter duration (in seconds): ");
                    int durationListing = int.Parse(Console.ReadLine());
                    Listing listingActivity = new Listing(durationListing);
                    listingActivity.Start();
                    });
                    break;
                case "4":
                    Console.WriteLine($"Sad to see you go, but have a great day!");
                    Thread.Sleep(2000);
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number from 1 to 4.");
                    break;
            }
        }
    }

    static void LoadingMessage(Action activity) {
        Console.WriteLine("Loading your selection...");
        Thread.Sleep(1500); 
        Console.Clear();

        Console.Write("Loading");
        for (int i = 0; i < 3; i++) {
            Thread.Sleep(500);
            Console.Write(",");
        }
        Console.WriteLine();

        activity.Invoke();
        Console.Clear();
    }
}