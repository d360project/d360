using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D360.Bindings
{
    public class ControllerStickBinding
    {
        public ControllerStick side;
        public Vector2 position;


        public ControllerStickBinding(ControllerStick leftOrRight, Vector2 v)
        {
            side = leftOrRight;
            position = v;
        }
    }
}
