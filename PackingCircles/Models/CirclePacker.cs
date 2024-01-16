using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace PackingCircles.Models;

public class CirclePacker
{
    private List<Solution> _plots;
    private Solution _bestSolution;

    private int _numberOfOnlookers;
    private int _numberOfScouts;
    private int _numberOfPlots;
    
    private SolutionGenerator _solutionGenerator;

    private const int IterationStopCount = 4000;

    public CirclePacker(int numberOfPlots, List<int> radii, int numberOfOnlookers, int numberOfScouts)
    {
        _numberOfOnlookers = numberOfOnlookers;
        _numberOfScouts = numberOfScouts;
        _numberOfPlots = numberOfPlots;
        _solutionGenerator = new SolutionGenerator(radii);
        _plots = new List<Solution>();
        for (int i = 0; i < numberOfPlots; i++)
        {
            _plots.Add(_solutionGenerator.Generate());
        }

        _bestSolution = _plots[0];
    }


    public void Solve()
    {
        int iterations = 0;
        do
        {
            int plotIndex = ChoosePlot();
            RandomValue randomValue = new RandomValue(_plots[plotIndex]);
            for (int i = 0; i < _numberOfOnlookers; i++)
            {
                int circleIndex = randomValue.GetRandomValue();
                (Solution plot, bool result) = TryImproveSolution(_plots[plotIndex], _plots[plotIndex].Circles[circleIndex]);
                if (result)
                {
                    _plots[plotIndex] = plot;
                }
                else
                {
                    _plots[plotIndex].IncreaseMutations();
                }
                
            }
            SearchNewPlots();
            UpdateBestSolution();
            iterations++;
        } while (iterations < IterationStopCount);
    }

    public Solution GetSolution() => _bestSolution;

    private int ChoosePlot()
    {
        List<(int, double)> candidates = new List<(int, double)>();
        List<Solution> plots = new List<Solution>(_plots);
        Random random = new Random();
        for (int i = 0; i < _numberOfScouts; i++)
        {
            int index = random.Next(0, plots.Count-1);
            candidates.Add((index, plots[index].Estimate));
            plots.RemoveAt(index);
        }

        return candidates.MinBy(t => t.Item2).Item1;
    }

    private void SearchNewPlots()
    {
        List<int> plotIndexesToDelete = new List<int>();
        for (int i = 0; i < _numberOfPlots; i++)
        {
            if (_plots[i].Mutations > 200)
            {
                plotIndexesToDelete.Add(i);
            }
        }

        foreach (var plotIndex in plotIndexesToDelete)
        {
            _plots.RemoveAt(plotIndex);
            _plots.Add(_solutionGenerator.Generate());
        }
    }
    

    private void UpdateBestSolution()
    {
        Solution bestInPlots = _plots.MinBy(plot => plot.Estimate);
        if (bestInPlots.Estimate < _bestSolution.Estimate)
        {
            _bestSolution = bestInPlots;
        }
    }

    private (Solution solution,bool result) TryImproveSolution(Solution solution, Circle circle)
    {
        CircleAlgorithms algorithms = new CircleAlgorithms();
        double oldEstimate = solution.Estimate;
        Solution newSolution = new Solution(algorithms.MoveWithCollision(new List<Circle>(solution.Circles), circle, 10));
        if (newSolution.Estimate < oldEstimate)
            return (newSolution, true);
         newSolution = new Solution(algorithms.MoveWithCollision(new List<Circle>(solution.Circles), circle, 5));
        if (newSolution.Estimate < oldEstimate)
            return (newSolution, true);
        
        newSolution = new Solution(algorithms.MoveWithCollision(new List<Circle>(solution.Circles), circle, 3));
        if (newSolution.Estimate < oldEstimate)
            return (newSolution, true);
        
        newSolution = new Solution(algorithms.MoveWithCollision(new List<Circle>(solution.Circles), circle, 1));
        if (newSolution.Estimate < oldEstimate)
            return (newSolution, true);

        return (solution, false);
    }
}