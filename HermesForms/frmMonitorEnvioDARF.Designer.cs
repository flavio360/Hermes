namespace HermesForms
{
    partial class frmMonitorEnvioDARF
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnIniciarServico = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnIniciarServico
            // 
            this.btnIniciarServico.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnIniciarServico.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnIniciarServico.Location = new System.Drawing.Point(136, 133);
            this.btnIniciarServico.Name = "btnIniciarServico";
            this.btnIniciarServico.Size = new System.Drawing.Size(141, 38);
            this.btnIniciarServico.TabIndex = 0;
            this.btnIniciarServico.Text = "Iniciar serviço";
            this.btnIniciarServico.UseVisualStyleBackColor = false;
            this.btnIniciarServico.Click += new System.EventHandler(this.btnIniciarServico_Click);
            // 
            // frmMonitorEnvioDARF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 238);
            this.Controls.Add(this.btnIniciarServico);
            this.Name = "frmMonitorEnvioDARF";
            this.Text = "Envio pedidos Airlink x Interlog";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnIniciarServico;
    }
}