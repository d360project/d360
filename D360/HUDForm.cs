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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
//using System.Windows.Forms;

namespace D360
{
    public partial class HUDForm : System.Windows.Forms.Form
    {


        public bool diabloActive = false;
        private bool setNonTopmost = false;

        public bool hudDisabled = false;

        private int screenWidth;
        private int screenHeight;

        HUD hud;
        InputProcessor inputProcessor;

        D3BindingsForm d3bindingsForm;
        ConfigForm configForm;

        GamePadState oldGamePadState;
        KeyboardState oldKeyboardState;

        /*
        Thread hudlessUpdateThread;
        private bool terminateThread = false;

        private bool currentlyUpdating = false;
         */

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

            ClientSize = new System.Drawing.Size(screenWidth, screenHeight);


            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;  // no borders

            Left = 0;
            Top = 0;

            TopMost = true;        // make the form always on top                     
            Visible = true;        // Important! if this isn't set, then the form is not shown at all

            // Set the form click-through
            int initialStyle = GetWindowLong(this.Handle, -20);

            if (WindowFunctions.isCompositionEnabled())
            {
                SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);
            }
            else
            {
                hudDisabled = true;
            }


            inputProcessor = new InputProcessor(GamePad.GetState(0));



            hud = new HUD(this.Handle);
            hud.screenWidth = screenWidth;
            hud.screenHeight = screenHeight;



            // Extend aero glass style on form init
            OnResize(null);

            d3bindingsForm = new D3BindingsForm();
            d3bindingsForm.inputProcessor = inputProcessor;


            if (File.Exists(@"D3Bindings.xml"))
            {
                inputProcessor.d3Bindings = LoadD3Bindings();
            }
            else
            {
                SaveD3Bindings(inputProcessor.d3Bindings);
                d3bindingsForm.Show();
            }



            configForm = new ConfigForm();
            configForm.inputProcessor = inputProcessor;

            if (File.Exists(@"Config.xml"))
            {
                inputProcessor.config = LoadConfig();
            }
            else
            {
                SaveConfig(inputProcessor.config);
                configForm.Show();
            }

            inputProcessor.AddConfiguredBindings();

            Hotkey configHotKey = new Hotkey();

            configHotKey.KeyCode = System.Windows.Forms.Keys.F10;
            configHotKey.Control = true;
            configHotKey.Pressed += delegate
            {
                if (configForm.Visible)
                {
                    configForm.Visible = false;
                }
                else
                {
                    configForm.Visible = true;
                }
            };

            configHotKey.Register(this);



            Hotkey bindingsHotKey = new Hotkey();

            bindingsHotKey.KeyCode = System.Windows.Forms.Keys.F11;
            bindingsHotKey.Control = true;
            bindingsHotKey.Pressed += delegate
            {
                if (d3bindingsForm.Visible)
                {
                    d3bindingsForm.Visible = false;
                }
                else
                {
                    d3bindingsForm.Visible = true;
                }
            };

            bindingsHotKey.Register(this);



            Hotkey quitHotKey = new Hotkey();

            quitHotKey.KeyCode = System.Windows.Forms.Keys.F12;
            quitHotKey.Control = true;
            quitHotKey.Pressed += delegate
            {
                this.Close();
            };

            quitHotKey.Register(this);

            oldGamePadState = GamePad.GetState(0, GamePadDeadZone.Circular);
            oldKeyboardState = Keyboard.GetState();

