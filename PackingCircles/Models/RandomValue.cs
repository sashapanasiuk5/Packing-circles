using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace PackingCircles.Models;

public class RandomValue
{
    public List<double> Probabilities { get; private set; }


    public RandomValue(List<double> probabilities)
    {
        Probabilities = probabilities;
    }

    public RandomValue(Solution solution)
    {
        Probabilities = new List<double>();
        float sum = 0;
        foreach (var circle in solution.Circles)
        {
            sum += circle.DistanceToFurthestPoint();
        }

        foreach (var circle in solution.Circles)
        {
            Probabilities.Add(circle.DistanceToFurthestPoint() / sum);
        }
    }
    public int GetRandomValue()
    {
        List<double> distributionFunction = new List<double>();
        distributionFunction.Add(0);
        double previousValue = 0;
        foreach (var p in Probabilities)
        {
            double value = previousValue + p;
            distributionFunction.Add(value);
            previousValue = value;
        }

        if (Math.Abs(distributionFunction.Last() - 1.0) > 0.0000001)
        {
            throw new Exception("Probabilities are incorrect");
        }

        Random random = new Random();
        double randomDouble = random.NextDouble();
        for (int i = 0; i < distributionFunction.Count-1; i++)
        {
            if( (distributionFunction[i] < randomDouble) && ( randomDouble <= distributionFunction[i+1]))
                return i;
        }

        throw new Exception("Random generating works incorrect");
    }
}