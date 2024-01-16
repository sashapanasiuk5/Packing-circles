using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PackingCircles.Models;
using PackingCircles.MVVM;

namespace PackingCircles.ViewModels;

public class MainViewModel:ViewModelBase
{
    private string _input;
    private Solution _solution;
    private CirclePacker _packer;

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
    public Command SolveCommand { get; }

    public MainViewModel()
    {
        Solution = new Solution();
        SolveCommand = new Command(Solve, false);
    }
    private void Solve(object param)
    {
        if (_input.Length > 0)
        {
            List<string> radiiString = new List<string>(_input.Split(
                new string[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            ));
            List<int> radii = new List<int>();
            radiiString.ForEach((el) => radii.Add(Int32.Parse(el)));
            _packer = new CirclePacker(10, radii, 10, 5);
            _packer.Solve();
            Solution = _packer.GetSolution();
        }
    }
}