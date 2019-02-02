using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateUserInput
{
    class UserInput
    {
        static public void MouseClick(MouseButton Button)
        {
            switch (Button)
            {
                case MouseButton.Middle:
                    WinApi.MouseEvent(WinApi.MouseEventFlags.MiddleDown);
                    WinApi.MouseEvent(WinApi.MouseEventFlags.MiddleUp);
                    break;
                case MouseButton.Left:
                    WinApi.MouseEvent(WinApi.MouseEventFlags.LeftDown);
                    WinApi.MouseEvent(WinApi.MouseEventFlags.LeftUp);
                    break;
                case MouseButton.Right:
                    WinApi.MouseEvent(WinApi.MouseEventFlags.RightDown);
                    WinApi.MouseEvent(WinApi.MouseEventFlags.RightUp);
                    break;
            }
        }

        static public void KeyboardClick(WinApi.Vk Button)
        {
            WinApi.KeyBDEvent(Button);
            WinApi.KeyBDEvent(Button,WinApi.KeyBDdwFlags.KEYEVENTF_KEYUP);
        }

        public enum MouseButton : byte
        {
            Middle = 0,
            Left = 1,
            Right = 2
        }
    }
}
