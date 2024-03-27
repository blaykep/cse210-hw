using System;
using System.Collections.Generic;

public enum ActionType
{
    Move,
    Take,
    Use,
    Talk,
    Quit
}

public class Item
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class Character
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class Location
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public List<Character> Characters { get; set; }
    public Dictionary<string, Location> Exits { get; set; }

    // Ensure Exits is initialized
    public Location()
    {
        Exits = new Dictionary<string, Location>();
    }
}

public class Player
{
    public string Name { get; set; }
    public Location CurrentLocation { get; set; }
    public List<Item> Inventory { get; set; }
}

public class Game
{
    private Player player;
    private Location currentLocation;

    public Game(Player player, Location startingLocation)
    {
        this.player = player;
        this.currentLocation = startingLocation;
    }

    public void Start()
    {
        Console.WriteLine("Welcome to the Text-Based Adventure Game!");

        while (true)
        {
            Console.WriteLine("\n" + currentLocation.Description);

            Console.WriteLine("Available actions:");
            foreach (var exit in currentLocation.Exits)
            {
                Console.WriteLine($"- Move to {exit.Key}");
            }

            Console.WriteLine("- Quit");

            Console.Write("Enter your action: ");
            string input = Console.ReadLine();

            ActionType action;
            Enum.TryParse(input, true, out action);

            switch (action)
            {
                case ActionType.Move:
                    Move();
                    break;
                case ActionType.Take:
                    // Implement Take action
                    break;
                case ActionType.Use:
                    // Implement Use action
                    break;
                case ActionType.Talk:
                    // Implement Talk action
                    break;
                case ActionType.Quit:
                    Console.WriteLine("Thank you for playing!");
                    return;
                default:
                    Console.WriteLine("Invalid action. Please try again.");
                    break;
            }
        }
    }

    private void Move()
    {
        Console.Write("Enter direction (e.g., 'north', 'south'): ");
        string direction = Console.ReadLine().ToLower();

        if (currentLocation.Exits.ContainsKey(direction))
        {
            currentLocation = currentLocation.Exits[direction];
        }
        else
        {
            Console.WriteLine("You cannot move in that direction.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Location startingLocation = new Location
        {
            Name = "Forest",
            Description = "You find yourself in a dark forest.",
            Exits = new Dictionary<string, Location>()
            {
                { "north", new Location { Name = "Cave", Description = "A dark cave entrance." } },
                { "south", new Location { Name = "River", Description = "A rushing river blocks your path." } }
            }
        };

        Player player = new Player { Name = "Player", CurrentLocation = startingLocation, Inventory = new List<Item>() };

        Game game = new Game(player, startingLocation);
        game.Start();
    }
}
