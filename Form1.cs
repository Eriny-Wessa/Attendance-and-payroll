using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public string name;
        public string pass;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void Name_box_TextChanged(object sender, EventArgs e)
        {
            name = Name_box.Text;
            this.label3.Visible = false;
            this.label4.Visible = false;
        }

        private void Pass_box_TextChanged(object sender, EventArgs e)
        {
            pass = Pass_box.Text;
            this.label3.Visible = false;
            this.label4.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // admin log in check
            if(name != null && name.Length > 0 && pass != null && pass.Length > 0)
            {
                bool granted = false;
                string filePath = "D:/projects/attendance el batal/WindowsFormsApp1/data/login.csv";
                StreamReader streamReader = new StreamReader(filePath);
                string [] row = streamReader.ReadLine().Split(',');
                while (!streamReader.EndOfStream)
                {
                    row = streamReader.ReadLine().Split(',');
                    if (isuser(row))
                    {
                        granted = true;
                        break;
                    }
                }
                if (granted)
                {
                    
                    Form2 f2 = new Form2();
                    this.Hide();
                    f2.ShowDialog();
                  
                }
                else
                {
                    this.label4.Visible = true;
                }
            }
            else
            {
                this.label3.Visible = true;
            }
        }


        bool isuser(string[] row)
        {
            if (row[0]== name && row[1]== pass)
            {
                return true;
            }
            return false;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3= new Form3();
            this.Hide();
            f3.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
           // this.Hide();
            f2.ShowDialog();
            //this.Close();
        }
    }
}
