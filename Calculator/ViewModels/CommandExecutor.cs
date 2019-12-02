using System;
using System.ComponentModel;
using System.Windows.Input;
using Calculator.BLL;

namespace Calculator.ViewModels
{
    public class CommandExecutor : ICommand, INotifyPropertyChanged
    {
        private ICommandAcceptor _commandAcceptor;
        private readonly ICommandAcceptorFactory _factory = new CommandAcceptorFactory();
        private bool _enabled = true;

        public CommandExecutor()
        {
            _commandAcceptor = _factory.CreateLiteral();
        }

        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (value == _enabled) return;
                _enabled = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public string Expression => _commandAcceptor.ExpressionString;

        public string Value => _commandAcceptor.Value.ToString();

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (!(parameter is Command command)) return true;
            return _commandAcceptor?.CanAccept(parameter as Command) ?? false;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is Command command)) return;
            _commandAcceptor = _commandAcceptor?.Accept(parameter as Command);
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            OnPropertyChanged(nameof(Expression));
            OnPropertyChanged(nameof(Value));
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}