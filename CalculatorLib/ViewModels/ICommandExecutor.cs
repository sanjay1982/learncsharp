using System.Windows.Input;

namespace CalculatorLib.ViewModels
{
    public interface ICommandExecutor : ICommand
    {
        string Expression { get; }
        string Value { get; }
    }
}