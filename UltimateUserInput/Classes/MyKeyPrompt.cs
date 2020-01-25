using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateUserInput
{
    class MyKeyPrompt
    {
        public MyKeyPromptTypes Type { get; set; }
        public string Title { get; set; }
        public object data;

        public MyKeyPrompt()
        {
            Type = MyKeyPromptTypes.Undefined;
            Title = "Новое неизвестное событие";
        }
        public MyKeyPrompt(MyKeyPromptTypes _Type,string _Title)
        {
            Type = _Type;
            Title = _Title;
        }
    }

    enum MyKeyPromptTypes
    {
        Undefined = -1,
        Mouse = 0,
        Keyboard = 1,
        Delay = 2
    }
}
