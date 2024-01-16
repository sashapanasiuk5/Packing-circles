using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PackingCircles.Models;

public class Solution
{
    public ObservableCollection<Circle> Circles { get; }
    public int Mutations { get;  set; }
    public float Estimate { get; private set; }

    public Solution(List<Circle> circles)
    {

        Circles = new ObservableCollection<Circle>(circles);
        Circles.CollectionChanged += CalculateEstimate;
        CalculateEstimate(this, null);
    }

    public Solution()
    {
        Circles = new ObservableCollection<Circle>();
        Circles.CollectionChanged += CalculateEstimate;
    }

    public void AddCircle(Circle circle) => Circles.Add(circle);

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

    public void IncreaseMutations() => Mutations++;

    public bool CanAdd(Circle circleToAdd)
    {
        foreach (var circle in Circles)
        {
            if (circleToAdd.IsIntersects(circle))
                return false;
        }
        return true;
    }
}