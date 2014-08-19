using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows.Forms;

namespace D360
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        // Directx graphics device
        GraphicsDevice dev = null;
        BasicEffect effect = null;

        SpriteBatch spriteBatch = null;

        private const int MOUSEEVENTF_ABSOLUTE = 0x8000; /* mouse move */

        private const int MOUSEEVENTF_MOVE = 0x0001; /* mouse move */
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002; /* left button down */
        private const int MOUSEEVENTF_LEFTUP = 0x0004; /* left button up */
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008; /* right button down */
        private const int MOUSEEVENTF_RIGHTUP = 0x0010; /* right button down */

        private bool mouseMoving = false;
        private bool mouse_absolute = false;
        private bool leftTriggerDown = false;
        private bool rightTriggerDown = false;

        private uint centerX = 32768;
        private uint centerY = 30850;

        private uint xScaleAbsolute = 30000;
        private uint yScaleAbsolute = 25000;

        private uint xScaleRelative = 20;
        private uint yScaleRelative = 20;

        private uint xScaleAbsoluteRightStick = 30000;
        private uint yScaleAbsoluteRightStick = 25000;

        private uint rightStickXValue;
        private uint rightStickYValue;

        private int screenWidth;
        private int screenHeight;

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInf);

        GamePadState oldGamePadState;

        Texture2D targetTexture;
        Texture2D moveModeTexture;
        Texture2D pointerModeTexture;

        /// <summary>
        /// Gets an IServiceProvider containing our IGraphicsDeviceService.
        /// This can be used with components such as the ContentManager,
        /// which use this service to look up the GraphicsDevice.
        /// </summary>


        public Form1()
        {
            InitializeComponent();


            StartPosition = FormStartPosition.Manual;
            screenWidth = Screen.GetBounds(this).Width;
            screenHeight = Screen.GetBounds(this).Height;

            Size = new System.Drawing.Size(screenWidth, screenHeight);
            Left = 0;
            Top = 0;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;  // no borders

            TopMost = true;        // make the form always on top                     
            Visible = true;        // Important! if this isn't set, then the form is not shown at all

            // Set the form click-through
            int initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);

            // Create device presentation parameters
            PresentationParameters p = new PresentationParameters();
            p.IsFullScreen = false;
            p.DeviceWindowHandle = this.Handle;
            p.BackBufferFormat = SurfaceFormat.Vector4;
            p.PresentationInterval = PresentInterval.One;

            // Create XNA graphics device
            dev = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, p);

            using (FileStream stream = new FileStream(@"Content\Target.png", FileMode.Open))
            {
                targetTexture = Texture2D.FromStream(dev, stream);
            }

            using (FileStream stream = new FileStream(@"Content\Move.png", FileMode.Open))
            {
                moveModeTexture = Texture2D.FromStream(dev, stream);
            }

            using (FileStream stream = new FileStream(@"Content\Pointer.png", FileMode.Open))
            {
                pointerModeTexture = Texture2D.FromStream(dev, stream);
            }

            // Init basic effect
            effect = new BasicEffect(dev);

            spriteBatch = new SpriteBatch(dev);

            // Extend aero glass style on form init
            OnResize(null);
        }


        protected override void OnResize(EventArgs e)
        {
            int[] margins = new int[] { 0, 0, Width, Height };

            // Extend aero glass style to whole form
            DwmExtendFrameIntoClientArea(this.Handle, ref margins);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // Set the form click-through
                //cp.ExStyle |= 0x80000 /* WS_EX_LAYERED */ | 0x20 /* WS_EX_TRANSPARENT */;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // do nothing here to stop window normal background painting
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            // Clear device with fully transparent black
            dev.Clear(new Microsoft.Xna.Framework.Color(0, 0, 0, 0.0f));

            int x = (int)(((centerX + rightStickXValue) / 65535.0f) * screenWidth) - 16;
            int y = (int)(((centerY + rightStickYValue) / 65535.0f) * screenHeight) - 16;
            Microsoft.Xna.Framework.Rectangle targetRect = new Microsoft.Xna.Framework.Rectangle(x, y, 32, 32);

            spriteBatch.Begin();

            if ((rightStickXValue != 0) && (rightStickYValue != 0))
            {
                spriteBatch.Draw(targetTexture, targetRect, new Microsoft.Xna.Framework.Color(1.0f, 1.0f, 1.0f, 0.5f));
            }

            //spriteBatch.Draw(moveModeTexture, new Microsoft.Xna.Framework.Rectangle(screenWidth - 256, screenHeight - 128, 128, 64), Microsoft.Xna.Framework.Color.White);

            if (mouse_absolute) 
            {
                spriteBatch.Draw(moveModeTexture, new Microsoft.Xna.Framework.Rectangle(screenWidth - 256, screenHeight - 128, 128, 64), Microsoft.Xna.Framework.Color.White);
            }
            else
            {
                spriteBatch.Draw(pointerModeTexture, new Microsoft.Xna.Framework.Rectangle(screenWidth - 256, screenHeight - 128, 128, 64), Microsoft.Xna.Framework.Color.White);
            }


            spriteBatch.End();

            // Present the device contents into form
            dev.Present();

            // Redraw immediatily
            updateInput();

            Invalidate();
        }



        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("dwmapi.dll")]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref int[] pMargins);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private void updateInput()
        {
            GamePadState newState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);

            // mouse position

            if (!newState.IsConnected)
            {
                return;

                mouseMoving = false;
                mouse_absolute = false;
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.D1);
                VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.D2);
                VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.D3);
                VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.D4);
                VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.LShiftKey);

                
            }

            uint xValue = (uint)(newState.ThumbSticks.Left.X * xScaleAbsolute);
            uint yValue = (uint)(newState.ThumbSticks.Left.Y * -yScaleAbsolute);

            if (mouse_absolute)
            {


                mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, centerX + xValue, centerY + yValue, 0, 0);

                if ((newState.ThumbSticks.Left.X != 0) && (newState.ThumbSticks.Left.Y != 0))
                {
                    if (!mouseMoving)
                    {
                        mouseMoving = true;
                        //mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);//make left button down
                        VirtualKeyboard.KeyDown(System.Windows.Forms.Keys.Space);
                    }

                }
                else
                {
                    if (mouseMoving)
                    {
                        mouseMoving = false;
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);//make left button up
                        VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.Space);
                    }
                }
            }
            else
            {
                xValue = (uint)(newState.ThumbSticks.Left.X * xScaleRelative);
                yValue = (uint)(newState.ThumbSticks.Left.Y * -yScaleRelative);

                if ((newState.ThumbSticks.Left.X == 0) && (newState.ThumbSticks.Left.Y == 0))
                {
                    xValue = (uint)(newState.ThumbSticks.Right.X * xScaleRelative);
                    yValue = (uint)(newState.ThumbSticks.Right.Y * -yScaleRelative);
                }

                mouse_event(MOUSEEVENTF_MOVE, xValue, yValue, 0, 0);
            }

            rightStickXValue = (uint)(newState.ThumbSticks.Right.X * xScaleAbsoluteRightStick);
            rightStickYValue = (uint)(newState.ThumbSticks.Right.Y * -yScaleAbsoluteRightStick);


            if (newState.Triggers.Left > 0)
            {
                if (!leftTriggerDown)
                {
                    VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.Space);
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.LShiftKey);
                    leftTriggerDown = true;
                }
            }
            else
            {
                if (leftTriggerDown)
                {
                    leftTriggerDown = false;
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    if (mouseMoving)
                    {
                        VirtualKeyboard.KeyDown(System.Windows.Forms.Keys.Space);
                    }
                }
            }

            /*
            if (newState.Triggers.Right > 0)
            {
                
                if (mouse_absolute)
                {
                    if ((rightStickXValue == 0) && (rightStickYValue == 0))
                    {
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, centerX + xValue, centerY + yValue, 0, 0);
                    }
                    else
                    {
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, centerX + rightStickXValue, centerY + rightStickYValue, 0, 0);
                    }
                }

                if (!rightTriggerDown)
                {
                    //if (mouse_absolute)
                    {
                        VirtualKeyboard.KeyDown(System.Windows.Forms.Keys.LShiftKey);

                    }
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);//make right button down
                    rightTriggerDown = true;
                }
            }
            else
            {
                if (rightTriggerDown)
                {
                    VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.LShiftKey);
                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);//make right button up
                    rightTriggerDown = false;
                }
            }
                 */

            
            if ((newState.Buttons.RightShoulder == Microsoft.Xna.Framework.Input.ButtonState.Pressed) && (oldGamePadState.Buttons.RightShoulder == Microsoft.Xna.Framework.Input.ButtonState.Released))
            {
                if (mouse_absolute)
                {
                    if ((rightStickXValue == 0) && (rightStickYValue == 0))
                    {
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, centerX + xValue, centerY + yValue, 0, 0);
                    }
                    else
                    {
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, centerX + rightStickXValue, centerY + rightStickYValue, 0, 0);
                    }
                }

                if (!rightTriggerDown)
                {
                    //if (mouse_absolute)
                    {
                        VirtualKeyboard.KeyDown(System.Windows.Forms.Keys.LShiftKey);

                    }
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);//make right button down
                    rightTriggerDown = true;
                }
            }
            else
            {
                if (rightTriggerDown)
                {
                    VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.LShiftKey);
                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);//make right button up
                    rightTriggerDown = false;
                }
            }

            if ((newState.Buttons.RightShoulder == Microsoft.Xna.Framework.Input.ButtonState.Released) && (oldGamePadState.Buttons.RightShoulder == Microsoft.Xna.Framework.Input.ButtonState.Pressed))
            {
                VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.LShiftKey);
                mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);//make right button up
            }
            

            if (newState.Buttons.LeftShoulder == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                if (mouse_absolute)
                {
                    if ((rightStickXValue == 0) && (rightStickYValue == 0))
                    {
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, centerX + xValue, centerY + yValue, 0, 0);
                    }
                    else
                    {
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, centerX + rightStickXValue, centerY + rightStickYValue, 0, 0);
                    }
                }
            }

            if ((newState.Buttons.LeftShoulder == Microsoft.Xna.Framework.Input.ButtonState.Pressed) && (oldGamePadState.Buttons.LeftShoulder == Microsoft.Xna.Framework.Input.ButtonState.Released))
            {
                if (leftTriggerDown) return;

                VirtualKeyboard.KeyDown(System.Windows.Forms.Keys.LShiftKey);
                /*
                if (mouse_absolute)
                {
                    mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, centerX + rightStickXValue, centerY + rightStickYValue, 0, 0);
                }
                 */
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);//make left button down
            }

            if ((newState.Buttons.LeftShoulder == Microsoft.Xna.Framework.Input.ButtonState.Released) && (oldGamePadState.Buttons.LeftShoulder == Microsoft.Xna.Framework.Input.ButtonState.Pressed))
            {
                if (leftTriggerDown) return;
                VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.LShiftKey);
                //if (!mouseMoving)
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);//make left button up
                }

            }




            // 

            if ((newState.Buttons.X == Microsoft.Xna.Framework.Input.ButtonState.Pressed) && (oldGamePadState.Buttons.X == Microsoft.Xna.Framework.Input.ButtonState.Released))
            {
                if (mouse_absolute)
                {
                    if ((rightStickXValue == 0) && (rightStickYValue == 0))
                    {
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, centerX + xValue, centerY + yValue, 0, 0);
                    }
                    else
                    {
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, centerX + rightStickXValue, centerY + rightStickYValue, 0, 0);
                    }
                }
                VirtualKeyboard.KeyDown(System.Windows.Forms.Keys.D1);
            }

            if ((newState.Buttons.X == Microsoft.Xna.Framework.Input.ButtonState.Released) && (oldGamePadState.Buttons.X == Microsoft.Xna.Framework.Input.ButtonState.Pressed))
            {
                VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.D1);
            }

            // 

            if ((newState.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Pressed) && (oldGamePadState.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Released))
            {
                if (mouse_absolute)
                {
                    if ((rightStickXValue == 0) && (rightStickYValue == 0))
                    {
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, centerX + xValue, centerY + yValue, 0, 0);
                    }
                    else
                    {
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, centerX + rightStickXValue, centerY + rightStickYValue, 0, 0);
                    }
                }
                VirtualKeyboard.KeyDown(System.Windows.Forms.Keys.D2);
            }

            if ((newState.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Released) && (oldGamePadState.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Pressed))
            {
                VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.D2);
            }

            // 

            if ((newState.Buttons.Y == Microsoft.Xna.Framework.Input.ButtonState.Pressed) && (oldGamePadState.Buttons.Y == Microsoft.Xna.Framework.Input.ButtonState.Released))
            {
                if (mouse_absolute)
                {
                    if ((rightStickXValue == 0) && (rightStickYValue == 0))
                    {
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, centerX + xValue, centerY + yValue, 0, 0);
                    }
                    else
                    {
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, centerX + rightStickXValue, centerY + rightStickYValue, 0, 0);
                    }
                }
                VirtualKeyboard.KeyDown(System.Windows.Forms.Keys.D3);
            }

            if ((newState.Buttons.Y == Microsoft.Xna.Framework.Input.ButtonState.Released) && (oldGamePadState.Buttons.Y == Microsoft.Xna.Framework.Input.ButtonState.Pressed))
            {
                VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.D3);
            }

            // 

            if ((newState.Buttons.B == Microsoft.Xna.Framework.Input.ButtonState.Pressed) && (oldGamePadState.Buttons.B == Microsoft.Xna.Framework.Input.ButtonState.Released))
            {
                if (mouse_absolute)
                {
                    if ((rightStickXValue == 0) && (rightStickYValue == 0))
                    {
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, centerX + xValue, centerY + yValue, 0, 0);
                    }
                    else
                    {
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, centerX + rightStickXValue, centerY + rightStickYValue, 0, 0);
                    }
                }
                VirtualKeyboard.KeyDown(System.Windows.Forms.Keys.D4);
            }

            if ((newState.Buttons.B == Microsoft.Xna.Framework.Input.ButtonState.Released) && (oldGamePadState.Buttons.B == Microsoft.Xna.Framework.Input.ButtonState.Pressed))
            {
                VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.D4);
            }

            if ((newState.Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed) && (oldGamePadState.Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Released))
            {
                VirtualKeyboard.KeyDown(System.Windows.Forms.Keys.Tab);
            }

            if ((newState.Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Released) && (oldGamePadState.Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed))
            {
                VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.Tab);
            }

            if ((newState.Buttons.Start == Microsoft.Xna.Framework.Input.ButtonState.Pressed) && (oldGamePadState.Buttons.Start == Microsoft.Xna.Framework.Input.ButtonState.Released))
            {
                mouse_absolute = true;
                VirtualKeyboard.KeyDown(System.Windows.Forms.Keys.Escape);
            }

            if ((newState.Buttons.Start == Microsoft.Xna.Framework.Input.ButtonState.Released) && (oldGamePadState.Buttons.Start == Microsoft.Xna.Framework.Input.ButtonState.Pressed))
            {
                mouse_absolute = false;
                VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.Escape);
            }

            if ((newState.DPad.Down == Microsoft.Xna.Framework.Input.ButtonState.Pressed) && (oldGamePadState.DPad.Down == Microsoft.Xna.Framework.Input.ButtonState.Released))
            {
                mouse_absolute = true;
                VirtualKeyboard.KeyDown(System.Windows.Forms.Keys.I);
            }

            if ((newState.DPad.Down == Microsoft.Xna.Framework.Input.ButtonState.Released) && (oldGamePadState.DPad.Down == Microsoft.Xna.Framework.Input.ButtonState.Pressed))
            {
                mouse_absolute = false;
                VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.I);
            }

            if ((newState.DPad.Up == Microsoft.Xna.Framework.Input.ButtonState.Pressed) && (oldGamePadState.DPad.Up == Microsoft.Xna.Framework.Input.ButtonState.Released))
            {
                VirtualKeyboard.KeyDown(System.Windows.Forms.Keys.Q);
            }

            if ((newState.DPad.Up == Microsoft.Xna.Framework.Input.ButtonState.Released) && (oldGamePadState.DPad.Up == Microsoft.Xna.Framework.Input.ButtonState.Pressed))
            {
                VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.Q);
            }

            if ((newState.DPad.Left == Microsoft.Xna.Framework.Input.ButtonState.Pressed) && (oldGamePadState.DPad.Left == Microsoft.Xna.Framework.Input.ButtonState.Released))
            {
                mouse_absolute = true;
                VirtualKeyboard.KeyDown(System.Windows.Forms.Keys.M);
            }

            if ((newState.DPad.Left == Microsoft.Xna.Framework.Input.ButtonState.Released) && (oldGamePadState.DPad.Left == Microsoft.Xna.Framework.Input.ButtonState.Pressed))
            {
                mouse_absolute = false;
                VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.M);
            }


            if ((newState.DPad.Right == Microsoft.Xna.Framework.Input.ButtonState.Pressed) && (oldGamePadState.DPad.Right == Microsoft.Xna.Framework.Input.ButtonState.Released))
            {
                VirtualKeyboard.KeyDown(System.Windows.Forms.Keys.T);
            }

            if ((newState.DPad.Right == Microsoft.Xna.Framework.Input.ButtonState.Released) && (oldGamePadState.DPad.Right == Microsoft.Xna.Framework.Input.ButtonState.Pressed))
            {
                VirtualKeyboard.KeyUp(System.Windows.Forms.Keys.T);
            }


            if ((newState.Buttons.LeftStick == Microsoft.Xna.Framework.Input.ButtonState.Pressed) && (oldGamePadState.Buttons.LeftStick == Microsoft.Xna.Framework.Input.ButtonState.Released))
            {
                if (mouseMoving)
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);//make left button up
                    mouseMoving = false;
                }
                mouse_absolute = !mouse_absolute;
            }

            oldGamePadState = newState;
        }
    }
}
