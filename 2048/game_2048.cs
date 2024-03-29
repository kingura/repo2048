﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

internal class game_2048 : Form
{
  // TO DO
  // 1) Не двигать в сторону, если ни одной ячейке нет места для движения
  // 2) Добавить спецэффекты
  //      - перерисовывать не всю форму, а только где ченить менялось, иначе блымает. А лучше не перерисовывать,
  //        а только двигать и рисовать появление, а перерисовка только при перемещении
  // 3) Сохранение и возможность возврата на предыдущий ход
  // 4) Баг: складывается два раза если есть например: 16 2 2 4 -> 16 8 0 0
  // 5) Определить финальное победное событие и сообщение
  // 6) Проверка на конец игры должна быть в конце метода, сразу после появления новой ячейки,
  //    типа если рядом есть две одинаковые, то ок, а нет то не ок:)
  // 7) взять цвета со скриншота этой, пипеткой

  private const int size = 4;
  private const int fontSize = 24;
  private const int cellSize = 50;
  private const int borderSize = 2;
  private const int multiplier = 2;

  private readonly Color[] aColors =
  {
       /*0*/ Color.DarkGray,
       /*2*/ Color.Linen,
       /*4*/ Color.Bisque,
       /*8*/ Color.Coral,
      /*16*/ Color.Chocolate,
      /*32*/ Color.Brown,
      /*64*/ Color.DarkRed,
     /*128*/ Color.LightYellow,
     /*256*/ Color.GreenYellow,
     /*512*/ Color.Yellow,
    /*1024*/ Color.Orange,
    /*2048*/ Color.DarkOrange
  };

  int[,] arr = new int[size, size];
  Random rand = new Random();

  public static void Main()
  {
    Application.Run(new game_2048());
  }


  public game_2048()
  {
    SetStyle(ControlStyles.OptimizedDoubleBuffer |
             ControlStyles.AllPaintingInWmPaint |
             ControlStyles.UserPaint, true);
    UpdateStyles();

    Text = "2048";
    BackColor = Color.Gray; //SystemColors.Window;
    ForeColor = SystemColors.WindowText;
    //MaximizeBox = false;
    FormBorderStyle = FormBorderStyle.FixedSingle;
    ClientSize = new Size(size * cellSize, size * cellSize);
    
    ArrayInitializer();
  }


  void ArrayInitializer()
  {
    for (int i = 0; i < size; i++)
      for (int j = 0; j < size; j++)
        arr[i, j] = 0;

    InitNewCell();
    InitNewCell();
  }


  void InitNewCell()
  {
    // Проверяем, остались ли пустые ячейки
    bool isFull = true;
    foreach (int elem in arr)
      if (elem == 0)
        isFull = false;
    if (isFull)
    {
      MessageBox.Show("Игра окончена!\nНет свободных ячеек.");
      Close();
      return;
    }

    // Заполняем случайную непустую ячейку новым значением
    int cellNum;
    cellNum = rand.Next(arr.Length);


    while (arr[cellNum % size, cellNum / size] != 0)
      cellNum = rand.Next(arr.Length);

    // После каждого такого хода на случайной пустой клетке появляется новая плитка номинала «2» (с вероятностью 90%) или «4» (с вероятностью 10%)
    int rand_val = (rand.Next(1, 11) == 1) ? 2 : 1;
    int num = rand_val * multiplier;
    arr[cellNum % size, cellNum / size] = num;

    // прорисовка появления ячейки
    AppearanceAnimation(cellNum % size, cellNum / size);
  }


