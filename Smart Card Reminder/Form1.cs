using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Smart_Card_Reminder
{
    public partial class Form1 : Form
    {
        private static bool CardIsPresent = false;
        private const int checkinterval = 3000;
        public Form1()
        {          
            InitializeComponent();

            this.Visible = false;
            this.TopMost = true;
            this.SetDesktopLocation(Screen.PrimaryScreen.Bounds.Width - this.Size.Width, (int)(Screen.PrimaryScreen.Bounds.Height * 0.9) - this.Size.Height);
            y = this.pictureBox1.Location.Y;
            CheckCardTimer.Interval = checkinterval;
            CheckCardTimer.Start();           
        }
           
        // Startposition of the pictuebox
        private int y;
        // Endposition after the animation
        private int yEnd = 100;
        // Speed of animation (lower = faster) 
        private int speed = 20;

        /// <summary>
        /// This Method starts the timer for the picture animation
        /// </summary>
        private void startAnimation()
        {
            stopAnimation();
            ToEndAnimation.Interval = speed;
            ToStartAnimation.Interval = speed;
            ToEndAnimation.Start();
        }

        /// <summary>
        /// This method stops the animation
        /// </summary>
        private void stopAnimation()
        {
            ToEndAnimation.Stop();
            ToStartAnimation.Stop();
            this.pictureBox1.Location = new Point(pictureBox1.Location.X, y);
        }

        /// <summary>
        /// If the animation is in "down-phase" then every tick the picture Y-Cordinate += 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToEndAnimation_Tick(object sender, EventArgs e)
        {
            if (this.pictureBox1.Location.Y < yEnd)
            {
                pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y + 1);
            }
            else
            {
                //pictureBox1.Location = new Point(pictureBox1.Location.X, y);
                ToEndAnimation.Stop();
                ToStartAnimation.Start();
            }
        }

        /// <summary>
        /// If the animation is in "up-phase" then every tick the picture Y-Cordinate -= 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToStartAnimation_Tick(object sender, EventArgs e)
        {
            if (this.pictureBox1.Location.Y > y)
            {
                pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - 1);
            }
            else
            {
                ToStartAnimation.Stop();
                ToEndAnimation.Start();
            }
        }    
        /// <summary>
        /// Close Button background color if mouse enters zone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label1_MouseHover(object sender, EventArgs e)
        {
            label1.BackColor = Color.Red;
        }

        /// <summary>
        /// Close Button background color if mouse leaves
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label1_MouseLeave(object sender, EventArgs e)
        {
            label1.BackColor = Color.Transparent;
        }

        /// <summary>
        /// This method checks if a smartcard is present or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckCardTimer_Tick(object sender, EventArgs e)
        {
            if(Smartcard.CardIsPresent())
            {
                if (!remindActive)
                {
                    label2.ForeColor = Color.Crimson;
                    label2.Text = "Please remove your badge";
                    this.Visible = true;
                    Form1.CardIsPresent = true;
                    if(!(ToStartAnimation.Enabled || ToEndAnimation.Enabled))
                    {
                        startAnimation();
                    }
                    
                }
            }
            else
            {
                if(Form1.CardIsPresent)
                {
                    this.label3.Visible = false;
                    this.label4.Visible = false;
                    this.label5.Visible = false;
                    this.label6.Visible = false;
                    this.label7.Visible = false;
                    this.Visible = true;
                    stopAnimation();
                    label2.ForeColor = Color.DarkGreen;
                    label2.Text = "Badge removed ✔";
                    Form1.CardIsPresent = false;
                    reminder.Stop();
                    remindActive = false;
                    hideApp.Start();
                }
            }
        }
        /// <summary>
        ///  If the user snozed the remindActive is true
        /// </summary>
        private bool remindActive = false;

        /// <summary>
        /// 3h snoze if user clicks on Close button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            if (Form1.CardIsPresent)
            {
                reminder.Interval = 10800000;
                remindActive = true;
                reminder.Start();              
            }
            stopAnimation();
        }

        /// <summary>
        /// Basicly the remind method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reminder_Tick(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                this.Visible = true;
                startAnimation();
            }
            remindActive = false;
            reminder.Stop();
        }
        
        /// <summary>
        /// Method activates snoze labels and hides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HideApp_Tick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Visible = false;
                stopAnimation();
                this.label3.Visible = true;
                this.label4.Visible = true;
                this.label5.Visible = true;
                this.label6.Visible = true;
                this.label7.Visible = true;
            }
            hideApp.Stop();
        }
 
        /// <summary>
        /// Hides application OnLoad
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            base.OnLoad(e);
        }

        /// <summary>
        ///  Snooze 5 minutes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label4_Click(object sender, EventArgs e)
        {
             
            if (Form1.CardIsPresent)
            {
                this.Visible = false;
                stopAnimation();
                reminder.Interval = 300000;
                remindActive = true;
                reminder.Start();
            }
             
        }
        /// <summary>
        /// Snooze 60 minutes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label6_Click(object sender, EventArgs e)
        {
            if (Form1.CardIsPresent)
            {
                this.Visible = false;
                stopAnimation();
                reminder.Interval = 3600000;
                remindActive = true;
                reminder.Start();
            }
        }
        /// <summary>
        /// Snooze 15 minutes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label5_Click(object sender, EventArgs e)
        {
            if (Form1.CardIsPresent)
            {
                this.Visible = false;
                stopAnimation();
                reminder.Interval = 900000;
                remindActive = true;
                reminder.Start();
            }
        }
        /* Backgroundcolor for Snoze Buttons */
        private Color backcolor = Color.White;
        private void Label4_MouseHover(object sender, EventArgs e)
        {
            label4.BackColor = backcolor;
        }

        private void Label4_MouseLeave(object sender, EventArgs e)
        {
            label4.BackColor = Color.Transparent;
        }

        private void Label5_MouseEnter(object sender, EventArgs e)
        {
            label5.BackColor = backcolor;
        }

        private void Label5_MouseLeave(object sender, EventArgs e)
        {
            label5.BackColor = Color.Transparent;
        }

        private void Label6_MouseEnter(object sender, EventArgs e)
        {
            label6.BackColor = backcolor;
        }

        private void Label6_MouseLeave(object sender, EventArgs e)
        {
            label6.BackColor = Color.Transparent;
        }
        /// <summary>
        /// Snooze 30 minutes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label7_Click(object sender, EventArgs e)
        {
            if (Form1.CardIsPresent)
            {
                this.Visible = false;
                stopAnimation();
                reminder.Interval = 1800000;
                remindActive = true;
                reminder.Start();
            }
        }

        private void Label7_MouseEnter(object sender, EventArgs e)
        {
            label7.BackColor = backcolor;
        }

        private void Label7_MouseLeave(object sender, EventArgs e)
        {
            label7.BackColor = Color.Transparent;
        }
    }
}
