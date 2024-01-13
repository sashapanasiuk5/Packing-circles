using System;
using System.Windows.Input;

namespace PackingCircles.MVVM;

public class Command
{
    private Action<object> _execute;
    private Func<object, bool> _canExecute;
    private bool _isParameterNecessery;

    public Command(Action<object> executeFunction, bool isParameterNecessery = true,Func<object, bool> canExecuteFunction = null)
    {
        _execute = executeFunction;
        _canExecute = canExecuteFunction;
        _isParameterNecessery = isParameterNecessery;
    }
    
    public void Execute(object? parameter) => _execute(parameter);

    public bool CanExecute(object? parameter)
    {
        if ( (parameter == null) && _isParameterNecessery)
        {
            return false;
        }
        return _canExecute?.Invoke(parameter) ?? true;
        
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}