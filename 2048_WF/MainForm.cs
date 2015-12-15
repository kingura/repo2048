using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048_WF
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Number numb = new Number();
            button1.Text = numb.Value.ToString();

            Field field = new Field();
            field.Draw(this);



            //int[,] arr = new int[2, 2];
            //arr[0, 0] = 1;
            //arr[1, 1] = 2;
            ////button1.Text = arr[1, 1].ToString();
            //button1.Text = arr.Length.ToString();
        }
    }
}
