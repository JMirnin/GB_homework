using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L8_Task_2
{
    //      Анатолий Толстых
    //    Задание 2 (методичка)
    //    Создайте простую форму на котором свяжите свойство Text элемента TextBox со свойством Value элемента NumericUpDown


    //  (Вроде правильно понял - при изменеии значения одного элемента, менять значение второго. Не совсем ясно, как это относится к теме)

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            textBox.Text = numericUpDown.Value.ToString();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox.Text, out int value))
            {
                numericUpDown.Value = value;
                textBox.BackColor = SystemColors.Window;
            }
            else
            {
                textBox.BackColor = Color.Red;
            }
        }
    }
}
