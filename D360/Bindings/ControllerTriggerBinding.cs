using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D360
{
    public class ControllerTriggerBinding
    {
        public ControllerTrigger side;
        public float position;


        public ControllerTriggerBinding(ControllerTrigger leftOrRight, float v)
        {
            side = leftOrRight;
            position = v;
        }
    }
}
