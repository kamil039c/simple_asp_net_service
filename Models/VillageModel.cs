using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace service_kamil.Models
{
    [Serializable()]
    public class VillageModel
    {
        [System.Xml.Serialization.XmlElement("id")]
        public int Id { get; set; }

        [System.Xml.Serialization.XmlElement("uid")]
        public int Uid { get; set; }

        [System.Xml.Serialization.XmlElement("x")]
        public int X { get; set; }

        [System.Xml.Serialization.XmlElement("y")]
        public int Y { get; set; }

        [System.Xml.Serialization.XmlElement("name")]
        public string Name { get; set; }

        [System.Xml.Serialization.XmlElement("points")]
        public int Points { get; set; }

        [System.Xml.Serialization.XmlElement("res_1")]
        public double ResStone { get; set; }

        [System.Xml.Serialization.XmlElement("res_2")]
        public double ResWood { get; set; }

        [System.Xml.Serialization.XmlElement("res_3")]
        public double ResIron { get; set; }
    }
}
