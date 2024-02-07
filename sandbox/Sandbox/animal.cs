using System;

public abstract class Animal {

    private string name;

    public Animal(string name) {
        this.name = name;

    }

    public virtual void MakeSound() {
        Console.WriteLine("**Ominous Silence**");
    }


}