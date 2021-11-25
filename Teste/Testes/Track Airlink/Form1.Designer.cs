namespace Teste
{
    partial class Form1
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
            this.btnTrackAirlink = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSplit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTrackAirlink
            // 
            this.btnTrackAirlink.Location = new System.Drawing.Point(28, 24);
            this.btnTrackAirlink.Name = "btnTrackAirlink";
            this.btnTrackAirlink.Size = new System.Drawing.Size(183, 23);
            this.btnTrackAirlink.TabIndex = 0;
            this.btnTrackAirlink.Text = "Teste Track Airlink";
            this.btnTrackAirlink.UseVisualStyleBackColor = true;
            this.btnTrackAirlink.Click += new System.EventHandler(this.btnTrackAirlink_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(28, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Conecta DB AIRLINK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSplit
            // 
            this.btnSplit.Location = new System.Drawing.Point(28, 83);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(183, 23);
            this.btnSplit.TabIndex = 2;
            this.btnSplit.Text = "Split log genérico";
            this.btnSplit.UseVisualStyleBackColor = true;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 450);
            this.Controls.Add(this.btnSplit);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnTrackAirlink);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTrackAirlink;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSplit;
    }
}

