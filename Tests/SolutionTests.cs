using PackingCircles.Models;

namespace Tests;

public class Tests
{
    private Solution _solution;
    [SetUp]
    public void Setup()
    {
        _solution = new Solution(new List<Circle>()
        {
            new Circle(25, 0,0),
            new Circle(15, 0, 40),
            new Circle(5, 30,0)
        });
    }

    [Test]
    public void Estimate_55_Expected()
    {
        Assert.AreEqual(55, _solution.Estimate);
    }
}