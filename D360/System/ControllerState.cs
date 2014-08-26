using D360.Types;
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
        public bool connected;

        public UIntVector targetingReticulePosition;
        public UIntVector cursorPosition;

        public InputMode inputMode;
        public UIntVector centerPosition;
    }
}
