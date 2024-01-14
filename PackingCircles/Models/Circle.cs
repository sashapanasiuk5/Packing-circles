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
        double distance = Math.Sqrt(Math.Pow((circle.Position.X - Position.X), 2) + Math.Pow((circle.Position.Y - Position.Y), 2));
        return distance < circle.Radius + Radius;
    }

    public float DistanceToFurthestPoint()
    {
        return Position.Length() + Radius;
    }
}