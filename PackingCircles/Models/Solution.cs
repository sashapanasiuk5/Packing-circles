using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PackingCircles.Models;

public class Solution
{
    public ObservableCollection<Circle> Circles { get; }
    public float Estimate { get; private set; }

    public Solution(List<Circle> circles)
    {
        Circles = new ObservableCollection<Circle>(circles);
        Circles.CollectionChanged += CalculateEstimate;
    }

    private void CalculateEstimate(object sender, EventArgs e)
    {
        float maxDistance = 0;
        foreach (var circle in Circles)
        {
            float distance = circle.DistanceToFurthestPoint();
            if (distance > maxDistance)
                maxDistance = distance;
        }

        Estimate = maxDistance;
    }
}