            if (hudDisabled)
            {
                this.Visible = false;
                this.ClientSize = new System.Drawing.Size(0, 0);

                //hudlessUpdateThread = new Thread(new ThreadStart(DoUpdate));
                //hudlessUpdateThread.Start();
                //while (!hudlessUpdateThread.IsAlive) ;

                backgroundWorker1.RunWorkerAsync();
            }

        }

        /*
        public void DoUpdate()
        {
            while (!terminateThread)
            {
                if (!currentlyUpdating)
                {
                    currentlyUpdating = true;
                    diabloActive = false;
                    string foregroundWindowString = WindowFunctions.GetActiveWindowTitle();

                    try
                    {
                        if (foregroundWindowString != null)
                        {
                            if (foregroundWindowString.ToUpper() == "DIABLO III")
                            {
                                diabloActive = true;

                                if (!setNonTopmost)
                                {
                                    WindowFunctions.DisableTopMost(WindowFunctions.GetForegroundWindowHandle());

                                    setNonTopmost = true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string crashPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\crash.txt";
                        using (StreamWriter outfile = new StreamWriter(crashPath, true))
                        {
                            outfile.WriteLine();
                            outfile.WriteLine(DateTime.Now.ToString());
                            outfile.WriteLine(ex.Message);
                            outfile.WriteLine(ex.StackTrace);
                            outfile.WriteLine();
                            outfile.Flush();
                        }
                        MessageBox.Show("Exception in windowing functions. Written to crash.txt.");
                        this.Close();
                    }

                    try
                    {
                        if (diabloActive) LogicUpdate();
                    }
                    catch (Exception ex)
                    {
                        string crashPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\crash.txt";
                        using (StreamWriter outfile = new StreamWriter(crashPath, true))
                        {
                            outfile.WriteLine();
                            outfile.WriteLine(DateTime.Now.ToString());
                            outfile.WriteLine(ex.Message);
                            outfile.WriteLine(ex.StackTrace);
                            outfile.WriteLine();
                            outfile.Flush();
                        }
                        MessageBox.Show("Exception in Logic update. Written to crash.txt.");
                        this.Close();
                    }

                    if (d3bindingsForm.Visible)
                    {
                        d3bindingsForm.Refresh();
                    }

                    if (configForm.Visible)
                    {
                        configForm.Refresh();
                    }

                    currentlyUpdating = false;
                }
                Thread.Sleep(10);
            }
        }
         */

        private void SaveD3Bindings(D3Bindings bindings)
        {
            var bindingsFileStream = new FileStream(Application.StartupPath + @"\D3Bindings.xml", FileMode.Create);
            var bindingsXMLSerializer = new XmlSerializer(typeof(D3Bindings));
            bindingsXMLSerializer.Serialize(bindingsFileStream, bindings);
            bindingsFileStream.Close();
        }

        private D3Bindings LoadD3Bindings()
        {
            var bindingsFileStream = new FileStream(Application.StartupPath + @"\D3Bindings.xml", FileMode.Open);
            var bindingsXMLSerializer = new XmlSerializer(typeof(D3Bindings));
            D3Bindings result = (D3Bindings)bindingsXMLSerializer.Deserialize(bindingsFileStream);
            bindingsFileStream.Close();
            return result;
        }


        private void SaveConfig(Configuration config)
        {
            var configFileStream = new FileStream(Application.StartupPath + @"\Config.xml", FileMode.Create);
            var configXMLSerializer = new XmlSerializer(typeof(Configuration));
            configXMLSerializer.Serialize(configFileStream, config);
            configFileStream.Close();
        }

        private Configuration LoadConfig()
        {
            var ConfigFileStream = new FileStream(Application.StartupPath + @"\Config.xml", FileMode.Open);
            var ConfigXMLSerializer = new XmlSerializer(typeof(Configuration));
            Configuration result = (Configuration)ConfigXMLSerializer.Deserialize(ConfigFileStream);
            ConfigFileStream.Close();
            return result;
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
            diabloActive = false;
            string foregroundWindowString = WindowFunctions.GetActiveWindowTitle();

            try
            {
                if (foregroundWindowString != null)
                {
                    if (foregroundWindowString.ToUpper() == "DIABLO III")
                    {
                        diabloActive = true;

                        if (!setNonTopmost)
                        {
                            WindowFunctions.DisableTopMost(WindowFunctions.GetForegroundWindowHandle());

                            setNonTopmost = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string crashPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\crash.txt";
                using (StreamWriter outfile = new StreamWriter(crashPath, true))
                {
                    outfile.WriteLine();
                    outfile.WriteLine(DateTime.Now.ToString());
                    outfile.WriteLine(ex.Message);
                    outfile.WriteLine(ex.StackTrace);
                    outfile.WriteLine();
                    outfile.Flush();
                }
                MessageBox.Show("Exception in windowing functions. Written to crash.txt.");
                this.Close();
            }

            try
            {
                hud.Draw(inputProcessor.currentControllerState, diabloActive);
            }
            catch (Exception ex)
            {
                string crashPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\crash.txt";
                using (StreamWriter outfile = new StreamWriter(crashPath, true))
                {
                    outfile.WriteLine();
                    outfile.WriteLine(DateTime.Now.ToString());
                    outfile.WriteLine(ex.Message);
                    outfile.WriteLine(ex.StackTrace);
                    outfile.WriteLine();
                    outfile.Flush();
                }
                MessageBox.Show("Exception in HUD draw. Written to crash.txt.");
                this.Close();
            }

            // Redraw immediatily

            Invalidate();

            try
            {
                if (diabloActive) LogicUpdate();
            }
            catch (Exception ex)
            {
                string crashPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\crash.txt";
                using (StreamWriter outfile = new StreamWriter(crashPath, true))
                {
                    outfile.WriteLine();
                    outfile.WriteLine(DateTime.Now.ToString());
                    outfile.WriteLine(ex.Message);
                    outfile.WriteLine(ex.StackTrace);
                    outfile.WriteLine();
                    outfile.Flush();
                }
                MessageBox.Show("Exception in Logic update. Written to crash.txt.");
                this.Close();
            }

            if (d3bindingsForm.Visible)
            {
                d3bindingsForm.Refresh();
            }

            if (configForm.Visible)
            {
                configForm.Refresh();
            }
        }


        public void BindingsUpdate()
        {
            GamePadState newState = GamePad.GetState(0, GamePadDeadZone.Circular);

            if ((newState.IsButtonDown(Buttons.Back)) && (newState.IsButtonDown(Buttons.Start)))
            {
                if ((oldGamePadState.IsButtonUp(Buttons.Back)) || (oldGamePadState.IsButtonUp(Buttons.Start)))
                {
                    if (d3bindingsForm.Visible)
                    {
                        d3bindingsForm.Visible = false;
                    }
                    else
                    {
                        d3bindingsForm.Visible = true;
                    }
                }
            }

            oldGamePadState = newState;


        }

        public void LogicUpdate()
        {
            inputProcessor.Update(GamePad.GetState(0, GamePadDeadZone.Circular));

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

        private void HUDForm_Load(object sender, EventArgs e)
        {

        }

        private void HUDForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //terminateThread = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //DoUpdate();
        }






    }
}
