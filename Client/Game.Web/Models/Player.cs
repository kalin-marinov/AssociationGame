
using System;

public class Player : IEquatable<Player>
{
    public string Name { get; set; }

    public string TeamMate { get; set; }

    public Player()
    {
        
    }

    public Player(string name)
    {
        this.Name = name;
    }

    public bool Equals(Player other)
    {
        return this.Name.Equals(other.Name);
    }

    public override int GetHashCode()
    {
        return this.Name.GetHashCode();
    }
}