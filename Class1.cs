using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO; // This namespace usage is important or else TextFieldParser method will lead to error
using System.IO;
using System.Data;

namespace WindowsFormsApp1
{
    public class Elements
    {
        public string Code  { get; set; }
        public string Name { get; set; }

        public string Hour_normal { get; set; }

    }
}
