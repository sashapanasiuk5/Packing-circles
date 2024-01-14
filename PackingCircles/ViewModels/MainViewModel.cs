using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PackingCircles.Models;
using PackingCircles.MVVM;

namespace PackingCircles.ViewModels;

public class MainViewModel:ViewModelBase
{
    private string _input;
    private Solution _solution;

    public string Input
    {
        get => _input;
        set => Set(ref _input, value);
    }

    public Solution Solution
    {
        get => _solution;
        set => Set(ref _solution, value);
    }
    public Command PlaceCirclesCommand { get; }
    public Command MoveCommand { get; }

    public MainViewModel()
    {
        Solution = new Solution();
        PlaceCirclesCommand = new Command(PlaceCircles, false);
        MoveCommand = new Command(MoveCircle, false);
    }

    private void PlaceCircles(object param)
    {
        SolutionGenerator generator = new SolutionGenerator();
        if (_input.Length > 0)
        {
            List<string> radiiString = new List<string>(_input.Split("\n"));
            List<int> radii = new List<int>();
            radiiString.ForEach((el) => radii.Add(Int32.Parse(el)));
            Solution = generator.Generate(radii);
        }
    }

    private void MoveCircle(object param)
    {
        CircleAlgorithms algorithms = new CircleAlgorithms();

        //Circles = new ObservableCollection<Circle>(algorithms.MoveFarrest(new List<Circle>(_circles), 10));
    }
}