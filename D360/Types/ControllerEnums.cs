using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D360
{
    public enum ControllerButtonState {OnDown, WhileDown, OnUp, WhileUp}
    public enum ControllerTrigger { Left, Right }
    public enum ControllerStick { Left, Right }
    public enum ButtonState {  Down, Up }
    public enum InputMode { All, None, Move, Pointer }
    public enum ControllerTriggerState { OnDown, WhileDown, OnUp, WhileUp }
    public enum CommandTarget { Cursor, TargetReticule, None }
    public enum MouseMoveType { CurrentState, Absolute, Relative }
    public enum StickState { Equal, NotEqual, Any}
}
