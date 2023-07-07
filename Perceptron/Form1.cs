using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perceptron
{
    public partial class Form1 : Form
    {
        List<BinaryOperations> ds = new List<BinaryOperations>();
        List<BinaryOperationsLearningSet> ls = new List<BinaryOperationsLearningSet>();
        public Form1()
        {
            InitializeComponent();
            learningSetInit();
            comboInit();
            comboBox1.DataSource = ds;
            comboBox1.DisplayMember = "name";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedValue.Equals("New function"))
            {

            }
            else
            {
                if (!checkBox3.Checked)
                {
                    LearnData(false, true, Convert.ToDouble(textBox14.Text), Convert.ToDouble(textBox15.Text), Convert.ToDouble(textBox16.Text),ls[comboBox1.SelectedIndex]);
                }
                else
                {
                    LearnData(true, true, 0, 0, 0, ls[comboBox1.SelectedIndex]);
                }
            }
            
            
        }

        public void learningSetInit()
        {

           ls.Add(new BinaryOperationsLearningSet() { input = new int[,] { { 1, 0 }, { 1, 1 }, { 0, 1 }, { 0, 0 } }, outputs = new int[4] { 0, 1, 0, 0 } });
           ls.Add(new BinaryOperationsLearningSet() { input = new int[,] { { 1, 0 }, { 1, 1 }, { 0, 1 }, { 0, 0 } }, outputs = new int[4] { 1, 0, 1, 0 } });
           ls.Add(new BinaryOperationsLearningSet() { input = new int[,] { { 1, 0 }, { 1, 1 }, { 0, 1 }, { 0, 0 } }, outputs = new int[4] { 1, 1, 1, 0 } });
           ls.Add(new BinaryOperationsLearningSet() { input = new int[,] { { 1, 0 }, { 1, 1 }, { 0, 1 }, { 0, 0 } }, outputs = new int[4] { 0, 0, 0, 1 } });
           ls.Add(new BinaryOperationsLearningSet() { input = new int[,] { { 1, 0 }, { 1, 1 }, { 0, 1 }, { 0, 0 } }, outputs = new int[4] { 0, 1, 0, 1 } });
           ls.Add(new BinaryOperationsLearningSet() { input = new int[,] { { 1, 0 }, { 1, 1 }, { 0, 1 }, { 0, 0 } }, outputs = new int[4] { 1, 0, 1, 1 } });

        }
        public void comboInit()
        {
            ds.Add(new BinaryOperations() { name = "AND", index = 0 });
            ds.Add(new BinaryOperations() { name = "XOR", index = 1 });
            ds.Add(new BinaryOperations() { name = "OR", index = 2 });
            ds.Add(new BinaryOperations() { name = "NOR", index = 3 });
            ds.Add(new BinaryOperations() { name = "XNOR", index = 4 });
            ds.Add(new BinaryOperations() { name = "NAND", index = 5 });
            ds.Add(new BinaryOperations() { name = "New function", index = 6 });
        }  
        public void LearnData(bool randomWeights, bool iszeroone, double w1, double w2, double w3, BinaryOperationsLearningSet opls)
        {
            double[] weights = { 0,0,0 };
            int ep = 0;
            double learningRate = 1; 
            double totalError = 1;
            string outp;

            switch (randomWeights)
            {
                case true:
                    Random r = new Random();
                    weights = new double[3] { r.NextDouble(), r.NextDouble(), r.NextDouble() };
                    break;
                case false:
                    weights = new double[3] { w1, w2, w3 };
                    break;
            }

            while (totalError > 0 && ep < 100)
            {
                totalError = 0;
                for (int i = 0; i < 4; i++)
                {
                    int output = calculateOutput(opls.input[i, 0], opls.input[i, 1], weights, iszeroone); 

                    int error = opls.outputs[i] - output; 

                    weights[0] += learningRate * error * opls.input[i, 0]; 
                    weights[1] += learningRate * error * opls.input[i, 1];
                    weights[2] += learningRate * error * 1;

                    totalError += Math.Abs(error);
                }
                ep++;
            }

            ///output do textboxa
            outp = ep.ToString() + "\n";
            for (int i = 0; i < 4; i++)
            {
                outp += "Dla " + opls.input[i, 0] + ", " + opls.input[i, 1] + " rezultat to: " + calculateOutput(opls.input[i, 0], opls.input[i, 1], weights, iszeroone) + "\n";
            }
            richTextBox1.Text = outp;
        }
        private static int calculateOutput(double input1, double input2, double[] weights, bool iszeroone) 
        {
            double sum = input1 * weights[0] + input2 * weights[1] + 1 * weights[2];
            if(iszeroone)
            {
                return (sum >= 0) ? 1 : 0; 
            }
            else
            {
                return (sum >= 0) ? 1 : -1; 
            }
            
        }
    }
}
