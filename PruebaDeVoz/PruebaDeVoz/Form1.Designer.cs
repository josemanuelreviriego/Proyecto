namespace PruebaDeVoz
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.grabar = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.salir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 13);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(621, 191);
            this.textBox1.TabIndex = 0;
            // 
            // grabar
            // 
            this.grabar.Location = new System.Drawing.Point(53, 226);
            this.grabar.Name = "grabar";
            this.grabar.Size = new System.Drawing.Size(143, 23);
            this.grabar.TabIndex = 1;
            this.grabar.Text = "Grabar";
            this.grabar.UseVisualStyleBackColor = true;
            this.grabar.Click += new System.EventHandler(this.grabar_Click);
            // 
            // stop
            // 
            this.stop.Location = new System.Drawing.Point(202, 226);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(143, 23);
            this.stop.TabIndex = 2;
            this.stop.Text = "Stop";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // salir
            // 
            this.salir.Location = new System.Drawing.Point(490, 226);
            this.salir.Name = "salir";
            this.salir.Size = new System.Drawing.Size(143, 23);
            this.salir.TabIndex = 3;
            this.salir.Text = "Salir";
            this.salir.UseVisualStyleBackColor = true;
            this.salir.Click += new System.EventHandler(this.salir_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 270);
            this.Controls.Add(this.salir);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.grabar);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button grabar;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.Button salir;
    }
}

