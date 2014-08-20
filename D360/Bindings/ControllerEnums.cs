using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D360.Bindings
{
    public enum ControllerButtonState { Down, Up }
    public enum ControllerTrigger { Left, Right }
    public enum ControllerStick { Left, Right }
    public enum KeyboardMouseCommandState {  Down, Up }
    public enum InputMode { All, None, Move, Pointer }
    public enum ControllerTriggerState { OnDown, WhileDown, OnUp, WhileUp }
    public enum CommandTarget { Cursor, TargetReticule, None }
    public enum MouseMoveType { CurrentState, Absolute, Relative }
}
