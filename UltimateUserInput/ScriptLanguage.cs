using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace UltimateUserInput
{
    class ScriptLanguage
    {
        static public void RunScript(string script)
        {
            string[] functions = script.Split('\n');
            foreach (string command in functions)
            {
                RunCommand(command.Trim());
            }
        }

        static public void RunCommand(string command)
        {
            string[] param = command.Split(' ');
            switch (param[0])
            {
                case "WAIT":
                    Thread.Sleep(int.Parse(param[1]));
                    break;
                case "MOUSE":
                    switch (param[1])
                    {
                        case "CLICK":
                            UserInput.MouseButton Click;
                            UserInput.MouseButton.TryParse(param[2], out Click);
                            UserInput.MouseClick(Click);
                            break;
                    }
                    break;
            }
        }
        
    }
}
