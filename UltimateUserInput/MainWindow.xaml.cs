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
using Timer = System.Threading.Timer;

namespace UltimateUserInput
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SwitcherKey = new WinHotKey(Key.Q,KeyModifier.Alt, AcSwitch);
        }

        #region AutoClicker
        WinHotKey SwitcherKey;
        bool timerEnable = false;
        Timer timer = new Timer(timerTick);
        static int selectedbutton = 0,multipler = 0;
        private void AcSwitch(WinHotKey Key)
        {
            if (timerEnable)
            {
                timer.Change(Timeout.Infinite, 0);
                StartButton.Content = "Старт(Alt+Q)";
                timerEnable = false;
            }
            else
            {
                timer.Change(0, (int)(1000 / Slider.Value));
                StartButton.Content = "Стоп(Alt+Q)";
                timerEnable = true;
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
        }

        private void Slider_Copy_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            multipler = (int)Slider_Copy.Value;
        }
        #endregion


        #region Menu
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ModeTab.SelectedIndex = 0;
            MinWidth = 400;
            Width = MinWidth;
            MinHeight = 170;
            Height = MinHeight;
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            ModeTab.SelectedIndex = 1;
            MinWidth = 800;
            Width = MinWidth;
            MinHeight = 400;
            Height = MinHeight;
        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            ModeTab.SelectedIndex = 2;
            MinWidth = 600;
            Width = MinWidth;
            MinHeight = 300;
            Height = MinHeight;
        }
        #endregion
    }
}
