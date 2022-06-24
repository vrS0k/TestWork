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
using System.Windows.Input;

namespace Reverse_Polish_Notation
{
     partial class Form1 : Form
     {

        public Form1()
        {
            InitializeComponent();
            InitialExpression.KeyDown += InitialExpression_KeyDown;
        }
   
        private void InitialExpression_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Calculate();
        }

        private void Calculate()
        {
            if (InitialExpression.Text == string.Empty)
                MessageBox.Show("Введите значения!");
            else
            {     
                if (CheckInput.IsCorrectBracketsAndOperators(InitialExpression.Text))
                {
                    ResultOfCounting ResultOrException = RpnAlgorithm.Calculate(InitialExpression.Text);
                    if (ResultOrException.IsCorrectInput)
                    {     
                        Result.Text = Math.Round(ResultOrException.Value, 3,
                        MidpointRounding.AwayFromZero).ToString();
                        RpnExpression.Text = ResultOrException.Expression.Replace('~', '-');
                    }
                    else
                    {
                        MessageBox.Show("Выражение содержит ошибку!");
                    }
                }
                else             
                {
                    MessageBox.Show("Выражение содержит ошибку!");
                }

            }
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            Calculate();    
        }

        private void BtnInput_Click(object sender, EventArgs e)
        {
            Button Input = (Button)sender;
            InitialExpression.Text += Input.Text;
        }
     }      
}

