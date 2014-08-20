using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo3Controller
{
    public class ControllerInputBinding
    {
        public Buttons? button { get; set; }
        public GamePadTriggers? trigger { get; set; }

        public D3Command command { get; set; }

        public ControllerInputBinding()
        {
            //
        }

    }
}
