using D360.InputEmulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D360
{
    public class MouseButtonCommand : Command
    {
        public MouseButtons mouseButton { get; set; }

        public ButtonState commandState = ButtonState.Down;

        public override bool Execute(ref ControllerState state)
        {
            if (base.Execute(ref state))
            {

                #region Mouse Buttons
                if (commandState == ButtonState.Down)
                {
                    if (mouseButton == MouseButtons.Left)
                    {
                        VirtualMouse.LeftDown();
                    }
                    else if (mouseButton == MouseButtons.Right)
                    {
                        VirtualMouse.RightDown();
                    }
                }
                else
                {
                    if (mouseButton == MouseButtons.Left)
                    {
                        VirtualMouse.LeftUp();
                    }
                    else if (mouseButton == MouseButtons.Right)
                    {
                        VirtualMouse.RightUp();
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
