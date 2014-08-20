using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace D360.InputEmulation
{
    public static class VirtualMouse
    {
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000; /* mouse move */

        private const int MOUSEEVENTF_MOVE = 0x0001; /* mouse move */
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002; /* left button down */
        private const int MOUSEEVENTF_LEFTUP = 0x0004; /* left button up */
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008; /* right button down */
        private const int MOUSEEVENTF_RIGHTUP = 0x0010; /* right button down */


        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInf);

        public static void MoveAbsolute(int xValue, int yValue)
        {
            MoveAbsolute((uint)xValue, (uint)yValue);
        }

        public static void MoveAbsolute(uint xValue, uint yValue)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, xValue, yValue, 0, 0);
        }

        public static void MoveRelative(int xValue, int yValue)
        {
            MoveRelative((uint)xValue, (uint)yValue);
        }

        public static void MoveRelative(uint xValue, uint yValue)
        {
            mouse_event(MOUSEEVENTF_MOVE, (uint)xValue, (uint)yValue, 0, 0);
        }


        public static void LeftDown()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        }

        public static void LeftDown(uint xValue, uint yValue)
        {
            MoveRelative(xValue, yValue);
            LeftDown();
        }


        public static void LeftUp()
        {
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static void LeftUp(uint xValue, uint yValue)
        {
            MoveRelative(xValue, yValue);
            LeftUp();
        }


        public static void RightDown()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
        }

        public static void RightDown(uint xValue, uint yValue)
        {
            MoveRelative(xValue, yValue);
            RightDown();
        }


        public static void RightUp()
        {
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        public static void RightUp(uint xValue, uint yValue)
        {
            MoveRelative(xValue, yValue);
            RightUp();
        }

    }
}
