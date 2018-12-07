using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample_Timer
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer timer;
        TimeSpan countdownClock = TimeSpan.Zero;


        public Form1()
        {
            InitializeComponent();

            serialPort1.Open();
        }


        private void DisplayTime()
        {
            sec.Text = countdownClock.ToString(@"hh\:mm\:ss");
        }
        private void OnTimeEvent(object sender, EventArgs e)
        {
            // Subtract whatever our interval is from the countdownClock
            countdownClock = countdownClock.Subtract(TimeSpan.FromMilliseconds(timer.Interval));

            if (countdownClock.TotalMilliseconds <= 0)
            {
                // Countdown clock has run out, so set it to zero 
                // (in case it's negative), and stop our timer

                countdownClock = TimeSpan.Zero;
                timer.Stop();
                MessageBox.Show("YOUR TIME IS OVER!");
            }

            // Display the current time
            DisplayTime();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            timer = new System.Windows.Forms.Timer();
            timer.Interval = (int)TimeSpan.FromSeconds(1).TotalMilliseconds;
            timer.Tick += OnTimeEvent;
            DisplayTime();

        }

        private void AddTimeToClock(TimeSpan timeToAdd)
        {
            countdownClock += timeToAdd;

            // Display the new time
            DisplayTime();

            // Start the timer if it's stopped
            if (!timer.Enabled) timer.Start();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            TimeSpan.FromMinutes(5);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            button1.PerformClick();
        }
    }
}
