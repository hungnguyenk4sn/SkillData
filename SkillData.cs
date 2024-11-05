using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
    public class SkillData
    {
        public int ID { get;  set; }
        public string Name { get;  set; }
        public string Icon { get;  set; }
        public static SkillData Parse(XElement xmlNode)
        {
            SkillData skillData = new SkillData()
            {
                ID = int.Parse(xmlNode.Attribute("ID").Value),
                Name = xmlNode.Attribute("Name").Value,
                Icon = xmlNode.Attribute("Icon").Value
            };
            return skillData;
        }
    }
}
