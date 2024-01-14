using System;
using System.Collections.Generic;
using System.Linq;

namespace PackingCircles.Models;

public class SolutionGenerator
{
    public Solution Generate(List<int> radii)
    {
        Random random = new Random();
        Solution solution = new Solution();
        int MaxRadius = radii.Max();
        solution.AddCircle(new Circle(MaxRadius, 0,0));
        radii.Remove(MaxRadius);
        foreach (var radius in radii)
        {
            Circle newCircle = new Circle();
            do
            {
                Circle target = solution.Circles[random.Next(0, solution.Circles.Count - 1)];
                double angle = random.Next(0, 360)*Math.PI/180;
                float x = (float)((target.Radius + radius) * Math.Cos(angle) + target.Position.X);
                float y = (float)((target.Radius + radius) * Math.Sin(angle) + target.Position.Y);
                newCircle = new Circle(radius, x, y);
            } while (solution.CanAdd(newCircle));
            solution.AddCircle(newCircle);
        }
        return solution;
    }
}