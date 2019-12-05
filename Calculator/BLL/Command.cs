using System.Windows.Input;
using System.Xml.Serialization;

namespace Calculator.BLL
{
    public class Command
    {
        [XmlAttribute] public string Text { get; set; }

        [XmlAttribute] public CommandType Type { get; set; }

        [XmlAttribute(AttributeName = "Key")] public Key KeyBoardKey { get; set; }

        [XmlAttribute] public string Value { get; set; }

        [XmlAttribute] public int ArgumentCount { get; set; } = 0;
    }
}