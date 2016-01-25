using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


namespace _2048_WF
{

    class Number
    {
        public int Value { get; private set; }

        // конструктор остается "по умолчанию"
        public Number()
        {
            Value = 0;
        }

        // функция рандомного числа
        public void NewRandNum(byte multiplier)
        {
            //Правила:
            //После каждого хода на случайной пустой клетке появляется новая плитка
            //номинала «2» (с вероятностью 90%) или «4» (с вероятностью 10%).
            Random rand = new Random();
            int randVal = rand.Next(1, 101);
            Value = randVal * multiplier;
            if (randVal >= 91)
            {
                Value = Value*2;
            }
        }
    }
}
