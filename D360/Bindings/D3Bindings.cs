
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
        public Keys forceStandStillKey = Keys.LShiftKey;
        public Keys forceMoveKey = Keys.Space;
        public Keys actionBarSkill1Key = Keys.D1;
        public Keys actionBarSkill2Key = Keys.D2;
        public Keys actionBarSkill3Key = Keys.D3;
        public Keys actionBarSkill4Key = Keys.D4;
        public Keys potionKey = Keys.Q;
        public Keys townPortalKey = Keys.T;
        public Keys gameMenuKey = Keys.Escape;

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

        }

    }
}
