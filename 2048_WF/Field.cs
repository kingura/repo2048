using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_WF
{
    class Field
    {
        // константы
        public const int default_dimension = 4;


        // свойства
        public int Dimension { get; private set; }
        public int[,] arrayGrid;

        // конструкторы
        public Field(int dim)
        {
            Dimension = dim;
            arrayGrid = new int[Dimension, Dimension];
            // обнуляем массив
            for (int i = 0; i < arrayGrid.GetLength(0); i++)
            {
                for (int j = 0; j < arrayGrid.GetLength(1); j++)
                {
                    arrayGrid[i, j] = 0;
                }
            }

            // нарисовать
            //Draw(MainForm);

        }
        public Field() : this(default_dimension) { }


        // методы
        public void Draw(System.Windows.Forms.Form form)
        {
            System.Drawing.Graphics formGraphics = form.CreateGraphics();
            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
            formGraphics.DrawRectangle(myPen, new System.Drawing.Rectangle(50, 50, 200, 200));

            // очищаем экземпляры
            myPen.Dispose();
            formGraphics.Dispose();
        }
    }
}
