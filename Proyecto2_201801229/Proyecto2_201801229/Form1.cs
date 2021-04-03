using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto2_201801229
{
    public partial class Form1 : Form
    {
        int caracter;
        int caracter2;
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 10;
            timer1.Start();
            timer2.Interval = 10;
            timer2.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            caracter = 0;
            int altura = Codigo.GetPositionFromCharIndex(0).Y;
            if (Codigo.Lines.Length > 0)
            {
                for (int i = 0; i < Codigo.Lines.Length; i++)
                {
                    e.Graphics.DrawString((i + 1).ToString(), Codigo.Font, Brushes.Blue, pictureBox1.Width - (e.Graphics.MeasureString((i + 1).ToString(), Codigo.Font).Width + 10), altura);
                    caracter += Codigo.Lines[i].Length + 1;
                    altura = Codigo.GetPositionFromCharIndex(caracter).Y;
                }
            }
            else
            {
                e.Graphics.DrawString((1).ToString(), Codigo.Font, Brushes.Blue, pictureBox1.Width - (e.Graphics.MeasureString((1).ToString(), Codigo.Font).Width + 10), altura);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBox2.Refresh();
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            caracter2 = 0;
            int altura = Consola.GetPositionFromCharIndex(0).Y;
            if (Consola.Lines.Length > 0)
            {
                for (int i = 0; i < Consola.Lines.Length; i++)
                {
                    e.Graphics.DrawString((i + 1).ToString(), Consola.Font, Brushes.Blue, pictureBox1.Width - (e.Graphics.MeasureString((i + 1).ToString(), Consola.Font).Width + 10), altura);
                    caracter2 += Consola.Lines[i].Length + 1;
                    altura = Consola.GetPositionFromCharIndex(caracter2).Y;
                }
            }
            else
            {
                e.Graphics.DrawString((1).ToString(), Consola.Font, Brushes.Blue, pictureBox1.Width - (e.Graphics.MeasureString((1).ToString(), Consola.Font).Width + 10), altura);
            }
        }
    }
}
