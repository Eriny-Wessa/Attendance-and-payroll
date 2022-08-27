using System.Windows.Forms;
using CsvHelper;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        List<Elements> records;
        string[] filePaths ;
        List<string> filename = new List<string>();
        public Form2()
        {
            InitializeComponent();
           
            string Filename = "D:/projects/attendance el batal/WindowsFormsApp1/data/emps.csv";

            
            using (var reader = new StreamReader(Filename))
            using (var csv = new CsvReader(reader,CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<Elements>().ToList();
            }

            init_datagrive();
            // dataGridView1.DataSource = records;
            magic_conn();
            textBox1.Text = "اسم جديد";
            textBox2.Text = "الساعة العادية";
        }

        private void init_datagrive()
        {
            dataGridView1.Rows.Clear();
           // dataGridView1.Rows.Add("code","اسم","ساعة عادية");

            for (int i = 0; i < records.Count; i++)
            {
                dataGridView1.Rows.Add(records[i].Code, records[i].Name, records[i].Hour_normal);

            }
        }


        private void button1_Click(object sender, System.EventArgs e)
        {
            // save sure??
            DialogResult dialogResult = MessageBox.Show("Sure", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                save_new_empfile();
                magic_conn();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

         
        }
        private void save_new_empfile()
        {
            var csv = new StringBuilder();
            var newLine = "Code,Name,Hour_normal";
            csv.AppendLine(newLine);


            for (int i = 0; i < records.Count; i++)
            {
                Elements r = records[i];
                newLine = string.Format("{0},{1},{2}", r.Code, r.Name,r.Hour_normal);

                csv.AppendLine(newLine);
            }



            string path = @"D:\projects\attendance el batal\WindowsFormsApp1\data\" + "emps.csv";
            File.WriteAllText(path, csv.ToString());
        }
        private void dgvUserList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(e.RowIndex.ToString());
        }

        private void label2_Click(object sender, System.EventArgs e)
        {

        }

      


        private void magic_conn()
        {
            filePaths = Directory.GetFiles(@"D:\projects\attendance el batal\WindowsFormsApp1\data\emps");

            for (int i = 0; i < filePaths.Length; i++)
            {
                filename.Add(Path.GetFileName(filePaths[i]));
            }


            for (int i = 0; i < records.Count; i++)
            {
                string code = records[i].Code;
                if (!check_emp_exist(code))
                {
                    add_emp(records[i]);
                }
            }

        }

        private bool check_emp_exist(string code)
        {
            return filename.Contains(code+".csv") || filename.Contains(code + ".xls") || filename.Contains(code + ".xlsx");
        }

        private void add_emp(Elements Newemp)
        {
            /// new file called code.csv
            /// date, time_in , time out , Hours_normal , Hours_over
            var csv = new StringBuilder();
            var newLine = "date,time_in,time_out";
            csv.AppendLine(newLine);


            //newLine = string.Format("{0},{1}", first, second);
            //csv.AppendLine(newLine);
            string path = @"D:\projects\attendance el batal\WindowsFormsApp1\data\emps\" + Newemp.Code+".csv";
            File.WriteAllText(path, csv.ToString());
        }


        private void button3_Click(object sender, EventArgs e) // add new
        {
            float value;
            if (! float.TryParse(textBox2.Text, out value))
            {
                textBox2.Text = " ليس رقم ";
                return;
            }


            Elements Newemp = new Elements();
            Newemp.Name = textBox1.Text;
            Newemp.Hour_normal = textBox2.Text;

            Random rand = new Random();
            int number = rand.Next(100, 999);

            while(seen_before(number.ToString()))
            {
                number = rand.Next(100, 999);
            }


            Newemp.Code = number.ToString();
            records.Add(Newemp);
            dataGridView1.Rows.Add(Newemp.Code, Newemp.Name, Newemp.Hour_normal);
        }


        private bool seen_before(string code)
        {
            for (int i = 0; i < records.Count; i++)
            {
                if (code== records[i].Code)
                {
                    return true;
                }

            }

            return false;
        }




        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "اسم جديد")
            {
                textBox1.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "اسم جديد";
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "الساعة العادية" || textBox2.Text == " ليس رقم ")
            {
                textBox2.Text = "";
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "الساعة العادية";
            }
        }


        public DateTime fromdate;
        public DateTime todate;
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            fromdate= dateTimePicker1.Value;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            todate = dateTimePicker2.Value;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            // make report
        }
    }
}
