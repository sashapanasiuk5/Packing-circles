using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PackingCircles.Models;
using PackingCircles.MVVM;

namespace PackingCircles.ViewModels;

public class MainViewModel:ViewModelBase
{
    private string _input;
    private ObservableCollection<Circle> _circles;

    public string Input
    {
        get => _input;
        set => Set(ref _input, value);
    }

    public ObservableCollection<Circle> Circles
    {
        get => _circles;
        set => Set(ref _circles, value);
    }
    public Command PlaceCirclesCommand { get; }
    public Command MoveCommand { get; }

    public MainViewModel()
    {
        Circles = new ObservableCollection<Circle>();
        PlaceCirclesCommand = new Command(PlaceCircles, false);
        MoveCommand = new Command(MoveCircle, false);
    }

    private void PlaceCircles(object param)
    {
        CircleGenerator generator = new CircleGenerator();
        if (_input.Length > 0)
        {
            List<string> radiiString = new List<string>(_input.Split("\n"));
            List<int> radii = new List<int>();
            radiiString.ForEach((el) => radii.Add(Int32.Parse(el)));
            Circles = new ObservableCollection<Circle>(generator.Generate(radii));
        }
    }

    private void MoveCircle(object param)
    {
        CircleAlgorithms algorithms = new CircleAlgorithms();

        //Circles = new ObservableCollection<Circle>(algorithms.MoveFarrest(new List<Circle>(_circles), 10));
    }
}