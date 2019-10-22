using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace L8_Task_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static PropertyInfo GetPropertyInfo(object obj, string str)
        {
            return obj.GetType().GetProperty(str);
        }


        private void button1_Click(object sender, EventArgs e)
        {

            tableDateTimeStruct.Controls.Clear(); //очищаем таблицу перед заполнением

            var d = DateTime.Now;

            var properties = d.GetType().GetProperties();

            
            AddTextToTab("Имя свойства:", 0, 0);  //заполняем шапку таблицы
            AddTextToTab("Тип свойства:", 1, 0);
            AddTextToTab("Значение свойства:", 2, 0);
            AddTextToTab("Чтение:", 3, 0);
            AddTextToTab("Запись:", 4, 0);


            var row = 1;

            foreach (var item in properties)
            {
                var col = 0;

                AddTextToTab(item.Name, col, row);
                

                col++;
                AddTextToTab(GetPropertyInfo(d, item.Name).PropertyType.Name, col, row);
               
                col++;
                AddTextToTab(GetPropertyInfo(d, item.Name).GetValue(d).ToString(), col, row);
        
                col++;
                AddTextToTab(GetPropertyInfo(d, item.Name).CanRead.ToString(), col, row);
                
                col++;
                AddTextToTab(GetPropertyInfo(d, item.Name).CanWrite.ToString(), col, row);
              
                row++;

            }
        }

        /// <summary>
        /// Добавляет тектовый блок в таблицу по указанным строке и колонке.
        /// </summary>
        /// <param name="text">Текст блока</param>
        /// <param name="col">Колонка</param>
        /// <param name="row">Строка</param>
        private void AddTextToTab(string text, int col, int row)
        {
            var labelText = new Label
            {
                Dock = DockStyle.Fill,
                Text = text
            };

            tableDateTimeStruct.Controls.Add(labelText, col, row);
        }
    }
}
