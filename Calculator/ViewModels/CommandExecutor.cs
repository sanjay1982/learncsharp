using System;
using System.ComponentModel;
using System.Windows.Input;
using Calculator.BLL;

namespace Calculator.ViewModels
{
    public class CommandExecutor : ICommand, INotifyPropertyChanged
    {
        private bool _enabled = true;
        private readonly ExpressionBuilder _expressionBuilder = new ExpressionBuilder();

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

        public string Expression => _expressionBuilder.ExpressionString;

        public string Value => _expressionBuilder.Value;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return Enabled;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is Command command)) return;
            if (_expressionBuilder.Accept(command))
            {
                OnPropertyChanged(nameof(Expression));
                OnPropertyChanged(nameof(Value));
            }
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