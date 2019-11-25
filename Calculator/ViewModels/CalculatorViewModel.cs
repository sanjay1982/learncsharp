using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Calculator.ViewModels
{
    public class CalculatorViewModel
    {
        private readonly Commands _commands = new Commands();

        public CalculatorViewModel()
        {
            var path = "ViewModels\\SimpleCalculator.xml";

            var serializer = new XmlSerializer(typeof(Commands));

            var reader = new StreamReader(path);
            _commands = (Commands)serializer.Deserialize(reader);
        }

        public List<Command> Commands
        {
            get => _commands.CommandList;
            set => _commands.CommandList = value;
        }

        public CommandExecutor CommandExecutor { get; } = new CommandExecutor();
    }
}