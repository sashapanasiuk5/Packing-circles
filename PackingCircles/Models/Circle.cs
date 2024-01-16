using System;
using System.Numerics;

namespace PackingCircles.Models;

public class Circle
{
    public int Radius { get; }
    public Vector2 Position { get; }

    public Circle(int radius, float x, float y)
    {
        Radius = radius;
        Position = new Vector2(x,y);
    }
    public Circle(){}
    
    public bool IsIntersects(Circle circle)
    {
        double distance = Vector2.Distance(Position, circle.Position);
        return (circle.Radius + Radius - distance) > 0.0001;
    }
    
    public static bool operator ==(Circle c1, Circle c2)
    {
        return (c1.Radius == c2.Radius) && (Math.Abs(c1.Position.X - c2.Position.X) < 0.001) &&
               (Math.Abs(c1.Position.Y - c2.Position.Y) < 0.001);
    }
    
    public static bool operator !=(Circle c1, Circle c2)
    {
        return (c1.Radius != c2.Radius) || (Math.Abs(c1.Position.X - c2.Position.X) > 0.001) ||
               (Math.Abs(c1.Position.Y - c2.Position.Y) > 0.001);
    }

    public float DistanceToFurthestPoint()
    {
        return Position.Length() + Radius;
    }
}