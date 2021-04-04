using Neural_Network.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Neural_Network
{
    public partial class Form1 : Form
    {
   
        Network Input = new Network();
        public static int Length_of_File = 0;
        public Form1()
        {
            InitializeComponent();          
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
           
            OpenFile();
        }

        public void OpenFile()
        {
            Input.Initialize_Weights();
         //   int[] GrabData = new int[48];
            Excel excel = new Excel(@"C:\Users\Nukph\Desktop\Data\LottoDatabase.xlsx", 1);
         
            do
            {
              
                for (int j = 0; j < 5; j++)
                {
                   
                    for (int k = 0; k < 48; k++)
                    {
                        
                        if (k.ToString() == excel.ReadCell(Length_of_File, j).ToString())
                            Input.InputGrab(k, Length_of_File);
                      //  else
                           // GrabData[k] = 0;
                    }
                }

                Length_of_File++;
            } while (excel.ReadCell(Length_of_File, 1) != "");



            // Format and display the TimeSpan value. 

            Input.ForwardNetwork();
        }
    }
}
