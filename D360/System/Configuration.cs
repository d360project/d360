using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace D360
{
    public class Configuration
    {
       [XmlElement("leftTriggerBinding")]
        public String leftTriggerBinding;

        [XmlElement("rightTriggerBinding")]
       public String rightTriggerBinding;

        public Configuration()
        {
            leftTriggerBinding = "actionBarSkill1Key";
            rightTriggerBinding = "actionBarSkill2Key";
        }
    }
}
