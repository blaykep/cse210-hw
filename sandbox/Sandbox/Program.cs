using System;

class Program 
{

    static void Main(string[] args) {
        BaldEagle joey = new BaldEagle("Joey");
        BaldEagle dragon = new BaldEagle("Dragon");

        joey.MakeSound();
        dragon.MakeSound();
    }
}