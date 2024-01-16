using PackingCircles.Models;

namespace Tests;

public class SolutionGeneratorTests
{
    private SolutionGenerator _generator;


    [SetUp]
    public void SetUp()
    {
        _generator = new SolutionGenerator(new List<int>() { 10, 25, 20, 15 });
    }

    [Test]
    public void Generate_CirclesDoNotIntersect()
    {
        Solution solution = _generator.Generate();
        for (int i = 0; i < 3; i++)
        {
            for (int j = i+1; j < 4; j++)
            {
                Assert.False(solution.Circles[i].IsIntersects(solution.Circles[j]));
            }
        }
    }
}