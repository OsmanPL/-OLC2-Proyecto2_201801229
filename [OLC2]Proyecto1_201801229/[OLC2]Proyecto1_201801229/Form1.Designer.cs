
namespace _OLC2_Proyecto1_201801229
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
            Consola = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Ejecutar = new System.Windows.Forms.Button();
            this.Reportes = new System.Windows.Forms.Button();
            this.Cargar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // Codigo
            // 
            this.Codigo.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Codigo.Location = new System.Drawing.Point(52, 29);
            this.Codigo.Name = "Codigo";
            this.Codigo.Size = new System.Drawing.Size(437, 302);
            this.Codigo.TabIndex = 0;
            this.Codigo.Text = "";
            // 
            // Consola
            // 
            Consola.BackColor = System.Drawing.Color.Black;
            Consola.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Consola.ForeColor = System.Drawing.Color.White;
            Consola.Location = new System.Drawing.Point(602, 29);
            Consola.Name = "Consola";
            Consola.Size = new System.Drawing.Size(437, 302);
            Consola.TabIndex = 2;
            Consola.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Codigo";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(602, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Consola";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Ejecutar);
            this.groupBox1.Controls.Add(this.Reportes);
            this.groupBox1.Controls.Add(this.Cargar);
            this.groupBox1.Location = new System.Drawing.Point(12, 357);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1027, 146);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Acciones";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // Ejecutar
            // 
            this.Ejecutar.Location = new System.Drawing.Point(789, 57);
            this.Ejecutar.Name = "Ejecutar";
            this.Ejecutar.Size = new System.Drawing.Size(232, 52);
            this.Ejecutar.TabIndex = 14;
            this.Ejecutar.Text = "Ejecutar";
            this.Ejecutar.UseVisualStyleBackColor = true;
            this.Ejecutar.Click += new System.EventHandler(this.Ejecutar_Click);
            // 
            // Reportes
            // 
            this.Reportes.Location = new System.Drawing.Point(408, 57);
            this.Reportes.Name = "Reportes";
            this.Reportes.Size = new System.Drawing.Size(232, 52);
            this.Reportes.TabIndex = 12;
            this.Reportes.Text = "Reportes";
            this.Reportes.UseVisualStyleBackColor = true;
            this.Reportes.Click += new System.EventHandler(this.Reportes_Click);
            // 
            // Cargar
            // 
            this.Cargar.Location = new System.Drawing.Point(6, 57);
            this.Cargar.Name = "Cargar";
            this.Cargar.Size = new System.Drawing.Size(232, 52);
            this.Cargar.TabIndex = 11;
            this.Cargar.Text = "Cargar Archivo";
            this.Cargar.UseVisualStyleBackColor = true;
            this.Cargar.Click += new System.EventHandler(this.Cargar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 302);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint_1);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(562, 29);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(34, 302);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 516);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(Consola);
            this.Controls.Add(this.Codigo);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compi Pascal";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox Codigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Ejecutar;
        private System.Windows.Forms.Button Reportes;
        private System.Windows.Forms.Button Cargar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Timer timer2;
        public static System.Windows.Forms.RichTextBox Consola;
    }
}

