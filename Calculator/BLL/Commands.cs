﻿using System.Collections.Generic;
using System.Xml.Serialization;

namespace Calculator.BLL
{
    [XmlRoot(ElementName = "Commands")]
    public class Commands
    {
        [XmlElement(ElementName = "Command")] public List<Command> CommandList { get; set; }
    }
}