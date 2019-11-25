using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Calculator.ViewModels
{
    [XmlRoot(ElementName = "Commands")]
    public class Commands
    {
        [XmlElement(ElementName = "Command")] public List<Command> CommandList { get; set; }
    }

    public class FunctionInfo
    {
        public MethodInfo MethodInfo
        {
            get;
            set;
        }
        public object Context { get; set; }
        public string Name { get; set; }
        public int ArgumentCount => MethodInfo?.GetParameters().Length ?? 0;

        public object Invoke(IEnumerable<object> arguments)
        {
            return MethodInfo?.Invoke(Context, arguments.ToArray());
        }

    }
    public class Command
    {
        [XmlAttribute] public string Text { get; set; }

        [XmlAttribute] public CommandType Type { get; set; }

        [XmlAttribute(AttributeName = "Key")] public Key KeyBoardKey { get; set; }

        [XmlAttribute] public string Value { get; set; }

        [XmlAttribute] public string FunctionName { get; set; }
    }

    public class CommandExecutor : ICommand, INotifyPropertyChanged
    {
        private bool _enabled = true;
        private string _expression;
        private string _value;

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

        public string Expression
        {
            get => _expression;
            set
            {
                _expression = value;
                OnPropertyChanged(nameof(Expression));
            }
        }

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return Enabled;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is Command command)) return;
            Expression += command.Value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public enum CommandType
    {
        Literal,
        UnaryOperator,
        BinaryOperator,
        Calculate
    }
}