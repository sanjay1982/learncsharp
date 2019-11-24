using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Calculator.ViewModels
{
    [XmlRoot(ElementName ="Commands")]
    public class Commands
    {

        [XmlElement(ElementName ="Command")]
        public List<Command> CommandList { get; set; }
    }
    public class CalculatorViewModel
    {
        public  CalculatorViewModel(){
            string path = "ViewModels\\SimpleCalculator.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(Commands));

            StreamReader reader = new StreamReader(path);
            _commands = (Commands)serializer.Deserialize(reader);
        }
        private Commands _commands = new Commands();
        public List<Command> Commands { get => _commands.CommandList; set => _commands.CommandList = value; }
        public CommandExecutor CommandExecutor { get; } = new CommandExecutor();
    }
}
