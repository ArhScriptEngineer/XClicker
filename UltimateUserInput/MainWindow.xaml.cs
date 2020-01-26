using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Threading;
using Application = System.Windows.Application;
using MessageBox = System.Windows.Forms.MessageBox;
using TextBox = System.Windows.Controls.TextBox;
using Timer = System.Threading.Timer;
using System.IO;
using Button = System.Windows.Controls.Button;
using Path = System.IO.Path;
using System.Diagnostics;
using System.ComponentModel;
using System.Net;

namespace UltimateUserInput
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            if (File.Exists("update.vbs"))
                File.Delete("update.vbs");
            Settings.Load();
            InitializeComponent();
            MinWidth = 400;
            MinHeight = 170;
            MaxWidth = MinWidth;
            MaxHeight = MinHeight;
            SwitcherKey = new WinHotKey(Settings.Current.Hotkey, Settings.Current.HotkeyModifier, AcSwitch);
            MainHotKey.Text = (Settings.Current.HotkeyModifier == KeyModifier.None?"": Settings.Current.HotkeyModifier.ToString() + "+") + Settings.Current.Hotkey;
            WriteItButton.Content = "Начать(" + MainHotKey.Text + ")";
            StartButton.Content = "Старт(" + MainHotKey.Text + ")";
            //KeyDown = new WinHotKey(Key.F2, KeyModifier.Alt, x => { Button_Click(null,null); });
            //KeyUp = new WinHotKey(Key.F3, KeyModifier.Alt, x => { Button_Click_1(null, null); });
            VkSelect.ItemsSource = Enum.GetValues(typeof(WinApi.Vk));
            VkSelect.SelectedItem = WinApi.Vk.VK_F24;
            new Task(() =>
            {
                Thread.CurrentThread.Name = "Updating";
                string[] Vers = Extentions.ApiServer(ApiServerAct.CheckVersion).Split(' ');
                if (Vers.Length == 3)
                {
                    if (Vers[0] == "0")
                    {
                        Extentions.AsyncWorker(() => StartUpdate(Vers[1]));
                    }
                }
            }).Start();
            Version.Content = "v"+Extentions.Version;
        }

        #region Updater
        private void StartUpdate(string downUrl)
        {
            ModeTab.SelectedIndex = 4;
            WebClient web = new WebClient();
            web.DownloadFileAsync(new Uri(downUrl), Path.GetFileNameWithoutExtension(Extentions.AppFile) + ".update");
            web.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged2);
            web.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompleted2);
        }
        public void DownloadProgressChanged2(object sender, DownloadProgressChangedEventArgs e)
        {
            UpdateProgress.Value = e.ProgressPercentage;
            //Procents.Content = e.ProgressPercentage + "%";
            UpdateStatus.Content = "Скачиваем новую версию (" + e.BytesReceived + "bytes/" + e.TotalBytesToReceive + "bytes)";
        }
        public void DownloadFileCompleted2(object sender, AsyncCompletedEventArgs e)
        {
            FileStream Batch = File.Create("update.vbs");
            string UpdFile = System.IO.Path.GetFileNameWithoutExtension(Extentions.AppFile) + ".update";
            byte[] Data = Encoding.Default.GetBytes("WScript.Sleep(500)"
+ "\r\nOn Error Resume next"
+ "\r\nDim fso, Del, Upd, WshShell"
+ "\r\nSet fso = CreateObject(\"Scripting.FileSystemObject\")"
+ "\r\nSet WshShell = WScript.CreateObject(\"WScript.Shell\")"
+ "\r\nSet Del = fso.GetFile(\"" + Extentions.AppFile + "\")"
+ "\r\nIf (fso.FileExists(\"" + UpdFile + "\")) Then"
+ "\r\n     Set Upd = fso.GetFile(\"" + UpdFile + "\")"
+ "\r\n     Del.Delete"
+ "\r\n     Upd.Name = \"" + System.IO.Path.GetFileName(Extentions.AppFile) + "\""
+ "\r\n     WshShell.Run \"" + Extentions.AppFile + "\""
+ "\r\nElse"
+ "\r\n     WshShell.Run \"" + Extentions.AppFile + "\""
+ "\r\nEnd If"
+ "\r\nOn Error GoTo 0");
            Batch.Write(Data, 0, Data.Length);
            Batch.Close();
            Process.Start("update.vbs");
            Application.Current.Shutdown();
        }
        #endregion

        #region Menu
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ModeTab.SelectedIndex = 0;
            
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            ModeTab.SelectedIndex = 1;
            
        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            ModeTab.SelectedIndex = 2;
            
        }
        int OldMode = 0;
        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            if (ModeTab.SelectedIndex != 3)
            {
                OldMode = ModeTab.SelectedIndex;
                ModeTab.SelectedIndex = 3;
            }
            else
            {
                ModeTab.SelectedIndex = OldMode;
                OldMode = 3;
            }
        }
        private void ModeTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch (ModeTab.SelectedIndex)
                {
                    case 0:
                        MinWidth = 400;
                        MinHeight = 170;
                        MaxWidth = MinWidth;
                        MaxHeight = MinHeight;
                        ScriptMenu.Width = 0;
                        EventsMenu.Width = 0;
                        break;
                    case 1:
                        MinWidth = 800;
                        MinHeight = 400;
                        ScriptMenu.Width = 0;
                        EventsMenu.Width = 0;
                        MaxWidth = Double.PositiveInfinity;
                        MaxHeight = Double.PositiveInfinity;
                        break;
                    case 2:
                        MinWidth = 600;
                        MinHeight = 300;
                        ScriptMenu.Width = 53;
                        EventsMenu.Width = 62;
                        MaxWidth = Double.PositiveInfinity;
                        MaxHeight = Double.PositiveInfinity;
                        break;
                    case 3:
                        MinWidth = 600;
                        MinHeight = 300;
                        ScriptMenu.Width = 0;
                        EventsMenu.Width = 0;
                        MaxWidth = Double.PositiveInfinity;
                        MaxHeight = Double.PositiveInfinity;
                        break;
                    case 4:
                        MinWidth = 400;
                        MinHeight = 170;
                        MaxWidth = MinWidth;
                        MaxHeight = MinHeight;
                        ScriptMenu.Width = 0;
                        EventsMenu.Width = 0;
                        Menu.IsEnabled = false;
                        break;
                }
            }
            catch
            {

            }
        }
        #endregion

        #region Settings
        int keyMode = -1;
        private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift
            || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl
            || e.Key == Key.LeftAlt || e.Key == Key.RightAlt) keyMode = -1;
            else
            {
                Key key = e.Key;
                if (key == Key.System)
                {
                    key = e.SystemKey;
                    keyMode = 0;
                }
                else if (keyMode == 0) keyMode = -1;

                SwitcherKey.Unregister();
                SwitcherKey.Dispose();
                string mode = "";
                switch (keyMode)
                {
                    case 0:
                        mode = "Alt+";
                        SwitcherKey = new WinHotKey(key, KeyModifier.Alt, AcSwitch);
                        Settings.Current.HotkeyModifier = KeyModifier.Alt;
                        break;
                    case 1:
                        mode = "Ctrl+";
                        SwitcherKey = new WinHotKey(key, KeyModifier.Ctrl, AcSwitch);
                        Settings.Current.HotkeyModifier = KeyModifier.Ctrl;
                        break;
                    case 2:
                        mode = "Shift+";
                        SwitcherKey = new WinHotKey(key, KeyModifier.Shift, AcSwitch);
                        Settings.Current.HotkeyModifier = KeyModifier.Shift;
                        break;
                    default:
                        SwitcherKey = new WinHotKey(key, KeyModifier.None, AcSwitch);
                        Settings.Current.HotkeyModifier = KeyModifier.None;
                        break;
                }
                Settings.Current.Hotkey = key;
                ((TextBox) sender).Text = (mode) + key;
                WriteItButton.Content = "Начать(" + MainHotKey.Text + ")";
                if (!timerEnable)
                {
                    StartButton.Content = "Старт(" + MainHotKey.Text + ")";
                }
                else
                {
                    StartButton.Content = "Стоп(" + MainHotKey.Text + ")";
                }
            }
        }
        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift) keyMode = 2;
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl) keyMode = 1;
        }
        #endregion

        #region AutoClicker
        static WinHotKey SwitcherKey;
        bool timerEnable = false;
        static WinApi.Vk otherButton;
        Timer timer = new Timer(timerTick);
        List<Thread> processes = new List<Thread>();
        static int selectedbutton = 0,multipler = 0;
        float MidiSpeed = 6;
        Dictionary<byte, bool> KeyStates = new Dictionary<byte, bool>();
        Stopwatch st = new Stopwatch();
        private void AcSwitch(WinHotKey Key)
        {
            switch (ModeTab.SelectedIndex)
            {
                case 0:
                    if (timerEnable)
                    {
                        timer.Change(Timeout.Infinite, 0);
                        StartButton.Content = "Старт(" + MainHotKey.Text + ")";
                        timerEnable = false;
                    }
                    else
                    {
                        timer.Change(0, (int)(1000 / Slider.Value));
                        StartButton.Content = "Стоп(" + MainHotKey.Text + ")";
                        timerEnable = true;
                    }
                    break;
                case 1:
                    if (processes.Count == 0)
                        WriteItButton.Content = "Остановить(" + MainHotKey.Text + ")";
                    if (processes.Count > 0)
                    {
                        foreach (var x in processes)
                        {
                            x.Abort();
                        }
                        processes.Clear();
                        if (Record.IsChecked.Value)
                        {
                            st.Stop();
                            foreach (var keyst in KeyStates)
                            {
                                if (keyst.Value)
                                {
                                    WinApi.Vk Button = (WinApi.Vk)keyst.Key;
                                    switch (Button)
                                    {
                                        case WinApi.Vk.VK_LBUTTON:
                                            InputInstructions.Append($"0 Mouse Up Left\n");
                                            break;
                                        case WinApi.Vk.VK_RBUTTON:
                                            InputInstructions.Append($"0 Mouse Up  Right\n");
                                            break;
                                        case WinApi.Vk.VK_MBUTTON:
                                            InputInstructions.Append($"0 Mouse Up  Middle\n");
                                            break;
                                        default:
                                            InputInstructions.Append($"0 Button Up {Button.ToString().Replace("VK_", "")}\n");
                                            break;
                                    }
                                }
                            }
                        }
                        UserInput.ButtonEvent(WinApi.Vk.VK_RSHIFT, UserInput.ButtonEvents.Up);
                        //Console.WriteLine(InputInstructions.ToString());
                        WriteItButton.Content = "Начать(" + MainHotKey.Text + ")";
                    }
                    else if (TextTyping.IsChecked.Value)
                    {
                        //Console.WriteLine(WinApi.GetKeyboardLayout());
                        string Text = TextToWriteAsUser.Text;
                        Task.Run(() =>
                        {
                            processes.Add(Thread.CurrentThread);
                            int index = 0;
                            string textr = Text.ToLower();
                            foreach (char chawr in Text.ToUpper())
                            {
                                bool Shift = textr[index] != Text[index];
                                if (UserInput.CharToScript.ContainsKey(chawr))
                                {
                                    Thread.Sleep(5);
                                    string Code = UserInput.CharToScript[chawr];
                                    if (Code.Contains("<SHIFT?>")) {
                                        if (Shift)
                                            Code = $"{Code.Replace("<SHIFT?>", "0 Button Down SHIFT\n")}\n5 Button Up SHIFT";
                                        else
                                            Code = Code.Replace("<SHIFT?>", "");
                                    }
                                    ScriptLanguage.RunScript(Code);
                                }
                                //else Console.WriteLine(chawr);
                                index++;
                            }
                            processes.Clear();
                            WriteItButton.Dispatcher.Invoke(() =>
                            WriteItButton.Content = "Начать(" + MainHotKey.Text + ")");
                        });
                    }
                    else if (Play.IsChecked.Value)
                    {
                        Task.Run(() =>
                        {
                            processes.Add(Thread.CurrentThread);
                            ScriptLanguage.RunScript(InputInstructions.ToString());
                            processes.Clear();
                            WriteItButton.Dispatcher.Invoke(() =>
                            WriteItButton.Content = "Начать(" + MainHotKey.Text + ")");
                        });
                    }
                    else if (Record.IsChecked.Value)
                    {
                        Task.Run(() =>
                        {
                            processes.Add(Thread.CurrentThread);
                            //Thread.Sleep(1000);
                            WinApi.MousePoint MousePos = new WinApi.MousePoint(0,0);
                            int cnt = 0;
                            InputInstructions.Clear();
                            st.Start();
                            while (true)
                            {
                                Thread.Sleep(1);
                                cnt++;
                                WinApi.MousePoint MousePosN = WinApi.GetCursorPosition();
                                if (MousePos != MousePosN)
                                {
                                    InputInstructions.Append($"{st.ElapsedMilliseconds} Mouse Set {MousePosN.X} {MousePosN.Y}\n");
                                    MousePos = MousePosN;
                                    st.Restart();
                                }
                                for (byte i = 0; i < 255; i++)
                                {
                                    bool presed = WinApi.GetKeyState(i);
                                    //bool presed = state != 0;
                                    //if (!presed && cnt == 0) continue;
                                    if (!KeyStates.ContainsKey(i))
                                    {
                                        KeyStates.Add(i, presed);
                                        continue;
                                    }
                                    if (KeyStates[i] != presed)
                                    {
                                        WinApi.Vk Button = (WinApi.Vk)i;
                                        string Word = presed?"Down":"Up";
                                        switch (Button)
                                        {
                                            case WinApi.Vk.VK_LBUTTON:
                                                InputInstructions.Append($"{st.ElapsedMilliseconds} Mouse {Word} Left\n");
                                                break;
                                            case WinApi.Vk.VK_RBUTTON:
                                                InputInstructions.Append($"{st.ElapsedMilliseconds} Mouse {Word} Right\n");
                                                break;
                                            case WinApi.Vk.VK_MBUTTON:
                                                InputInstructions.Append($"{st.ElapsedMilliseconds} Mouse {Word} Middle\n");
                                                break;
                                            default:
                                                InputInstructions.Append($"{st.ElapsedMilliseconds} Button {Word} {Button.ToString().Replace("VK_","")}\n");
                                                break;
                                        }
                                        KeyStates[i] = presed;
                                        st.Restart();// = 0;
                                    }
                                }
                            }
                            //processes.Clear();
                            //WriteItButton.Dispatcher.Invoke(() =>
                            //WriteItButton.Content = "Начать(" + MainHotKey.Text + ")");
                        });
                    }
                    break;
            }
        }
        static private void timerTick(object sender)
        {
            switch (selectedbutton)
            {
                case 0:
                    for (int i = 0; i < (int)multipler; i++)
                        UserInput.MouseClick(UserInput.MouseButton.Left);
                    break;
                case 1:
                    for (int i = 0; i < multipler; i++)
                        UserInput.MouseClick(UserInput.MouseButton.Right);
                    break;
                case 2:
                    for (int i = 0; i < multipler; i++)
                        UserInput.MouseClick(UserInput.MouseButton.Middle);
                    break;
                case 3:
                        WinApi.MouseEvent(WinApi.MouseEventFlags.Wheel,(-multipler)*100);
                    break;
                case 4:
                    for (int i = 0; i < multipler; i++)
                        UserInput.KeyboardClick(otherButton);
                    break;

            }
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(timerEnable) timer.Change(0, (int)(1000 / Slider.Value));
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            AcSwitch(null);
        }
        private void ButtonSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedbutton = ButtonSelector.SelectedIndex;
            if(KeySelector != null)
                if (selectedbutton != 4)
                {
                    KeySelector.IsEnabled = false;
                }
                else
                {
                    KeySelector.IsEnabled = true;
                }
            if (Slider_Copy != null)
                if (selectedbutton != 3)
                {
                    Slider_Copy.Minimum = 1;
                }
                else
                {
                    Slider_Copy.Minimum = -50;
                }
        }
        private void Slider_Copy_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            multipler = (int)Slider_Copy.Value;
        }
        private void KeySelector_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeySelector.Text = e.Key.ToString();
            otherButton = (WinApi.Vk)KeyInterop.VirtualKeyFromKey(e.Key);
        }
        private void KeySelector_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeySelector.Text = e.Key.ToString();
            otherButton = (WinApi.Vk)KeyInterop.VirtualKeyFromKey(e.Key);
        }
        #endregion

        #region ProClicker
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            //EvenList.Items.Add(new MyEvenItem(MyEvenTypes.Mouse, "Новое событие мыши"));
        }
        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            //EvenList.Items.Add(new MyEvenItem(MyEvenTypes.Delay, "Новая задержка"));
        }
        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            //EvenList.Items.Clear();
        }
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            //EvenList.Items.Add(new MyEvenItem(MyEvenTypes.Keyboard, "Новое событие клавиатуры"));
        }

        byte SelectedKey;int Waiting = 0;
        private void VkNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (byte.TryParse(VkNum.Text, out byte key))
            {
                if (Enum.IsDefined(typeof(WinApi.Vk), key))
                {
                    VkSelect.SelectedItem = (WinApi.Vk)key;
                }
                else
                {
                    VkSelect.SelectedItem = WinApi.Vk.VK_UNKNOWN;
                }
                SelectedKey = key;
            }
            else
            {
                VkNum.Text = SelectedKey.ToString();
            }
            KeyName.Text = KeyInterop.KeyFromVirtualKey((int)SelectedKey).ToString();
        }
        private void VkSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WinApi.Vk Key = (WinApi.Vk)VkSelect.SelectedItem;
            if (Key != WinApi.Vk.VK_UNKNOWN)
            {
                VkNum.Text = ((byte)Key).ToString();
            }
            else if (byte.TryParse(VkNum.Text, out byte key) && Enum.IsDefined(typeof(WinApi.Vk), key))
            {
                VkSelect.SelectedItem = (WinApi.Vk)key;
            }
            else
            {
                VkSelect.SelectedItem = WinApi.Vk.VK_UNKNOWN;
            }
        }
        private void VkNum_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(WaitingBox.Text, out int key))
            {
                Waiting = key;
            }
            else
            {
                WaitingBox.Text = Waiting.ToString();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(Waiting);
            UserInput.ButtonEvent((WinApi.Vk)SelectedKey, UserInput.ButtonEvents.Down);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(Waiting);
            UserInput.ButtonEvent((WinApi.Vk)SelectedKey, UserInput.ButtonEvents.Up);
        }
        private void KeyName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            WinApi.Vk key = (WinApi.Vk)KeyInterop.VirtualKeyFromKey(e.Key);
            if (Enum.IsDefined(typeof(WinApi.Vk), key))
            {
                VkSelect.SelectedItem = key;
            }
            else
            {
                VkSelect.SelectedItem = WinApi.Vk.VK_UNKNOWN;
            }
            VkNum.Text = ((byte)key).ToString();
        }

        private void KeyName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private void WriteItButton_Click(object sender, RoutedEventArgs e)
        {
            AcSwitch(null);
        }
        StringBuilder InputInstructions = new StringBuilder();
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            OpenFileDialog Dial = new OpenFileDialog();
            Dial.Filter = "Инструкция ввода(*.u2i3)|*.u2i3";
            Dial.ShowDialog();
            string file = Dial.FileName;
            var btt = (Button)sender;
            //btt.Content = "Выбрать файл ("+Path.GetFileName(file)+")";
            //MidiPatch = file;
            InputInstructions.Clear();
            InputInstructions.Append(File.ReadAllText(file));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SaveFileDialog Dial = new SaveFileDialog();
            Dial.Filter = "Инструкция ввода(*.u2i3)|*.u2i3";
            Dial.ShowDialog();
            string file = Dial.FileName;
            var btt = (Button)sender;
            //btt.Content = "Выбрать файл ("+Path.GetFileName(file)+")";
            //MidiPatch = file;
            File.WriteAllText(file, InputInstructions.ToString());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(Waiting);
            UserInput.ButtonEvent((WinApi.Vk)SelectedKey, UserInput.ButtonEvents.Down);
            UserInput.ButtonEvent((WinApi.Vk)SelectedKey, UserInput.ButtonEvents.Up);
        }
        #endregion

        private void Window_Closed(object sender, EventArgs e)
        {
            Settings.Save();
        }
    }
}
