using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D360
{
    public class CursorMoveCommand : Command
    {
        public MouseMove mouseMove { get; set; }
        public Vector2 inputCommandValue;

        public override bool Execute(ref ControllerState state)
        {
            if (base.Execute(ref state))
            {
                #region Mouse Movements
                if (mouseMove != null)
                {
                    if (mouseMove.commandTarget == CommandTarget.Cursor)
                    {
                        if (mouseMove.moveType == MouseMoveType.Absolute)
                        {
                            state.cursorPosition.X = state.centerPosition.X + (uint)(inputCommandValue.X * mouseMove.moveScale.X);
                            state.cursorPosition.Y = state.centerPosition.Y - (uint)(inputCommandValue.Y * mouseMove.moveScale.Y);
                        }
                        else if (mouseMove.moveType == MouseMoveType.Relative)
                        {
                            state.cursorPosition.X += (uint)(inputCommandValue.X * mouseMove.moveScale.X);
                            state.cursorPosition.Y -= (uint)(inputCommandValue.Y * mouseMove.moveScale.Y);
                        }
                    }
                    else if (mouseMove.commandTarget == CommandTarget.TargetReticule)
                    {
                        if (mouseMove.moveType == MouseMoveType.Absolute)
                        {
                            
                            state.targetingReticulePosition.X = state.centerPosition.X + (uint)(inputCommandValue.X * mouseMove.moveScale.X);
                            state.targetingReticulePosition.Y = state.centerPosition.Y - (uint)(inputCommandValue.Y * mouseMove.moveScale.Y);
                        }
                        else if (mouseMove.moveType == MouseMoveType.Relative)
                        {
                            state.targetingReticulePosition.X += (uint)(inputCommandValue.X * mouseMove.moveScale.X);
                            state.targetingReticulePosition.Y -= (uint)(inputCommandValue.Y * mouseMove.moveScale.Y);
                        }


                    }
                }
                return true;

                #endregion
            }
            else
            {
                return false;
            }


        }

    }
}
