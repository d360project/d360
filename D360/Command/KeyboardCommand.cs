using D360.InputEmulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D360
{
    public class KeyboardCommand : Command
    {
        public Keys? key { get; set; }



        //public Boolean repeat = false;

        public ButtonState commandState = ButtonState.Down;




        public KeyboardCommand()
        {
            //
        }

        public override bool Execute(ref ControllerState state)
        {
            if (base.Execute(ref state))
            {
                #region Keys
                if (key.HasValue)
                {

                    if (commandState == ButtonState.Down)
                    {
                        VirtualKeyboard.KeyDown(key.Value);
                    }
                    else
                    {
                        VirtualKeyboard.KeyUp(key.Value);
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
