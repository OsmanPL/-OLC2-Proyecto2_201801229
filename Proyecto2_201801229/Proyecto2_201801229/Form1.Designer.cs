
namespace Proyecto2_201801229
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Codigo = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Consola = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Reportes = new System.Windows.Forms.Button();
            this.Optimizar = new System.Windows.Forms.Button();
            this.Compilar = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Codigo
            // 
            this.Codigo.Location = new System.Drawing.Point(52, 12);
            this.Codigo.Name = "Codigo";
            this.Codigo.Size = new System.Drawing.Size(635, 288);
            this.Codigo.TabIndex = 0;
            this.Codigo.Tag = "";
            this.Codigo.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 288);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(12, 345);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(34, 145);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            // 
            // Consola
            // 
            this.Consola.BackColor = System.Drawing.Color.Black;
            this.Consola.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Consola.ForeColor = System.Drawing.Color.White;
            this.Consola.Location = new System.Drawing.Point(52, 345);
            this.Consola.Name = "Consola";
            this.Consola.Size = new System.Drawing.Size(635, 145);
            this.Consola.TabIndex = 9;
            this.Consola.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Reportes);
            this.groupBox1.Controls.Add(this.Optimizar);
            this.groupBox1.Controls.Add(this.Compilar);
            this.groupBox1.Location = new System.Drawing.Point(728, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 478);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Funciones";
            // 
            // Reportes
            // 
            this.Reportes.Location = new System.Drawing.Point(6, 365);
            this.Reportes.Name = "Reportes";
            this.Reportes.Size = new System.Drawing.Size(216, 74);
            this.Reportes.TabIndex = 2;
            this.Reportes.Text = "Reportes";
            this.Reportes.UseVisualStyleBackColor = true;
            // 
            // Optimizar
            // 
            this.Optimizar.Location = new System.Drawing.Point(6, 187);
            this.Optimizar.Name = "Optimizar";
            this.Optimizar.Size = new System.Drawing.Size(216, 74);
            this.Optimizar.TabIndex = 1;
            this.Optimizar.Text = "Optimizar";
            this.Optimizar.UseVisualStyleBackColor = true;
            this.Optimizar.Click += new System.EventHandler(this.button1_Click);
            // 
            // Compilar
            // 
            this.Compilar.Location = new System.Drawing.Point(6, 43);
            this.Compilar.Name = "Compilar";
            this.Compilar.Size = new System.Drawing.Size(216, 74);
            this.Compilar.TabIndex = 0;
            this.Compilar.Text = "Compilar";
            this.Compilar.UseVisualStyleBackColor = true;
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 502);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.Consola);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Codigo);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compi Pascal";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox Codigo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.RichTextBox Consola;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Reportes;
        private System.Windows.Forms.Button Optimizar;
        private System.Windows.Forms.Button Compilar;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer1;
    }
}

