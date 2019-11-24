using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Calculator.ViewModels
{
    public class Command
    {
        [XmlAttribute]
        public string Text { get; set; }
        [XmlAttribute]
        public CommandType Type { get; set; }
        [XmlAttribute(AttributeName = "Key")]
        public Key KeyBoardKey { get; set; }
        [XmlAttribute]
        public string Value { get; set; }

    }

    public class CommandExecutor : ICommand, INotifyPropertyChanged
    {
        private bool _enabled = true;
        private string _expression;
        private string _value;

        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                if (value == _enabled)
                {
                    return;
                }
                _enabled = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public string Expression
        {
            get
            {
                return _expression;
            }
            set
            {
                _expression = value;
                OnPropertyChanged(nameof(Expression));
            }
        }
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        public bool CanExecute(object parameter)
        {
            return Enabled;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is Command command))
            {
                return;
            }
            Expression += command.Value;
        }
        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
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
