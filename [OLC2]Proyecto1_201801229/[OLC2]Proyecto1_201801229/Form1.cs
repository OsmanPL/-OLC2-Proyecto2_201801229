using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _OLC2_Proyecto1_201801229.Analizador;

namespace _OLC2_Proyecto1_201801229
{
    public partial class Form1 : Form
    {

        GeneradorAST ejecutar = new GeneradorAST();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Cargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var sr = new StreamReader(openFileDialog1.FileName);
                    Codigo.Text=sr.ReadToEnd();
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void Ejecutar_Click(object sender, EventArgs e)
        {
            Consola.Text = "";
            ejecutar.analizar(Codigo.Text);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Reportes_Click(object sender, EventArgs e)
        {

            Reporte reporte = new Reporte();
            if (ejecutar.retornarRaiz()!=null)
            {
                reporte.graficarArbol(ejecutar.retornarRaiz());
                reporte.HTML_ts(GeneradorAST.tablaCompleta);
            }

            reporte.Html_Errores(GeneradorAST.listaErrores);
        }

        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
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

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
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

        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBox2.Refresh();
        }
    }
}
