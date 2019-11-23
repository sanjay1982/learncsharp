using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    public class Command
    {
        public string Text { get; set; }
        public CommandType Type { get; set; }
        public Key KeyBoardKey { get; set; }
        public string Value { get; set; }
    }

    public enum CommandType
    {
        Literal,
        UnaryOperator,
        BinaryOperator
    }
}
