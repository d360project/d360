using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D360
{
    public class ControllerStickBinding
    {
        public ControllerStick side;
        public Vector2 position;
        public StickState newState;
        public StickState oldState;

        public ControllerStickBinding(ControllerStick leftOrRight, Vector2 v, StickState newS, StickState oldS = StickState.Any)
        {
            side = leftOrRight;
            position = v;
            newState = newS;
            oldState = oldS;
        }
    }
}
