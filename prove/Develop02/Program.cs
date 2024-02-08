using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, welcome to your journal!");

        Journal myJournal = new();
        myJournal.LoadJournal("journal.txt");

        int entryNumber = 1; // Change this to the desired entry number
        myJournal.Display(entryNumber);

        // Example of generating a question
        string generatedQuestion = QuestionGenerator.GenerateQuestion();
        Console.WriteLine($"Generated Question: {generatedQuestion}");
    }

    public class Journal
    {
        // Dictionary that is all of the Journal Entries
        private readonly Dictionary<int, string> journalEntries = [];

        public void Display(int entryNumber)
        {
            if (journalEntries.TryGetValue(entryNumber, out string value))
            {
                Console.WriteLine($"{value}");
            }
            else
            {
                Console.WriteLine($"Entry {entryNumber} not found.");
            }
        }

        public void LoadJournal(string fileName)
        {
            try
            {
                string[] fileLines = File.ReadAllLines(fileName);
                for (int i = 0; i < fileLines.Length; i++)
                {
                    journalEntries[i + 1] = fileLines[i];
                }
                Console.WriteLine("File loaded into Journal.");
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred while reading the file: " + e.Message);
            }
        }

        public static void SaveJournal(string fileName)
        {
            // Add code to save the journalEntries dictionary to a file
        }

        public static void SaveEntry(int entryNumber, string entryText)
        {
            // Add code to save a new entry to the journalEntries dictionary
        }
    }

    public class QuestionGenerator
    {
        public static string GenerateQuestion()
        {
            var questions = new List<string> { "What was the best part of your day?", "Did you serve anyone today or did anyone serve you?", "What did you eat today?", "Who are you grateful for today?", "What was your song of the day?" };

            int count = questions.Count;
            var r = new Random();
            int listIndex = r.Next(count);

            string generatedQuestion = questions[listIndex];
            return generatedQuestion;
        }
    }
}
