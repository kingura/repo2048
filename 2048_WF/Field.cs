using System;
using System.Drawing;

//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace _2048_WF
{
    class Field
    {
        // константы
        public const int DefaultDimension = 4;
        public const int CellWidth = 50;
        public const int CellHeight = 50;
        public const byte CellMultiplier = 2;

        // свойства
        public int Dimension { get; private set; }
        //public int[,] ArrayGrid;
        public Number[,] ArrayGrid;

        // конструкторы
        public Field(int dim)
        {
            Dimension = dim;
            //ArrayGrid = new int[Dimension, Dimension];
            ArrayGrid = new Number[Dimension, Dimension];


            // обнуляем/заполняем массив
            for (int i = 0; i < ArrayGrid.GetLength(0); i++)
            {
                for (int j = 0; j < ArrayGrid.GetLength(1); j++)
                {
                    ArrayGrid[i, j] = new Number();
                }
            }

            // обнуляем штатным методом; не работает! все в null, наверное срабатывает конструктор типа object
            //ArrayGrid.Initialize();

            // нарисовать
            //Draw(MainForm);

        }
        // конструктор без параметров
        public Field() : this(DefaultDimension) { }


        // методы
        public void Draw(System.Windows.Forms.Form form)
        {
            const int rectLeftPoint = 50;
            const int rectTopPoint = 50;
            const int rectRightPoint = 200;
            const int rectBottomPoint = 200;

            System.Drawing.Graphics formGraphics = form.CreateGraphics();
            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
            formGraphics.DrawRectangle(myPen,
                new System.Drawing.Rectangle(rectLeftPoint, rectTopPoint, rectRightPoint, rectBottomPoint));

            // отрисовка массива
            Font myFont = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point);

            for (int i = 0; i < ArrayGrid.GetLength(0); i++)
            {
                for (int j = 0; j < ArrayGrid.GetLength(1); j++)
                {
                    formGraphics.DrawString(ArrayGrid[i, j].Value.ToString(), myFont, Brushes.Blue,
                        new PointF(rectLeftPoint + CellWidth*i + (CellWidth - (int) myFont.SizeInPoints)/2,
                            rectTopPoint + CellHeight*j + (CellHeight - (int) myFont.SizeInPoints)/2));
                }
            }

            // очищаем экземпляры
            myPen.Dispose();
            formGraphics.Dispose();
        }
    }
}
