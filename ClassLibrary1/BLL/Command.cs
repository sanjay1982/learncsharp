using System.Xml.Serialization;

namespace CalculatorLib.BLL
{
    public class Command
    {
        [XmlAttribute] public string Text { get; set; }

        [XmlAttribute] public CommandType Type { get; set; }

        [XmlAttribute(AttributeName = "Key")] public string KeyBoardKey { get; set; }

        [XmlAttribute] public string Value { get; set; }

        [XmlAttribute] public int ArgumentCount { get; set; }
    }
}