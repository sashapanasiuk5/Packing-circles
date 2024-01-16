using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace PackingCircles.Models;

public class SolutionGenerator
{
    private List<int> _radii;

    public SolutionGenerator(List<int> radii)
    {
        _radii = radii;
    }
    public Solution Generate()
    {
        Random random = new Random();
        Solution solution = new Solution();

        List<int> tempRadii = new List<int>(_radii);
        int MaxRadius = tempRadii.Max();
        solution.AddCircle(new Circle(MaxRadius, random.Next(0, 2*MaxRadius),random.Next(0, 2*MaxRadius)));
        tempRadii.Remove(MaxRadius);
        foreach (var radius in tempRadii)
        {
            Circle newCircle = new Circle();
            do
            {
                Circle target = solution.Circles[random.Next(0, solution.Circles.Count - 1)];
                double angle = random.Next(0, 360)*Math.PI/180;
                float x = (float)((target.Radius + radius) * Math.Cos(angle) + target.Position.X);
                float y = (float)((target.Radius + radius) * Math.Sin(angle) + target.Position.Y);
                newCircle = new Circle(radius, x, y);
            } while (!solution.CanAdd(newCircle));
            solution.AddCircle(newCircle);
        }
        return solution;
    }
}