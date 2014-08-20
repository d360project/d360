using D360.Bindings;
using D360.InputEmulation;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D360
{
    public class InputProcessor
    {
        GamePadState lastState;

        public ControllerState currentControllerState;

        public Point center;

        

        public List<ControllerInputBinding> bindings;

        public InputProcessor(GamePadState initialState)
        {
            center = new Point(32768, 30850);

            bindings = new List<ControllerInputBinding>();
            lastState = initialState;

            currentControllerState = new ControllerState();
            currentControllerState.inputMode = InputMode.Pointer;
            currentControllerState.targetingReticulePosition = center;
            currentControllerState.cursorPosition = center;
            currentControllerState.centerPosition = center;

            // TESTING BINDINGS
            #region A to 1 binding
            ControllerInputBinding newBinding = new ControllerInputBinding();
            newBinding.button = Buttons.A;
            newBinding.buttonState = ControllerButtonState.Down;
            KeyboardMouseCommand newCommand = new KeyboardMouseCommand();
            newCommand.key = System.Windows.Forms.Keys.D1;
            newCommand.commandState = KeyboardMouseCommandState.Down;
            newCommand.applicableMode = InputMode.All;
            newCommand.target = CommandTarget.None;
            newBinding.commands.Add(newCommand);
            bindings.Add(newBinding);

            newBinding = new ControllerInputBinding();
            newBinding.button = Buttons.A;
            newBinding.buttonState = ControllerButtonState.Up;
            newCommand = new KeyboardMouseCommand();
            newCommand.key = System.Windows.Forms.Keys.D1;
            newCommand.commandState = KeyboardMouseCommandState.Up;
            newCommand.applicableMode = InputMode.All;
            newBinding.commands.Add(newCommand);

            bindings.Add(newBinding);
            #endregion

            #region Right Trigger to spam LeftClicks
            newBinding = new ControllerInputBinding();
            newBinding.trigger = new ControllerTriggerBinding(ControllerTrigger.Right, 0);
            newBinding.triggerState = ControllerTriggerState.WhileDown;
           
            newCommand = new KeyboardMouseCommand();
            newCommand.mouseButton = System.Windows.Forms.MouseButtons.Left;
            newCommand.commandState = KeyboardMouseCommandState.Down;
            newCommand.applicableMode = InputMode.All;
            newCommand.target = CommandTarget.None;
            newBinding.commands.Add(newCommand);

            newCommand = new KeyboardMouseCommand();
            newCommand.mouseButton = System.Windows.Forms.MouseButtons.Left;
            newCommand.commandState = KeyboardMouseCommandState.Up;
            newCommand.applicableMode = InputMode.All;
            newCommand.target = CommandTarget.None;
            newBinding.commands.Add(newCommand);
            bindings.Add(newBinding);
            #endregion

            #region Left Stick to control cursor position
            newBinding = new ControllerInputBinding();
            newBinding.stick = new ControllerStickBinding(ControllerStick.Left, Microsoft.Xna.Framework.Vector2.Zero);

            newCommand = new KeyboardMouseCommand();
            newCommand.mouseMove = new MouseMove();
            newCommand.mouseMove.commandTarget = CommandTarget.Cursor;
            newCommand.mouseMove.moveType = MouseMoveType.Absolute;
            newCommand.mouseMove.moveScale = new Point(30000, -25000);
            newCommand.applicableMode = InputMode.Move;
            newBinding.commands.Add(newCommand);
            bindings.Add(newBinding);
            #endregion

            #region Left Stick Click to toggle
            newBinding = new ControllerInputBinding();
            newBinding.button = Buttons.LeftStick;
            newBinding.buttonState = ControllerButtonState.Down;
            newCommand = new KeyboardMouseCommand();
            newCommand.stateChange = new StateChange() { toggle = true };
            newCommand.applicableMode = InputMode.All;
            newBinding.commands.Add(newCommand);
            bindings.Add(newBinding);
            #endregion
        }

        public void Update(GamePadState newState)
        {
            if (!newState.IsConnected)
            {
                return;
            }

            if (currentControllerState.inputMode == InputMode.Move)
            {
                VirtualMouse.MoveAbsolute(center.X, center.Y);
            }
            else
            {
                //
            }

            foreach (ControllerInputBinding binding in bindings)
            {
                if (binding.button != 0)
                {
                    if ((newState.IsButtonDown(binding.button)) && (lastState.IsButtonUp(binding.button)))
                    {
                        if (binding.buttonState == ControllerButtonState.Down)
                        {
                            binding.ExecuteCommands(ref currentControllerState);
                        }
                    }

                    else if ((newState.IsButtonUp(binding.button)) && (lastState.IsButtonDown(binding.button)))
                    {
                        if (binding.buttonState == ControllerButtonState.Up)
                        {
                            binding.ExecuteCommands(ref currentControllerState);
                        }
                    }
                }

                if (binding.trigger != null)
                {
                    if (binding.trigger.side == ControllerTrigger.Left)
                    {
                        if (binding.triggerState == ControllerTriggerState.OnDown)
                        {
                            if ((newState.Triggers.Left > binding.trigger.position) && (lastState.Triggers.Left < binding.trigger.position))
                            {
                                binding.ExecuteCommands(ref currentControllerState);
                            }
                        }
                        else if (binding.triggerState == ControllerTriggerState.OnUp)
                        {
                            if ((newState.Triggers.Left < binding.trigger.position) && (lastState.Triggers.Left > binding.trigger.position))
                            {
                                binding.ExecuteCommands(ref currentControllerState);
                            }
                        }
                        else if (binding.triggerState == ControllerTriggerState.WhileDown)
                        {
                            if (newState.Triggers.Left > 0)
                            {
                                binding.ExecuteCommands(ref currentControllerState);
                            }
                        }
                        else if (binding.triggerState == ControllerTriggerState.WhileUp)
                        {
                            if (newState.Triggers.Left == 0)
                            {
                                binding.ExecuteCommands(ref currentControllerState);
                            }
                        }
                    }
                    else if (binding.trigger.side == ControllerTrigger.Right)
                    {
                        if (binding.triggerState == ControllerTriggerState.OnDown)
                        {
                            if ((newState.Triggers.Right > binding.trigger.position) && (lastState.Triggers.Right < binding.trigger.position))
                            {
                                binding.ExecuteCommands(ref currentControllerState);
                            }
                        }
                        else if (binding.triggerState == ControllerTriggerState.OnUp)
                        {
                            if ((newState.Triggers.Right < binding.trigger.position) && (lastState.Triggers.Right > binding.trigger.position))
                            {
                                binding.ExecuteCommands(ref currentControllerState);
                            }
                        }
                        else if (binding.triggerState == ControllerTriggerState.WhileDown)
                        {
                            if (newState.Triggers.Right > 0)
                            {
                                binding.ExecuteCommands(ref currentControllerState);
                            }
                        }
                        else if (binding.triggerState == ControllerTriggerState.WhileUp)
                        {
                            if (newState.Triggers.Right == 0)
                            {
                                binding.ExecuteCommands(ref currentControllerState);
                            }
                        }
                    }
                }

                if (binding.stick != null)
                {
                    if (binding.stick.side == ControllerStick.Left)
                    {
                        //if ((newState.ThumbSticks.Left.X != 0) || (newState.ThumbSticks.Left.Y != 0))
                        {
                            currentControllerState.pointCommandValue = new Microsoft.Xna.Framework.Vector2(newState.ThumbSticks.Left.X, newState.ThumbSticks.Left.Y);
                            binding.ExecuteCommands(ref currentControllerState);
                        }
                    }
                    else if (binding.stick.side == ControllerStick.Right)
                    {
                        if ((newState.ThumbSticks.Right.X != 0) || (newState.ThumbSticks.Right.Y != 0))
                        {
                            binding.ExecuteCommands(ref currentControllerState);
                        }
                    }
                }
            }


            lastState = newState;
        }
    }
}

