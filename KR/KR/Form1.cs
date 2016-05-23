using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KR
{
    public partial class Form1 : Form
    {
        int cliks = 0;

        int ran = (new Random()).Next();

        public Form1()
        {
            InitializeComponent();

            radioButton1.Checked = true;

            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            Click += Form1_Click;

            textBox1.Text = "3567";
            timer1.Tick += Timer1_Tick;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            cliks = 0;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if(cliks == 0)
            {
                timer1.Enabled = true;
            }
            cliks++;
            if (cliks == 2)
            {
                if(timer1.Enabled == true)
                {
                    textBox1.Text = "3";
                    cliks = 0;
                    timer1.Enabled = false;
                }

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) return;



            if (ran == (int)(comboBox1.SelectedItem))
                this.BackColor = Color.Yellow;
            else this.BackColor = Control.DefaultBackColor;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ran = (new Random()).Next();
            if (radioButton1.Checked)
                ran %= (Convert.ToInt32(textBox1.Text) + 1);
            else
            { ran %= Convert.ToInt32(textBox1.Text); ran++; }

            comboBox1.Items.Clear();
            int begin = 1;
            int end = Convert.ToInt32(textBox1.Text);
            if (radioButton1.Checked) begin = 0;

            for(; begin <= end; begin++)
            {
                comboBox1.Items.Add(begin);
            }
        }

    }
}
