using System;
using System.Collections.Generic;
using System.Numerics;
using PackingCircles.ViewModels;

namespace PackingCircles.Models;

public class CircleAlgorithms
{
    public List<Circle> MoveWithCollision(List<Circle> circles, Circle target, float step)
    {
        circles.Remove(target);
        float oldDistance = target.Position.Length();
        float newDistance = oldDistance - step;
        float x = (target.Position.X * newDistance)/oldDistance;
        float y = (target.Position.Y * newDistance)/oldDistance;
        circles.Add(new Circle(target.Radius, x, y));
        return MoveOthers(circles);
    }
    
    private List<Circle> MoveOthers(List<Circle> circles)
    {
        for (int i = 0; i < circles.Count; i++)
        {
            for (int j = 0; j < circles.Count; j++)
            {
                if(i == j)
                    continue;
                Circle circle = circles[i];
                Circle moved = circles[j];
                if (circle.IsIntersects(moved))
                {
                    float d = (circle.Radius + moved.Radius) - Vector2.Distance(circle.Position, moved.Position);
                    Vector2 vector = Vector2.Subtract(circle.Position, moved.Position);
                    vector = Vector2.Normalize(vector);
                    vector = Vector2.Multiply(d, vector);
                    circles[i] = new Circle(circle.Radius, circle.Position.X + vector.X, circle.Position.Y + vector.Y);
                }
            }
        }
        return circles;
    }
}