using D360.InputEmulation;
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
    public partial class HUDForm : System.Windows.Forms.Form
    {
        

        

        private int screenWidth;
        private int screenHeight;



        HUD hud;
        InputProcessor inputProcessor;
        
        /// <summary>
        /// Gets an IServiceProvider containing our IGraphicsDeviceService.
        /// This can be used with components such as the ContentManager,
        /// which use this service to look up the GraphicsDevice.
        /// </summary>


        public HUDForm()
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


            inputProcessor = new InputProcessor(GamePad.GetState(0));

            hud = new HUD(this.Handle);
            hud.screenWidth = screenWidth;
            hud.screenHeight = screenHeight;


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
            //
            hud.Draw(inputProcessor.currentControllerState);

            // Redraw immediatily
            //updateInput();
            inputProcessor.Update(GamePad.GetState(0));

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

        
    }
}
