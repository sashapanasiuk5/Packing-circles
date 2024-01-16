using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows;
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
        Circle newCircle = new Circle(target.Radius, x, y);
        circles.Add(newCircle);
        HashSet<int> movedIndexes = new HashSet<int>();
        movedIndexes.Add(circles.Count-1);

        int k = 0;
        while (movedIndexes.Count > 0)
        {
            HashSet<int> newMovedCircles = new HashSet<int>();
            foreach (var index in movedIndexes)
            {
                for (int i = 0; i < circles.Count; i++)
                {
                    Circle circle = circles[i];
                    if(i == index)
                        continue;
                    if (circle.IsIntersects(circles[index]))
                    {
                        Circle movedCircle = circles[index];
                        float d = (circle.Radius + movedCircle.Radius) - Vector2.Distance(circle.Position, movedCircle.Position);
                        Vector2 vector = Vector2.Subtract(circle.Position, movedCircle.Position);
                        vector = Vector2.Normalize(vector);
                        vector = Vector2.Multiply(d, vector);
                        circles[i] = new Circle(circle.Radius, circle.Position.X + vector.X, circle.Position.Y + vector.Y);
                        newMovedCircles.Add(i);
                        if (circles[i].Position.Length() > 500)
                        {
                           break;
                        }
                    }
                }
            }
            movedIndexes = newMovedCircles;
            k++;
        }

        return circles;
    }
}