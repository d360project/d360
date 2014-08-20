using D360.InputEmulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D360.Bindings
{
    public class KeyboardMouseCommand
    {
        public Keys? key { get; set; }
        public MouseButtons? mouseButton { get; set; }
        public MouseMove mouseMove { get; set; }
        public StateChange stateChange { get; set; }
        public Boolean repeat = false;
        public CommandTarget target = CommandTarget.Cursor;
        public KeyboardMouseCommandState commandState = KeyboardMouseCommandState.Down;

        public KeyboardMouseCommand()
        {
            //
        }

        internal void Execute(ref ControllerState state)
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

            #region Mouse Movements
            if (mouseMove != null)
            {
                if (mouseMove.commandTarget == CommandTarget.Cursor)
                {
                    if (mouseMove.moveType == MouseMoveType.Absolute)
                    {

                        state.cursorPosition.X = state.centerPosition.X + (int)(state.pointCommandValue.X * mouseMove.moveScale.X);
                        state.cursorPosition.Y = state.centerPosition.Y + (int)(state.pointCommandValue.Y * mouseMove.moveScale.Y);
                    }
                }
            }

            if (target == CommandTarget.TargetReticule)
            {
                VirtualMouse.MoveAbsolute(state.targetingReticulePosition.X, state.targetingReticulePosition.Y);
            }
            else if (target == CommandTarget.Cursor)
            {
                VirtualMouse.MoveAbsolute(state.cursorPosition.X, state.cursorPosition.Y);
            }
            else
            {
                //
            }
            #endregion


            #region Keys
            if (key.HasValue)
            {
                if (commandState == KeyboardMouseCommandState.Down)
                {
                    VirtualKeyboard.KeyDown(key.Value);
                }
                else
                {
                    VirtualKeyboard.KeyUp(key.Value);
                }
            }
            #endregion

            #region Mouse Buttons
            if (mouseButton.HasValue)
            {
                if (commandState == KeyboardMouseCommandState.Down)
                {
                    if (mouseButton.Value == MouseButtons.Left)
                    {
                        VirtualMouse.LeftDown();
                    }
                    else if (mouseButton.Value == MouseButtons.Right)
                    {
                        VirtualMouse.RightDown();
                    }
                }
                else
                {
                    if (mouseButton.Value == MouseButtons.Left)
                    {
                        VirtualMouse.LeftUp();
                    }
                    else if (mouseButton.Value == MouseButtons.Right)
                    {
                        VirtualMouse.RightUp();
                    }
                }
            }
            #endregion


        }
    }
}
