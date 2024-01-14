using PackingCircles.Models;

namespace Tests;

public class RandomValueTests
{
    private RandomValue _randomValue;
    [SetUp]
    public void Setup()
    {
        Solution solution = new Solution(new List<Circle>()
        {
            new Circle(25, 0,0),
            new Circle(15, 0, 40),
            new Circle(5, 30,0)
        });
        _randomValue = new RandomValue(solution);
    }

    [Test]
    public void CheckProbabilities()
    {
        double firstProbability = (double)25 / 115;
        double secondProbability = (double)55 / 115;
        double thirdProbability = (double)35 / 115;
        double epsilo = 0.000001;
        Assert.Less( _randomValue.Probabilities[0] - firstProbability, epsilo);
        Assert.Less( _randomValue.Probabilities[1] - secondProbability, epsilo);
        Assert.Less( _randomValue.Probabilities[2] - thirdProbability, epsilo);
    }
}