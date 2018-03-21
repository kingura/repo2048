using System;
using System.Drawing;
using System.Windows.Forms;

class game_2048 : Form
{
  // TO DO
  // 1) Не двигать в сторону, если ни одной ячейке нет места для движения
  // 2) Добавить изменение цвета и размера чисел
  // 3) Добавить спецэффекты
  // 4) Правила: После каждого такого хода на случайной пустой клетке появляется новая плитка номинала «2» (с вероятностью 90%) или «4» (с вероятностью 10%).
  // 5) Сохранение и возможность возврата на предыдущий ход

  const int size = 4;
  const int fontSize = 24;
  const int cellSize = 50;
  const int borderSize = 2;
  const int multiplier = 2;

  int[,] arr = new int[size, size];
  Random rand = new Random();

  public static void Main()
  {
    Application.Run(new game_2048());
  }


  public game_2048()
  {
    Text = "2048";
    BackColor = SystemColors.Window;
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
    }

    // Заполняем случайную непустую ячейку новым значением
    int cellNum;
    cellNum = rand.Next(arr.Length);

    while (arr[cellNum % size, cellNum / size] != 0)
      cellNum = rand.Next(arr.Length);

    int num = rand.Next(1, 3) * multiplier;
    arr[cellNum % size, cellNum / size] = num;
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
      InitNewCell();
      Invalidate();
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
      InitNewCell();
      Invalidate();
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
      InitNewCell();
      Invalidate();
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
      InitNewCell();
      Invalidate();
    }
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    Graphics grfx = e.Graphics;
    Brush txtBrush = new SolidBrush(Color.White);
    Brush backBrush = new SolidBrush(Color.LightGray);
    Font font = new Font(Font.FontFamily, fontSize);

    StringFormat strfmt = new StringFormat();
    strfmt.LineAlignment = strfmt.Alignment = StringAlignment.Center;

    for (int i = 0; i < size; i++)
      for (int j = 0; j < size; j++)
      {
        grfx.FillRectangle(backBrush, new Rectangle((i * cellSize) + borderSize, (j * cellSize) + borderSize,
                                                    cellSize - 2 * borderSize, cellSize - 2 * borderSize));
        if (arr[i, j] != 0)
          grfx.DrawString(arr[i, j].ToString(), font, txtBrush,
              new Rectangle(i * cellSize, j * cellSize, cellSize, cellSize), strfmt);
      }
  }
}

