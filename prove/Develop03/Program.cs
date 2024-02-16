using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Scripture Hiding Program!");
        Console.WriteLine();

        var scripture = new Scripture("Psalms 30:5", "For his anger endureth but a moment; in his favour is life: weeping may endure for a night, but joy cometh in the morning.");
        while (!scripture.AllWordsHidden)
        {
            Console.Clear();
            scripture.HideRandomWord();
            Console.WriteLine(scripture.GetHiddenText());
            Console.WriteLine("Press Enter to continue or type 'quit' to exit.");
            var userInput = Console.ReadLine();
            if (userInput.Equals("quit", StringComparison.CurrentCultureIgnoreCase))
                break;
        }

        Console.WriteLine("Thank you for using the Scripture Hiding Program!");
    }
}

class Scripture(string reference, string text)
{
    private readonly string reference = reference;
    private readonly string text = text;
    private readonly Random random = new();
    private readonly List<string> words = text.Split(' ').Select(word => word.Trim()).ToList();

    public bool AllWordsHidden => words.All(word => word == "_");

    public void HideRandomWord()
    {
        int index = random.Next(words.Count);
        words[index] = "_";
    }

    public string GetHiddenText()
    {
        return $"{reference}: {string.Join(" ", words)}";
    }
}
