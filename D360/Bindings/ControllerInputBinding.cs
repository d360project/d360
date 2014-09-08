using D360.InputEmulation;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D360
{
    public class ControllerInputBinding
    {
        public Buttons button { get; set; }
        public ControllerTriggerBinding trigger { get; set; }
        public ControllerStickBinding stick { get; set; }


        public ControllerButtonState buttonState { get; set; }
        public ControllerTriggerState triggerState { get; set; }


        public List<Command> commands;

        public ControllerInputBinding()
        {
            commands = new List<Command>();
        }



        internal void ExecuteCommands(ref ControllerState state)
        {
            foreach (Command command in commands)
            {
                command.Execute(ref state);
            }
        }



        /// <summary>
        /// createButtonKeyBindings creates two new binding and command sets, which directly maps a controller button to a keyboard key.
        /// Given keyboard key is signalled as down when the button is down, and up when the button is up.
        /// </summary>
        /// <param name="button">Controller button to bind</param>
        /// <param name="key">Keyboard Key to bind </param>
        /// <param name="applicableMode">Which input mode in which this binding is active - all modes by default.</param>
        /// <param name="target">Indicates if the key should be pressed with the mouse cursor at a particular location (cursorPosition, reticulePosition, or none)</param>
        /// <returns>A two-element array of ControllerInputBinding, to be passed to bindings.AddRange()</returns>
        public static ControllerInputBinding[] createButtonKeyBindings(Buttons button, System.Windows.Forms.Keys key, InputMode applicableMode = InputMode.All, CommandTarget target = CommandTarget.None)
        {
            ControllerInputBinding downResult = new ControllerInputBinding();
            downResult.button = button;
            downResult.buttonState = ControllerButtonState.OnDown;
            KeyboardCommand newCommand = new KeyboardCommand();
            newCommand.key = key;
            newCommand.commandState = ButtonState.Down;
            newCommand.applicableMode = applicableMode;
            newCommand.target = target;
            downResult.commands.Add(newCommand);

            ControllerInputBinding upResult = new ControllerInputBinding();
            upResult = new ControllerInputBinding();
            upResult.button = button;
            upResult.buttonState = ControllerButtonState.OnUp;
            newCommand = new KeyboardCommand();
            newCommand.key = key;
            newCommand.commandState = ButtonState.Up;
            newCommand.applicableMode = applicableMode;
            newCommand.target = target;
            upResult.commands.Add(newCommand);

            return new ControllerInputBinding[2] { downResult, upResult };
        }

        /// <summary>
        /// createButtonModeChangeBinding creates a new binding and command set, which changes between input modes.
        /// </summary>
        /// <param name="button">Controller button to bind</param>
        /// <param name="newMode">Which input mode to change to. Disregarded if toggle == true</param>
        /// <param name="toggle">If toggle, newMode is ignored, and the bound key will instead cycle through all available modes</param>
        /// <param name="applicableMode">Which input mode in which this binding is active - all modes by default.</param>
        /// <param name="target">Indicates if the key should be pressed with the mouse cursor at a particular location (cursorPosition, reticulePosition, or none)</param>
        /// <returns></returns>
        public static ControllerInputBinding createButtonModeChangeBinding(Buttons button, InputMode newMode, Boolean toggle = false, InputMode applicableMode = InputMode.All, CommandTarget target = CommandTarget.None)
        {
            ControllerInputBinding result = new ControllerInputBinding();
            result.button = button;
            result.buttonState = ControllerButtonState.OnDown;

            StateChangeCommand newCommand = new StateChangeCommand();
            newCommand.stateChange = new StateChange() { toggle = toggle, newMode = newMode };
            newCommand.applicableMode = applicableMode;
            newCommand.target = target;
            result.commands.Add(newCommand);

            return result;
        }

        /// <summary>
        /// createButtonKeyBindings creates two new binding and command sets, which directly maps a controller button to a mouse button.
        /// Given mouse button is signalled as down when the button is down, and up when the button is up.
        /// </summary>
        /// <param name="buttons">Controller button to bind</param>
        /// <param name="mouseButtons">Mouse button to bind</param>
        /// <param name="applicableMode">Which input mode in which this binding is active - all modes by default.</param>
        /// <param name="target">Indicates if the mouse button should be pressed with the mouse cursor at a particular location (cursorPosition, reticulePosition, or none)</param>
        /// <returns></returns>
        internal static ControllerInputBinding[] createMouseButtonBindings(Buttons button, System.Windows.Forms.MouseButtons mouseButton, InputMode applicableMode = InputMode.All, CommandTarget target = CommandTarget.None, ControllerButtonState cbState = ControllerButtonState.WhileDown)
        {
            ControllerInputBinding downResult = new ControllerInputBinding();
            downResult.button = button;
            downResult.buttonState = cbState;
            MouseButtonCommand newCommand = new MouseButtonCommand();
            newCommand.mouseButton = mouseButton;
            newCommand.commandState = ButtonState.Down;
            newCommand.applicableMode = applicableMode;
            newCommand.target = target;
            downResult.commands.Add(newCommand);


            ControllerInputBinding upResult = new ControllerInputBinding();
            upResult = new ControllerInputBinding();
            upResult.button = button;
            upResult.buttonState = ControllerButtonState.OnUp;
            newCommand = new MouseButtonCommand();
            newCommand.mouseButton = mouseButton;
            newCommand.commandState = ButtonState.Up;
            newCommand.applicableMode = applicableMode;
            newCommand.target = target;
            upResult.commands.Add(newCommand);

            return new ControllerInputBinding[2] { downResult, upResult };
        }

        internal static ControllerInputBinding createStickCursorMoveBinding(ControllerStick stick, Microsoft.Xna.Framework.Vector2 comparisonVector, StickState comparisonState, MouseMoveType moveType, Types.UIntVector moveScale, CommandTarget commandTarget, InputMode applicableMode)
        {
            ControllerInputBinding newBinding = new ControllerInputBinding();
            newBinding.stick = new ControllerStickBinding(stick, comparisonVector, comparisonState);

            CursorMoveCommand newCommand = new CursorMoveCommand();
            newCommand.mouseMove = new MouseMove();
            newCommand.mouseMove.commandTarget = commandTarget;
            newCommand.mouseMove.moveType = moveType;
            newCommand.mouseMove.moveScale = moveScale;

            newCommand.applicableMode = applicableMode;
            newBinding.commands.Add(newCommand);
            //bindings.Add(newBinding);

            return newBinding;
        }

        internal static ControllerInputBinding createStickCursorMoveBinding(ControllerStick stick, Microsoft.Xna.Framework.Vector2 comparisonVector, StickState comparisonState, StickState oldState, MouseMoveType moveType, Types.UIntVector moveScale, CommandTarget commandTarget, InputMode applicableMode)
        {
            ControllerInputBinding newBinding = new ControllerInputBinding();
            newBinding.stick = new ControllerStickBinding(stick, comparisonVector, comparisonState, oldState);

            CursorMoveCommand newCommand = new CursorMoveCommand();
            newCommand.mouseMove = new MouseMove();
            newCommand.mouseMove.commandTarget = commandTarget;
            newCommand.mouseMove.moveType = moveType;
            newCommand.mouseMove.moveScale = moveScale;

            newCommand.applicableMode = applicableMode;
            newBinding.commands.Add(newCommand);
            //bindings.Add(newBinding);

            return newBinding;
        }

        internal static ControllerInputBinding[] createStickKeyBinding(ControllerStick stick, Microsoft.Xna.Framework.Vector2 comparisonVector, System.Windows.Forms.Keys key, InputMode applicableMode = InputMode.All, CommandTarget target = CommandTarget.None)
        {
            ControllerInputBinding downResult = new ControllerInputBinding();
            downResult.stick = new ControllerStickBinding(stick, comparisonVector, StickState.NotEqual);
            KeyboardCommand newCommand = new KeyboardCommand();
            newCommand.key = key;
            newCommand.commandState = ButtonState.Down;
            newCommand.applicableMode = applicableMode;
            newCommand.target = target;
            downResult.commands.Add(newCommand);

            ControllerInputBinding upResult = new ControllerInputBinding();
            upResult = new ControllerInputBinding();
            upResult.stick = new ControllerStickBinding(stick, comparisonVector, StickState.Equal);
            newCommand = new KeyboardCommand();
            newCommand.key = key;
            newCommand.commandState = ButtonState.Up;
            newCommand.applicableMode = applicableMode;
            newCommand.target = target;
            upResult.commands.Add(newCommand);

            return new ControllerInputBinding[2] { downResult, upResult };
        }

        internal static ControllerInputBinding[] createStickKeyBinding(ControllerStick stick, Microsoft.Xna.Framework.Vector2 comparisonVector, StickState comparisonState, StickState oldState, System.Windows.Forms.Keys key, InputMode applicableMode = InputMode.All, CommandTarget target = CommandTarget.None)
        {
            ControllerInputBinding downResult = new ControllerInputBinding();
            downResult.stick = new ControllerStickBinding(stick, comparisonVector, comparisonState, oldState);
            KeyboardCommand newCommand = new KeyboardCommand();
            newCommand.key = key;
            newCommand.commandState = ButtonState.Down;
            newCommand.applicableMode = applicableMode;
            newCommand.target = target;
            downResult.commands.Add(newCommand);

            ControllerInputBinding upResult = new ControllerInputBinding();
            upResult = new ControllerInputBinding();
            upResult.stick = new ControllerStickBinding(stick, comparisonVector, comparisonState, oldState);
            newCommand = new KeyboardCommand();
            newCommand.key = key;
            newCommand.commandState = ButtonState.Up;
            newCommand.applicableMode = applicableMode;
            newCommand.target = target;
            upResult.commands.Add(newCommand);

            return new ControllerInputBinding[2] { downResult, upResult };
        }

        internal static IEnumerable<ControllerInputBinding> createTriggerKeyBindings(ControllerTrigger controllerTrigger, float triggerValue, System.Windows.Forms.Keys key, InputMode applicableMode, CommandTarget target)
        {
            ControllerInputBinding downResult = new ControllerInputBinding();
            downResult.trigger = new ControllerTriggerBinding(controllerTrigger, triggerValue);
            downResult.triggerState = ControllerTriggerState.OnDown;
            KeyboardCommand newCommand = new KeyboardCommand();
            newCommand.key = key;
            newCommand.commandState = ButtonState.Down;
            newCommand.applicableMode = applicableMode;
            newCommand.target = target;
            downResult.commands.Add(newCommand);

            ControllerInputBinding upResult = new ControllerInputBinding();
            upResult = new ControllerInputBinding();
            upResult.trigger = new ControllerTriggerBinding(controllerTrigger, triggerValue);
            upResult.triggerState = ControllerTriggerState.OnUp;
            upResult.buttonState = ControllerButtonState.OnUp;
            newCommand = new KeyboardCommand();
            newCommand.key = key;
            newCommand.commandState = ButtonState.Up;
            newCommand.applicableMode = applicableMode;
            newCommand.target = target;
            upResult.commands.Add(newCommand);

            return new ControllerInputBinding[2] { downResult, upResult };
        }

        internal static IEnumerable<ControllerInputBinding> createButtonLootBindings(Buttons buttons)
        {
            ControllerInputBinding addResult = new ControllerInputBinding();
            addResult.button = buttons;
            addResult.buttonState = ControllerButtonState.WhileDown;
            
            MouseButtonCommand newCommand = new MouseButtonCommand();
            newCommand.mouseButton = System.Windows.Forms.MouseButtons.Left;
            newCommand.commandState = ButtonState.Down;
            newCommand.target = CommandTarget.CenterRandom;
            newCommand.applicableMode = InputMode.Move;
            addResult.commands.Add(newCommand);

            newCommand = new MouseButtonCommand();
            newCommand.mouseButton = System.Windows.Forms.MouseButtons.Left;
            newCommand.commandState = ButtonState.Up;
            newCommand.target = CommandTarget.CenterRandom;
            newCommand.applicableMode = InputMode.Move;
            addResult.commands.Add(newCommand);

            return new ControllerInputBinding[1] { addResult };
        }
    }
}
