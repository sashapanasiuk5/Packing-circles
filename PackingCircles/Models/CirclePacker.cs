using System.Collections.Generic;
using System.Windows.Documents;

namespace PackingCircles.Models;

public class CirclePacker
{
    private List<Solution> _plots;
    private Solution _bestSolution;

    private int _numberOfOnlookers;
    private int _numberOdScouts;
    private int _numberOfPlots;

    private const int IterationStopCount = 100;

    public CirclePacker(int numberOfPlots, List<int> radii, int numberOfOnlookers, int numberOfScouts)
    {
        _numberOfOnlookers = numberOfOnlookers;
        _numberOdScouts = numberOfScouts;
        _numberOfPlots = numberOfPlots;
        SolutionGenerator generator = new SolutionGenerator();
        for (int i = 0; i < numberOfPlots; i++)
        {
            _plots.Add(generator.Generate(radii));
        }
    }


    public void Solve()
    {
        int iterations = 0;
        do
        {
            Solution plot = ChoosePlot();
            iterations++;
        } while (iterations < IterationStopCount);
    }

    private Solution ChoosePlot()
    {
        return new Solution();
    }
}