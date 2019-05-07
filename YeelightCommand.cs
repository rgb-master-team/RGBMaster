using System;
using System.Collections.Generic;

namespace chroma_yeelight
{
    [Serializable]
    internal class YeelightCommand
    {
        public YeelightCommand()
        {
        }

        public int id { get; set; }
        public string method { get; set; }
        public List<object> @params { get; set; }
    }
}