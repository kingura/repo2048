using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _2048_WF
{

    class Number
    {
        public const int Width = 50;
        public const int Height = 50;
        public const byte Multiplier = 2;

        public int Value { get; private set; }

        // конструктор
        public Number()
        {
            Random rand = new Random();
            Value = rand.Next(1, 3) * Multiplier;
        }
    }
}
