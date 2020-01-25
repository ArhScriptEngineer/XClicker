using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace UltimateUserInput
{
    public class Settings
    {
        public static Settings Current = new Settings();
        private static Random Rand = new Random();
        public KeyModifier HotkeyModifier = KeyModifier.Alt;
        public Key Hotkey = Key.F1;
        /*public KeyModifier HotkeyModifier
        {
            get => (KeyModifier)hotkeyModifier;
            set => hotkeyModifier = (int)value;
        }
        public Key Hotkey
        {
            get => (Key)hotkey;
            set => hotkey = (int)value;
        }*/
        private Settings()
        {

        }
        public static void Load()
        {
            if (File.Exists("settings.xml"))
            {
                try
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(Settings));
                    using (FileStream fs = new FileStream("settings.xml", FileMode.OpenOrCreate))
                    {
                        Current = (Settings)formatter.Deserialize(fs);
                    }
                }
                catch
                {
                    File.Copy("settings.xml", "settings_errored" + Rand.Next() + ".xml");
                    Current = new Settings();
                    Save();
                }
            }
        }
        public static void Save()
        {
            Settings X = Current;
            XmlSerializer formatter = new XmlSerializer(typeof(Settings));
            if (File.Exists("settings.xml")) File.Delete("settings.xml");
            using (FileStream fs = new FileStream("settings.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, X);
            }
        }
    }
}
