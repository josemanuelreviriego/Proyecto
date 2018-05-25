namespace FormFinal
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnEmpezar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtURL1 = new System.Windows.Forms.TextBox();
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.txtURL2 = new System.Windows.Forms.TextBox();
            this.txtURL3 = new System.Windows.Forms.TextBox();
            this.txtURL4 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnEmpezar
            // 
            this.btnEmpezar.Location = new System.Drawing.Point(139, 77);
            this.btnEmpezar.Name = "btnEmpezar";
            this.btnEmpezar.Size = new System.Drawing.Size(75, 23);
            this.btnEmpezar.TabIndex = 0;
            this.btnEmpezar.Text = "Empezar";
            this.btnEmpezar.UseVisualStyleBackColor = true;
            this.btnEmpezar.Click += new System.EventHandler(this.btnEmpezar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Introduce la IP de tu controlador:";
            // 
            // txtURL1
            // 
            this.txtURL1.Location = new System.Drawing.Point(61, 51);
            this.txtURL1.MaxLength = 3;
            this.txtURL1.Name = "txtURL1";
            this.txtURL1.Size = new System.Drawing.Size(33, 20);
            this.txtURL1.TabIndex = 2;
            this.txtURL1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtURL1.TextChanged += new System.EventHandler(this.txtURL_TextChanged);
            // 
            // txtTexto
            // 
            this.txtTexto.Enabled = false;
            this.txtTexto.Location = new System.Drawing.Point(43, 121);
            this.txtTexto.Multiline = true;
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.ReadOnly = true;
            this.txtTexto.Size = new System.Drawing.Size(171, 23);
            this.txtTexto.TabIndex = 3;
            // 
            // txtURL2
            // 
            this.txtURL2.Location = new System.Drawing.Point(100, 51);
            this.txtURL2.MaxLength = 3;
            this.txtURL2.Name = "txtURL2";
            this.txtURL2.Size = new System.Drawing.Size(33, 20);
            this.txtURL2.TabIndex = 4;
            this.txtURL2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtURL2.TextChanged += new System.EventHandler(this.txtURL_TextChanged);
            // 
            // txtURL3
            // 
            this.txtURL3.Location = new System.Drawing.Point(139, 51);
            this.txtURL3.MaxLength = 3;
            this.txtURL3.Name = "txtURL3";
            this.txtURL3.Size = new System.Drawing.Size(33, 20);
            this.txtURL3.TabIndex = 5;
            this.txtURL3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtURL3.TextChanged += new System.EventHandler(this.txtURL_TextChanged);
            // 
            // txtURL4
            // 
            this.txtURL4.Location = new System.Drawing.Point(181, 51);
            this.txtURL4.MaxLength = 3;
            this.txtURL4.Name = "txtURL4";
            this.txtURL4.Size = new System.Drawing.Size(33, 20);
            this.txtURL4.TabIndex = 6;
            this.txtURL4.TabStop = false;
            this.txtURL4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtURL4.TextChanged += new System.EventHandler(this.txtURL_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 157);
            this.Controls.Add(this.txtURL4);
            this.Controls.Add(this.txtURL3);
            this.Controls.Add(this.txtURL2);
            this.Controls.Add(this.txtTexto);
            this.Controls.Add(this.txtURL1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEmpezar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Programa de voz";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEmpezar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtURL1;
        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.TextBox txtURL2;
        private System.Windows.Forms.TextBox txtURL3;
        private System.Windows.Forms.TextBox txtURL4;
    }
}