  protected override void OnKeyUp(KeyEventArgs e)
  {
    if (e.KeyCode == Keys.Left)
    {
      for (int y = 0; y < size; y++)
        for (int x = 1; x < size; x++)
        {
          int i = x;
          while ((i > 0) && (arr[i, y] != 0))
          {
            if (arr[i - 1, y] == 0)
            {
              arr[i - 1, y] = arr[i, y];
              arr[i, y] = 0;
            }
            else if (arr[i - 1, y] == arr[i, y])
            {
              arr[i - 1, y] = arr[i - 1, y] + arr[i, y];
              arr[i, y] = 0;
              break;
            }
            else
              break;

            i--;
          }
        }
    }
    else if (e.KeyCode == Keys.Right)
    {
      for (int y = 0; y < size; y++)
        for (int x = size - 2; x >= 0; x--)
        {
          int i = x;
          while ((i < size - 1) && (arr[i, y] != 0))
          {
            if (arr[i + 1, y] == 0)
            {
              arr[i + 1, y] = arr[i, y];
              arr[i, y] = 0;
            }
            else if (arr[i + 1, y] == arr[i, y])
            {
              arr[i + 1, y] = arr[i + 1, y] + arr[i, y];
              arr[i, y] = 0;
              break;
            }
            else
              break;

            i++;
          }
        }
    }
    else if (e.KeyCode == Keys.Up)
    {
      for (int x = 0; x < size; x++)
        for (int y = 1; y < size; y++)
        {
          int i = y;
          while ((i > 0) && (arr[x, i] != 0))
          {
            if (arr[x, i - 1] == 0)
            {
              arr[x, i - 1] = arr[x, i];
              arr[x, i] = 0;
            }
            else if (arr[x, i - 1] == arr[x, i])
            {
              arr[x, i - 1] = arr[x, i - 1] + arr[x, i];
              arr[x, i] = 0;
              break;
            }
            else
              break;

            i--;
          }
        }
    }
    else if (e.KeyCode == Keys.Down)
    {
      for (int x = 0; x < size; x++)
        for (int y = size - 2; y >= 0; y--)
        {
          int i = y;
          while ((i < size - 1) && (arr[x, i] != 0))
          {
            if (arr[x, i + 1] == 0)
            {
              arr[x, i + 1] = arr[x, i];
              arr[x, i] = 0;
            }
            else if (arr[x, i + 1] == arr[x, i])
            {
              arr[x, i + 1] = arr[x, i + 1] + arr[x, i];
              arr[x, i] = 0;
              break;
            }
            else
              break;

            i++;
          }
        }
    }

    if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
    {
      this.Refresh();
      InitNewCell();
      Invalidate();
    }
  }

  void AppearanceAnimation(int x, int y)
  {
    Graphics grfx = CreateGraphics();
    Brush txtBrush = new SolidBrush(BackColor);
    Brush backBrush = new SolidBrush(GetColor(arr[x, y]));
    Font font;

    StringFormat strfmt = new StringFormat();
    strfmt.LineAlignment = strfmt.Alignment = StringAlignment.Center;

    // Рисуем маленький -> средний -> большой -> средний квадратики

    // очистка
    grfx.FillRectangle(new SolidBrush(BackColor), new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize));

    int factor;
    for (int i = -6; i <= 2; i++)
    {
      factor = Math.Abs(i) - 1;

      // очистка при рисовании фигуры меньше предыдущей
      if (i > 0)
        grfx.FillRectangle(new SolidBrush(BackColor),
                           new Rectangle(x * cellSize - borderSize, y * cellSize - borderSize,
                                         cellSize + 2 * borderSize, cellSize + 2 * borderSize));

      // квадрат
      grfx.FillRectangle(backBrush,
                         new Rectangle((x * cellSize) + borderSize * factor,
                                       (y * cellSize) + borderSize * factor,
                                       cellSize - 2 * borderSize * factor,
                                       cellSize - 2 * borderSize * factor));
      // цифра
      font = new Font(Font.FontFamily, fontSize + borderSize * (1 - factor));
      grfx.DrawString(arr[x, y].ToString(), font, txtBrush,
          new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize), strfmt);

      Thread.Sleep(20);
    }
  }

  private Color GetColor(int number)
  {
    if (number == 0)
      return aColors[0];

    int i = 1;
    while (Math.Pow(multiplier, i) != number)
      i++;

    return aColors[i];
  }

  Font GetFontWithOptimalSize(Graphics grfx, int number)
  {
    float fSize = fontSize;
    Font font = new Font(Font.FontFamily, fSize);
    while (grfx.MeasureString(number.ToString(), font).Width > cellSize)
    {
      fSize--;
      font = new Font(Font.FontFamily, fSize);
    }
    return font;
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    Graphics grfx = e.Graphics;
    Brush txtBrush = new SolidBrush(BackColor);
    Brush backBrush;// = new SolidBrush(Color.LightGray);
    Font font;// = new Font(Font.FontFamily, fontSize);

    StringFormat strfmt = new StringFormat();
    strfmt.LineAlignment = strfmt.Alignment = StringAlignment.Center;

    for (int i = 0; i < size; i++)
      for (int j = 0; j < size; j++)
      {
        backBrush = new SolidBrush(GetColor(arr[i, j]));
        font = GetFontWithOptimalSize(grfx, arr[i, j]);

        grfx.FillRectangle(backBrush, new Rectangle((i * cellSize) + borderSize, (j * cellSize) + borderSize,
                                                    cellSize - 2 * borderSize, cellSize - 2 * borderSize));
        if (arr[i, j] != 0)
          grfx.DrawString(arr[i, j].ToString(), font, txtBrush,
              new Rectangle(i * cellSize, j * cellSize, cellSize, cellSize), strfmt);
      }
  }
}

