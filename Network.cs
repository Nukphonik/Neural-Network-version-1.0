using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neural_Network.Properties;


namespace Neural_Network.Properties
{
   public class Network
    {
        double collect_error = 0 ;
        double error = 0;
        double total_error = 0;
        int epochs = 10000;
        double learning_Rate = 0.5;
        double[] OUt_weight_error_correction = new double[5];
        double[] Hidden_weight_error_correction = new double[10];
        double[] SigmaOut = new double[5];
        double[] SigmaHidden = new double[10];
        double target = 0.99;
        double bias = 0.03;
        int Input = 48;
        int Hidden = 10;
        int Output = 5;
        public static int[,] InputLayer = new int[48,350];
        double[] HiddenLayer = new double[10]; 
        double[] OutputLayer = new double[5];  
        double[,] Entry_Hidden_Weights = new double[48, 10];
        double[,] Hidden_Output_Weights = new double[5, 10];

        public void Initialize_Weights()
        {
        //    MessageBox.Show("Initialize_Weights   ");
            Random rand = new Random();
            double Randweight = 0;
            double InputRandweight = 0;
            for (int counter = 0; counter < Form1.Length_of_File; counter++)
            {
                for (int count = 0; count < Input; count++)
                {
                    InputLayer[count, counter] = 0;
                    
                }
            }

            // Initialize for the incoming weights 
            for (int j = 0; j < Input; j++)
            { 
                for (int i = 0; i < Hidden; i++)
                {
                    InputRandweight = rand.Next(-9, 9);
                    Entry_Hidden_Weights[j, i] = InputRandweight/100;
                    HiddenLayer[i] = 0;
                    SigmaHidden[i] = 0;
                }
            }
            // Initialize for the hidden output weights
            for (int j = 0; j < Output; j++)
            {
                OutputLayer[j] = 0;
                SigmaOut[j] = 0;
                for (int i = 0; i < Hidden; i++)
                {
                    Randweight = rand.Next(1, 9);
                    Hidden_Output_Weights[j, i] = Randweight / 100;
                }
            }
            
        }

        public void InputGrab(int i , int j)
        {
            InputLayer[i, j] = 1;            
        }

        public void ResetNeurons()
        {
            for (int i = 0; i < Output; i++)
            {
                OutputLayer[i] = 0;
                for (int j = 0; j < Hidden; j++)
                {
                    HiddenLayer[j] = 0;
                }
            }
        }
        public void Backprogation(int k)
        {
            for(int i = 0; i < Hidden; i++)
            {
                for(int j = 0; j < Output; j++)
                {
                    Hidden_Output_Weights[j, i] = Hidden_Output_Weights[j, i] - ( learning_Rate* (SigmaOut[j] * HiddenLayer[i]));
                }
            }
            for (int a = 0; a < Input; a++)
            {
                for(int b = 0; b < Hidden; b++)
                {
                    Entry_Hidden_Weights[a, b] = Entry_Hidden_Weights[a, b] + (learning_Rate * (SigmaHidden[b] * InputLayer[a,k]));
                }
            }
            
        }


        public void ForwardNetwork()
        {
            int z = 0;
            double collect = 0;
            MessageBox.Show("ForwardNetwork    ");
            do
            {

                for (int k = 0; k < Form1.Length_of_File; k++)
                {
                    collect = 0;
                    for (int i = 0; i < Hidden; i++)
                    {
                        for (int j = 0; j < Input; j++)
                        {
                            HiddenLayer[i] = InputLayer[j, k] * Entry_Hidden_Weights[j, i] + HiddenLayer[i];
                        }

                        if (i == Hidden)
                            HiddenLayer[i] = Math.Sin(target - (HiddenLayer[i] * bias));
                        else
                            HiddenLayer[i] = Math.Sin(target - (HiddenLayer[i]));
                        SigmaHidden[i] = -(target - HiddenLayer[i]) * HiddenLayer[i] * (1 - HiddenLayer[i]);
                    }
                    for (int count = 0; count < Output; count++)
                    {
                        for (int counter = 0; counter < Hidden; counter++)
                        {
                            OutputLayer[count] = HiddenLayer[counter] * Hidden_Output_Weights[count, counter] + OutputLayer[count];
                        }
                        if (count == Output)
                            OutputLayer[count] = Math.Sin(target - (OutputLayer[count] * bias));
                        else
                            OutputLayer[count] = Math.Sin(target - (OutputLayer[count]));
                        error = 0.5 * ((target - OutputLayer[count]) * (target - OutputLayer[count]));
                        total_error = error + total_error;
                        error = 0;

                        SigmaOut[count] = -(target - OutputLayer[count]) * OutputLayer[count] * (1 - OutputLayer[count]);
                        collect = OutputLayer[count] + collect;
                    }

                    Backprogation(k);
                    ResetNeurons();
                    
                    
                }
              //  MessageBox.Show("Total Error is     " + total_error.ToString());
                collect_error = total_error;
                total_error = 0;
                z = z + 1;
            } while (z < epochs);
            MessageBox.Show("Total Error is     " +(collect_error/ Form1.Length_of_File).ToString());

        }
    }
}


