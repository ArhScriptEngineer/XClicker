using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateUserInput
{
    class UserInput
    {

        public static Dictionary<char, string> CharToScript = new Dictionary<char, string>() {
            {'1',"5 Button Click 1" },
            {'2',"5 Button Click 2" },
            {'3',"5 Button Click 3" },
            {'4',"5 Button Click 4" },
            {'5',"5 Button Click 5" },
            {'6',"5 Button Click 6" },
            {'7',"5 Button Click 7" },
            {'8',"5 Button Click 8" },
            {'9',"5 Button Click 9" },
            {'0',"5 Button Click 0" },
            {'A',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click A" },
            {'B',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click B" },
            {'C',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click C" },
            {'D',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click D" },
            {'E',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click E" },
            {'F',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click F" },
            {'G',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click G" },
            {'H',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click H" },
            {'I',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click I" },
            {'J',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click J" },
            {'K',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click K" },
            {'L',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click L" },
            {'M',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click M" },
            {'N',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click N" },
            {'O',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click O" },
            {'Q',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click Q" },
            {'R',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click R" },
            {'S',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click S" },
            {'T',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click T" },
            {'U',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click U" },
            {'V',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click V" },
            {'W',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click W" },
            {'X',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click X" },
            {'Y',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click Y" },
            {'Z',"0 Keyboard SetLayout EN\n<SHIFT?>10 Button Click Z" },
            {'А',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click F" },
            {'Б',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click OEM_COMMA" },
            {'В',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click D" },
            {'Г',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click U" },
            {'Д',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click L" },
            {'Е',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click T" },
            {'Ё',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click OEM_3" },
            {'Ж',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click OEM_1" },
            {'З',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click P" },
            {'И',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click B" },
            {'Й',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click Q" },
            {'К',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click R" },
            {'Л',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click K" },
            {'М',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click V" },
            {'Н',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click Y" },
            {'О',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click J" },
            {'П',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click G" },
            {'Р',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click H" },
            {'С',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click C" },
            {'Т',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click N" },
            {'У',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click E" },
            {'Ф',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click A" },
            {'Х',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click OEM_4" },
            {'Ц',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click W" },
            {'Ч',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click X" },
            {'Ш',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click I" },
            {'Щ',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click O" },
            {'Ъ',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click OEM_6" },
            {'Ы',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click S" },
            {'Ь',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click M" },
            {'Э',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click OEM_7" },
            {'Ю',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click OEM_PERIOD" },
            {'Я',"0 Keyboard SetLayout RU\n<SHIFT?>10 Button Click Z" },
            {' ',"5 Button Click SPACE" },
            {'!',"0 Button Down SHIFT\n5 Button Click 1\n2 Button Up SHIFT" },
            {'@',"0 Keyboard SetLayout EN\n5 Button Down SHIFT\n5 Button Click 2\n2 Button Up SHIFT" },
            {'#',"0 Keyboard SetLayout EN\n5 Button Down SHIFT\n5 Button Click 3\n2 Button Up SHIFT" },
            {'$',"0 Keyboard SetLayout EN\n5 Button Down SHIFT\n5 Button Click 4\n2 Button Up SHIFT" },
            {'"',"0 Keyboard SetLayout RU\n5 Button Down SHIFT\n5 Button Click 2\n2 Button Up SHIFT" },
            {'№',"0 Keyboard SetLayout RU\n5 Button Down SHIFT\n5 Button Click 3\n2 Button Up SHIFT" },
            {';',"0 Keyboard SetLayout RU\n5 Button Down SHIFT\n5 Button Click 4\n2 Button Up SHIFT" },
            {'%',"0 Button Down SHIFT\n5 Button Click 5\n2 Button Up SHIFT" },
            {'^',"0 Keyboard SetLayout EN\n5 Button Down SHIFT\n5 Button Click 6\n2 Button Up SHIFT" },
            {'&',"0 Keyboard SetLayout EN\n5 Button Down SHIFT\n5 Button Click 7\n2 Button Up SHIFT" },
            {':',"0 Keyboard SetLayout RU\n5 Button Down SHIFT\n5 Button Click 6\n2 Button Up SHIFT" },
            {'?',"0 Keyboard SetLayout RU\n5 Button Down SHIFT\n5 Button Click 7\n2 Button Up SHIFT" },
            {'*',"0 Button Down SHIFT\n5 Button Click 8\n2 Button Up SHIFT" },
            {'(',"0 Button Down SHIFT\n5 Button Click 9\n2 Button Up SHIFT" },
            {')',"0 Button Down SHIFT\n5 Button Click 0\n2 Button Up SHIFT" },
            {'.',"0 Keyboard SetLayout EN\n10 Button Click DECIMAL" },
            {',',"0 Keyboard SetLayout RU\n10 Button Click DECIMAL" },
            {'<',"0 Button Down SHIFT\n5 Button Click OEM_COMMA\n2 Button Up SHIFT" },
            {'>',"0 Button Down SHIFT\n5 Button Click OEM_PERIOD\n2 Button Up SHIFT" },
            {'/',"5 Button Click DEVIDE" },
            {'-',"5 Button Click SUBTRACT" },
            {'=',"5 Button Click OEM_PLUS" },
            {'+',"5 Button Click ADD" },
            {'\\',"5 Button Click OEM_5" },
            {'_',"0 Button Down SHIFT\n5 Button Click OEM_MINUS\n2 Button Up SHIFT" },
            {'[',"0 Keyboard SetLayout EN\n10 Button Click OEM_4" },
            {']',"0 Keyboard SetLayout EN\n10 Button Click OEM_6" },
            {'`',"0 Keyboard SetLayout EN\n10 Button Click OEM_3" },
            {'~',"0 Keyboard SetLayout EN\n5 Button Down SHIFT\n10 Button Click OEM_3\n2 Button Up SHIFT" },
            {'\'',"0 Keyboard SetLayout EN\n10 Button Click OEM_7" },
            {'{',"0 Keyboard SetLayout EN\n5 Button Down SHIFT\n5 Button Click OEM_4\n2 Button Up SHIFT" },
            {'}',"0 Keyboard SetLayout EN\n5 Button Down SHIFT\n5 Button Click OEM_6\n2 Button Up SHIFT" },
            {'|',"0 Keyboard SetLayout EN\n5 Button Down SHIFT\n5 Button Click OEM_5\n2 Button Up SHIFT" },
            //{'\n',"0 Button Click RETURN" },
            {'\r',"100 Button Click RETURN" },
        };

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

        static public void MouseButtonEvent(MouseButton Button, ButtonEvents Event = ButtonEvents.None)
        {
            switch (Event)
            {
                case ButtonEvents.None:
                    break;
                case ButtonEvents.Up:
                    switch (Button)
                    {
                        case MouseButton.Middle:
                            WinApi.MouseEvent(WinApi.MouseEventFlags.MiddleUp);
                            break;
                        case MouseButton.Left:
                            WinApi.MouseEvent(WinApi.MouseEventFlags.LeftUp);
                            break;
                        case MouseButton.Right:
                            WinApi.MouseEvent(WinApi.MouseEventFlags.RightUp);
                            break;
                    }
                    break;
                case ButtonEvents.Down:
                    switch (Button)
                    {
                        case MouseButton.Middle:
                            WinApi.MouseEvent(WinApi.MouseEventFlags.MiddleDown);
                            break;
                        case MouseButton.Left:
                            WinApi.MouseEvent(WinApi.MouseEventFlags.LeftDown);
                            break;
                        case MouseButton.Right:
                            WinApi.MouseEvent(WinApi.MouseEventFlags.RightDown);
                            break;
                    }
                    break;
            }
            
        }

        static public void SetMouse(int X, int Y)
        {
            WinApi.SetCursorPos(X, Y);
        }

        static public void MouseMove(int X, int Y)
        {
            var Old = WinApi.GetCursorPosition();
            WinApi.SetCursorPos(Old.X + X, Old.Y + Y);
        }

        static public void KeyboardClick(WinApi.Vk Button)
        {
            WinApi.KeyBDEvent(Button);
            WinApi.KeyBDEvent(Button,WinApi.KeyBDdwFlags.KEYEVENTF_KEYUP);
        }

        static public void ButtonEvent(WinApi.Vk Button, ButtonEvents Event = ButtonEvents.None)
        {
            switch (Event)
            {
                case ButtonEvents.None:
                    break;
                case ButtonEvents.Up:
                    WinApi.KeyBDEvent(Button, WinApi.KeyBDdwFlags.KEYEVENTF_KEYUP);
                    break;
                case ButtonEvents.Down:
                    WinApi.KeyBDEvent(Button);
                    break;
            }
        }

        static public Color GetScreenColorAt(int X,int Y)
        {
            Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = WinApi.BitBlt(hDC, 0, 0, 1, 1, hSrcDC, X, Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }

            return screenPixel.GetPixel(0, 0);
        }

        public enum ButtonEvents : byte
        {
            None = 0,
            Up = 1,
            Down = 2
        }

        public enum MouseButton : byte
        {
            Middle = 0,
            Left = 1,
            Right = 2
        }
    }
}
