using System.Windows.Input;

namespace Mini_Postman.ViewModels;

public class AsyncRelayCommand : ICommand
{
    private readonly Func<object,Task> _execute;
    private readonly Predicate<object?>? _canExecute;

    public AsyncRelayCommand(Func<object,Task> execute, Predicate<object?>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }        
    
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    public async void Execute(object? parameter)
    {
        try
        {
            await _execute(parameter);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while executing command: " + e.Message);
        }
    }
    
}