using D360.Bindings;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D360
{
    public class ControllerState
    {
        public System.Drawing.Point targetingReticulePosition;
        public System.Drawing.Point cursorPosition;

        public InputMode inputMode;
        public System.Drawing.Point centerPosition;

        public Vector2 pointCommandValue;
    }
}
