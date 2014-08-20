using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace D360.InputEmulation
{
    public static class VirtualKeyboard
    {
        static HashSet<System.Windows.Forms.Keys> downKeys;

        [DllImport("user32.dll")]
        static extern uint keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        public static void KeyDown(System.Windows.Forms.Keys key)
        {
            if (downKeys == null)
            {
                downKeys = new HashSet<System.Windows.Forms.Keys>();
            }

            if (downKeys.Contains(key))
            {
                // key is already down
            }
            else
            {
                downKeys.Add(key);
                keybd_event((byte)key, 0, 0, 0);
            }
        }

        public static void KeyUp(System.Windows.Forms.Keys key)
        {
            if (downKeys == null)
            {
                downKeys = new HashSet<System.Windows.Forms.Keys>();
            }

            if (downKeys.Contains(key))
            {
                // key is down, send up signal
                keybd_event((byte)key, 0, 0x0002, 0);
                downKeys.Remove(key);
            }
            else
            {
                // key is already up
            }

            
        }
    }
}
