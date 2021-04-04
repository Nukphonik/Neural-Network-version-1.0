using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;


namespace Neural_Network.Properties
{
    public partial class Excel : Control
    {
        
        string path = "";
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;
        Network Input = new Network();
        public Excel(string path,int Sheet)
        {
            InitializeComponent();
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[Sheet];            
        }
       
       

        public string ReadCell(int i, int j)
        {
            
            i++;
            j++;
            
            if (ws.Cells[i, j].Value2 != null)
            {
                
                return ws.Cells[i, j].Value2.ToString();
            }
                
            else
                return ""; 
        }
     
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
        
        
    }
}
