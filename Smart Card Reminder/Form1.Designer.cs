namespace Smart_Card_Reminder
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ToEndAnimation = new System.Windows.Forms.Timer(this.components);
            this.ToStartAnimation = new System.Windows.Forms.Timer(this.components);
            this.CheckCardTimer = new System.Windows.Forms.Timer(this.components);
            this.reminder = new System.Windows.Forms.Timer(this.components);
            this.hideApp = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(304, -1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            this.label1.MouseLeave += new System.EventHandler(this.Label1_MouseLeave);
            this.label1.MouseHover += new System.EventHandler(this.Label1_MouseHover);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(166, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(161, 126);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Crimson;
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(262, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "Please remove your badge";
            this.label2.Click += new System.EventHandler(this.Label2_Click);
            // 
            // ToEndAnimation
            // 
            this.ToEndAnimation.Tick += new System.EventHandler(this.ToEndAnimation_Tick);
            // 
            // ToStartAnimation
            // 
            this.ToStartAnimation.Tick += new System.EventHandler(this.ToStartAnimation_Tick);
            // 
            // CheckCardTimer
            // 
            this.CheckCardTimer.Interval = 5000;
            this.CheckCardTimer.Tick += new System.EventHandler(this.CheckCardTimer_Tick);
            // 
            // reminder
            // 
            this.reminder.Interval = 300000;
            this.reminder.Tick += new System.EventHandler(this.Reminder_Tick);
            // 
            // hideApp
            // 
            this.hideApp.Interval = 5000;
            this.hideApp.Tick += new System.EventHandler(this.HideApp_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "Snooze:";
            this.label3.Click += new System.EventHandler(this.Label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(33, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 22);
            this.label4.TabIndex = 4;
            this.label4.Text = "5 Minutes";
            this.label4.Click += new System.EventHandler(this.Label4_Click);
            this.label4.MouseLeave += new System.EventHandler(this.Label4_MouseLeave);
            this.label4.MouseHover += new System.EventHandler(this.Label4_MouseHover);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(33, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 22);
            this.label5.TabIndex = 5;
            this.label5.Text = "15 Minutes";
            this.label5.Click += new System.EventHandler(this.Label5_Click);
            this.label5.MouseEnter += new System.EventHandler(this.Label5_MouseEnter);
            this.label5.MouseLeave += new System.EventHandler(this.Label5_MouseLeave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(33, 158);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 22);
            this.label6.TabIndex = 6;
            this.label6.Text = "1 Hour";
            this.label6.Click += new System.EventHandler(this.Label6_Click);
            this.label6.MouseEnter += new System.EventHandler(this.Label6_MouseEnter);
            this.label6.MouseLeave += new System.EventHandler(this.Label6_MouseLeave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(33, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 22);
            this.label7.TabIndex = 7;
            this.label7.Text = "30 Minutes";
            this.label7.Click += new System.EventHandler(this.Label7_Click);
            this.label7.MouseEnter += new System.EventHandler(this.Label7_MouseEnter);
            this.label7.MouseLeave += new System.EventHandler(this.Label7_MouseLeave);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(324, 199);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(1, 1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Smartcard";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer ToEndAnimation;
        private System.Windows.Forms.Timer ToStartAnimation;
        private System.Windows.Forms.Timer CheckCardTimer;
        private System.Windows.Forms.Timer reminder;
        private System.Windows.Forms.Timer hideApp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}

