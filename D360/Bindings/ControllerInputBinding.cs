using D360.InputEmulation;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D360.Bindings
{
    public class ControllerInputBinding
    {
        public Buttons button { get; set; }
        public ControllerTriggerBinding trigger { get; set; }
        public ControllerStickBinding stick { get; set; }

        
        public ControllerButtonState buttonState { get; set; }
        public ControllerTriggerState triggerState { get; set; }
        

        public List<KeyboardMouseCommand> commands;

        public ControllerInputBinding()
        {
            commands = new List<KeyboardMouseCommand>();
        }


        internal void ExecuteCommands(ref ControllerState state)
        {
            foreach (KeyboardMouseCommand command in commands)
            {
                command.Execute(ref state);
            }
        }
    }
}
