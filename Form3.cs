using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using System.IO;
using System.Globalization;


namespace WindowsFormsApp1
{
    /// <summary>
    /// search for correct emp
    ///     hannel not found
    ///     show found
    /// 
    /// 
    /// enter ==> 
    ///     check if not entered today
    ///     save record of entry
    ///     show message succses
    ///    
    /// leave ==> 
    ///     check if entered today
    ///     retrive record of today
    ///     overite record with time_out  , total normal hours , total over hours
    ///     
    /// </summary>
    public partial class Form3 : Form
    {

        public Elements current_emp;
        
        List<Elements> records;
        List<EmlpyeeAttend> EmpRecords;
        List<String> Names = new List<string>();
        List<String> Codes = new List<string>() ;
        Mydate mydate;
        string directpath;
        bool FormType_isenter;
        int Standard_hour = 8;

        public Form3(bool enter)
        {
            InitializeComponent();
            init();

            FormType_isenter = enter;
        }

        private void init()
        {
            directpath = @"D:\projects\attendance el batal\WindowsFormsApp1\data\emps\";
            string Filename = "D:/projects/attendance el batal/WindowsFormsApp1/data/emps.csv";
            using (var reader = new StreamReader(Filename))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<Elements>().ToList();
            }
            for (int i = 0; i < records.Count; i++)
            {
                Names.Add(records[i].Name); 
                Codes.Add(records[i].Code);
            }

            Names.Add("");
            Codes.Add("");

            comboBox2.DataSource = Codes;

            comboBox2.SelectedIndex = Names.Count - 1;
          



        }
        private void ClearSearch()
        {
            comboBox2.SelectedIndex = Names.Count - 1;
        }
        private void comboBox2_Enter(object sender, EventArgs e)
        {
            //ClearSearch();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // بحث
            string code = search();
            int number;
      

            bool success = int.TryParse(code, out number);
            if (success)
            {
               Elements el =  getinfo(code);
                if(el != null)
                {
                    current_emp = el;
                    richTextBox1.Text = el.Name + "  " + el.Code;
                    which_option();
                }
                else
                {
                    richTextBox1.Text = "حدث خطا 2";
                }
            }
            else
            {
               richTextBox1.Text = code;
            }
        }

        private string search()
        {
           
            string found_code="";

    

            if (comboBox2.SelectedIndex != Names.Count - 1)
            {
                found_code = comboBox2.Text;

                for (int i = 0; i < records.Count; i++)
                {
                    if (found_code == records[i].Code)
                    {
                        return found_code;
                    }
                }
                return "الكود غير صحيح";
            }


            return "حدث خطا";
       
        }
   
        private Elements getinfo(string code)
        {
            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].Code == code)
                {
                    return records[i];
                }
            }
            return null;
        }

        private void which_option()
        {
           

            string Filename = directpath+current_emp.Code+".csv";


            using (var reader = new StreamReader(Filename))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                EmpRecords = csv.GetRecords<EmlpyeeAttend>().ToList();
            }
            mydate = new Mydate();
            bool entered = false;
            for (int i = 0; i < EmpRecords.Count; i++)
            {
                if (EmpRecords[i].date == mydate.date && EmpRecords[i].time_in!="" && EmpRecords[i].time_out == "")
                {
                    entered = true;
                }
            }
          
            if (entered)
            {
                ChickOutEmp();
              
            }
            else
            {

                ChickInEmp();
            }

        }



        private void ChickInEmp()
        {
            // enter 
            string Filename = directpath + current_emp.Code + ".csv";

            mydate = new Mydate();

            string clientDetails = string.Format("{0},{1},{2}", mydate.date, mydate.time, "");


            File.AppendAllText(Filename, clientDetails);

            richTextBox2.Text = "IN";
        }

        private void ChickOutEmp()
        {
            // leave
            TimeSpan interval = new TimeSpan(5, 00, 00); // 02:14:15

            string Filename = directpath + current_emp.Code + ".csv";
            var csv = new StringBuilder();
            var newLine = "date,time_in,time_out";
            csv.AppendLine(newLine);

            bool succses_out=false;

            for (int i = 0; i < EmpRecords.Count; i++)
            {
                // wrirte every record
                // but only modify today
                EmlpyeeAttend r = EmpRecords[i];

                newLine = string.Format("{0},{1},{2}", r.date,r.time_in, r.time_out);

                if (EmpRecords[i].date == mydate.date && EmpRecords[i].time_in != "" && EmpRecords[i].time_out=="")
                {
                    mydate = new Mydate();

                    //DateTime start = DateTime.ParseExact(EmpRecords[i].time_in, "HH:mm",
                    //                    CultureInfo.InvariantCulture);
                    //DateTime timenow = DateTime.ParseExact(mydate.time, "HH:mm",
                    //                   CultureInfo.InvariantCulture);
                    //TimeSpan diff1 = timenow - start;
                    //double h = diff1.TotalHours;
                    //int totalhours = (int)h;

                    //int over = 0;
                    //int normal = 0;
                    //if(totalhours > Standard_hour)
                    //{
                    //    over = totalhours - Standard_hour;
                    //    normal = Standard_hour;
                    //}
                    //else
                    //{
                    //    normal = totalhours;
                    //}


                    newLine = string.Format("{0},{1},{2}", r.date, r.time_in,mydate.time);
                    succses_out = true;
                }

                csv.AppendLine(newLine);
            }



            File.WriteAllText(Filename, csv.ToString());

            if (succses_out)
            {
                richTextBox2.Text = "OUT";
            }
            else
            {
                richTextBox2.Text = "العميل له حدور";
            }
          


        }

















        private void button4_Click(object sender, EventArgs e)
        {
            DateTime endofday = DateTime.Today.AddHours(17);
            mydate = new Mydate();
            DateTime trydate = DateTime.Today.AddHours(16);

            TimeSpan diff1 = trydate - endofday;
            TimeSpan diff2 = endofday - trydate;


            MessageBox.Show(" " + diff1 );
            //MessageBox.Show(" " + diff2 );


            if(diff1.TotalHours>0)
            {
                MessageBox.Show("over "+diff1.TotalHours);
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
