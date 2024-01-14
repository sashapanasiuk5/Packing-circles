using System;
using System.Collections.Generic;
using System.Linq;

namespace PackingCircles.Models;

public class CircleGenerator
{
    public List<Circle> Generate(List<int> radii)
    {
        Random random = new Random();
        List<Circle> circles = new List<Circle>();
        int MaxRadius = radii.Max();
        circles.Add(new Circle(MaxRadius, 0,0));
        radii.Remove(MaxRadius);
        foreach (var radius in radii)
        {
            Circle newCircle = new Circle();
            do
            {
                Circle target = circles[random.Next(0, circles.Count - 1)];
                double angle = random.Next(0, 360)*Math.PI/180;
                float x = (float)((target.Radius + radius) * Math.Cos(angle) + target.Position.X);
                float y = (float)((target.Radius + radius) * Math.Sin(angle) + target.Position.Y);
                newCircle = new Circle(radius, x, y);
            } while (IsIntersectsInList(circles, newCircle));
           
            circles.Add(newCircle);
        }

        return circles;
    }
    
    private bool IsIntersectsInList(List<Circle> circles, Circle target)
    {
        foreach (var circle in circles)
        {
            if (target.IsIntersects(circle))
                return true;
        }
        return false;
    }
}