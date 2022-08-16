using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class Mydate
    {
        public DateTime dateTime { get; set; }
        public string date { get; set; }
        public string time { get; set; }

        public Mydate()
        {
            DateTime today = DateTime.Today;

            dateTime = DateTime.Now;


            date = today.ToString("dd/MM/yyyy");
            time = dateTime.ToString("HH:mm");
        }
    }
}
