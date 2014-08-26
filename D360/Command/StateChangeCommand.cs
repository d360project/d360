using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D360
{
    public class StateChangeCommand : Command
    {
        public StateChange stateChange { get; set; }


        public override bool Execute(ref ControllerState state)
        {
            if (base.Execute(ref state))
            {

                #region State Changes
                if (stateChange != null)
                {
                    if (stateChange.toggle)
                    {
                        if (state.inputMode == InputMode.Move)
                        {
                            state.inputMode = InputMode.Pointer;
                        }
                        else if (state.inputMode == InputMode.Pointer)
                        {
                            state.inputMode = InputMode.Move;
                        }
                    }
                    else if (stateChange.newMode != InputMode.None)
                    {
                        state.inputMode = stateChange.newMode;
                    }

                }
                #endregion

                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
