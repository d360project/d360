using D360.InputEmulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D360
{
    public class Command
    {
        public InputMode applicableMode = InputMode.None;
        public CommandTarget target = CommandTarget.Cursor;

        public InputMode inputMode;

        public virtual bool Execute(ref ControllerState state)
        {
            if (applicableMode == InputMode.None)
            {
                return false;
            }

            if ((applicableMode != state.inputMode) && (applicableMode != InputMode.All))
            {
                return false;
            }

            /*
            if (target == CommandTarget.Cursor)
            {
                VirtualMouse.MoveAbsolute(state.cursorPosition.X, state.cursorPosition.Y);
            }
            else if (target == CommandTarget.TargetReticule)
            {
                if ((state.targetingReticulePosition.X == state.centerPosition.X) && (state.targetingReticulePosition.Y == state.centerPosition.Y))
                {
                    VirtualMouse.MoveAbsolute(state.cursorPosition.X, state.cursorPosition.Y);
                }
                else
                {
                    VirtualMouse.MoveAbsolute(state.targetingReticulePosition.X, state.targetingReticulePosition.Y);
                }
            }
            else if (target == CommandTarget.None)
            {
                //
            }
            */
            return true;
        }
    }
}
