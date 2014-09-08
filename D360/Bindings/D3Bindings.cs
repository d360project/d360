
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace D360
{
    public class D3Bindings
    {
        [XmlElement("inventoryKey")]
        public Keys inventoryKey = Keys.I;

        [XmlElement("mapKey")]
        public Keys mapKey = Keys.Tab;

        [XmlElement("forceStandStillKey")]
        public Keys forceStandStillKey = Keys.LShiftKey;

        [XmlElement("forceMoveKey")]
        public Keys forceMoveKey = Keys.Space;

        [XmlElement("actionBarSkill1Key")]
        public Keys actionBarSkill1Key = Keys.D1;

        [XmlElement("actionBarSkill2Key")]
        public Keys actionBarSkill2Key = Keys.D2;

        [XmlElement("actionBarSkill3Key")]
        public Keys actionBarSkill3Key = Keys.D3;

        [XmlElement("actionBarSkill4Key")]
        public Keys actionBarSkill4Key = Keys.D4;

        [XmlElement("potionKey")]
        public Keys potionKey = Keys.Q;

        [XmlElement("townPortalKey")]
        public Keys townPortalKey = Keys.T;

        [XmlElement("gameMenuKey")]
        public Keys gameMenuKey = Keys.Escape;

        [XmlElement("worldMapKey")]
        public Keys worldMapKey = Keys.M;


        public D3Bindings()
        {
            inventoryKey = Keys.I;
            mapKey = Keys.Tab;
            forceStandStillKey = Keys.LShiftKey;
            forceMoveKey = Keys.Space;
            actionBarSkill1Key = Keys.D1;
            actionBarSkill2Key = Keys.D2;
            actionBarSkill3Key = Keys.D3;
            actionBarSkill4Key = Keys.D4;
            potionKey = Keys.Q;
            townPortalKey = Keys.T;
            gameMenuKey = Keys.Escape;
            worldMapKey = Keys.M;

        }


        internal Keys fromString(string p)
        {
            switch (p)
            {
                case "actionBarSkill1Key": return actionBarSkill1Key;
                case "actionBarSkill2Key": return actionBarSkill2Key;
                case "actionBarSkill3Key": return actionBarSkill3Key;
                case "actionBarSkill4Key": return actionBarSkill4Key;
                case "inventoryKey": return inventoryKey; 
                case "mapKey": return mapKey; 
                case "potionKey": return potionKey;
                case "townPortalKey": return townPortalKey;
                default: return actionBarSkill1Key; 
            }
        }
    }
}
