using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo3Controller
{
    public class InputBinding
    {
        public Buttons? button { get; set; }
        public GamePadTriggers? trigger { get; set; }

        D3Command command;

    }
}
