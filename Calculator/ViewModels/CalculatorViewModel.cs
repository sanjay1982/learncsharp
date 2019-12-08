using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Calculator.BLL;

namespace Calculator.ViewModels
{
    public class CalculatorViewModel
    {
        private Commands _commands;

        public CalculatorViewModel()
        {
            LoadCommands();
        }

        public List<Command> Commands
        {
            get => _commands.CommandList;
            set => _commands.CommandList = value;
        }

        public CommandExecutor CommandExecutor { get; } = new CommandExecutor();

        private void LoadCommands()
        {
            const string path = @"ViewModels\SimpleCalculator.xml";
            if (File.Exists(path))
            {
                LoadCommandsFromStream(File.OpenRead(path));
                return;
            }

            var assembly = typeof(CalculatorViewModel).Assembly;
            const string resourceName = "Calculator.ViewModels.SimpleCalculator.xml";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                LoadCommandsFromStream(stream);
            }

            void LoadCommandsFromStream(Stream stream)
            {
                var serializer = new XmlSerializer(typeof(Commands));

                using (var reader = new StreamReader(stream))
                {
                    _commands = (Commands)serializer.Deserialize(reader);
                }
            }
        }
    }
}