using System;
using System.Windows.Forms;


namespace L8_Task_3
{

    //  Анатолий Толстых

    // Задание 3 (методичка)
    //а) Создать приложение, показанное на уроке, добавив в него защиту от возможных ошибок(не создана база данных, обращение к несуществующему вопросу, открытие слишком большого файла и т.д.).
    //б) Изменить интерфейс программы, увеличив шрифт, поменяв цвет элементов и добавив другие «косметические» улучшения на свое усмотрение.
    //в) Добавить в приложение меню «О программе» с информацией о программе(автор, версия, авторские права и др.).
    //г)* Добавить пункт меню Save As, в котором можно выбрать имя для сохранения базы данных(элемент SaveFileDialog).

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // База данных с вопросами
        TrueFalse database;
       
        // Обработчик пункта меню Exit
        private void miExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Обработчик пункта меню New
        private void miNew_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                database = new TrueFalse(sfd.FileName);
                database.Add("123", true);
                database.Save();
                nudNumber.Minimum = 1;
                nudNumber.Maximum = 1;
                nudNumber.Value = 1;
            };
        }

        // Обработчик события изменения значения numericUpDown
        private void nudNumber_ValueChanged(object sender, EventArgs e)
        {
            tboxQuestion.Text = database[(int)nudNumber.Value - 1].Text;
            cboxTrue.Checked = database[(int)nudNumber.Value - 1].TrueFalse;
        }

        // Обработчик кнопки Добавить
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (database == null)
            {
                MessageBox.Show("Создайте новую базу данных", "Сообщение");
                return;
            }
            database.Add((database.Count + 1).ToString(), true);
            nudNumber.Maximum = database.Count;
            nudNumber.Value = database.Count;
        }

        // Обработчик кнопки Удалить
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var alertDialog = MessageBox.Show("Вопрос будет удален, вы уверены?", "Опасносте!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (alertDialog == DialogResult.No)
            {
                return;
            }

            if (nudNumber.Maximum == 1 || database == null) return;
            database.Remove((int)nudNumber.Value - 1); //доработка: код, указанный в методичке, удалял следующий элемент базы, а не текущий. Поправлено.
            nudNumber.Maximum--;
            if (nudNumber.Value > 1) nudNumber.Value = nudNumber.Value;
            nudNumber_ValueChanged(sender, e); //доработка: вызываем событие, чтобы отразить следующий за удаленным элемент
        }

        // Обработчик пункта меню Save
        private void miSave_Click(object sender, EventArgs e)
        {
            if (database != null) database.Save();
            else MessageBox.Show("База данных не создана");
        }

        // Обработчик пункта меню Open
        private void miOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                database = new TrueFalse(ofd.FileName);
                database.Load();
                nudNumber.Minimum = 1;
                nudNumber.Maximum = database.Count;
                nudNumber.Value = 1;
            }
        }

        // Обработчик кнопки Сохранить (вопрос)
        private void btnSaveQuest_Click(object sender, EventArgs e)
        {
            database[(int)nudNumber.Value - 1].Text = tboxQuestion.Text;
            database[(int)nudNumber.Value - 1].TrueFalse = cboxTrue.Checked;
        }

        // Обработчик кнопки Сохранить как
        private void miSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                database.FileName = sfd.FileName;
                database.Save();
            };
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Знания полезные, спасибо за обучение. \n" +
                "Параллельно пишу на С# свой, отдельный проект. Почти в каждом уроке " +
                "проясняются некоторые моменты, упущенные при самостоятельном изучении, появляются новые способы доработки. " +
                "Возможно, в курсе не хватает изучения многопоточности, хотя, видимо, это С#-2..\n\n" +
                "А вот ДЗ часто однообразны и скучны, уж извините. " +
                "Куда приятнее применить новые навыки в своём проекте, чем в абстрактной \"Верю-Не верю\". " +
                "Поэтому ДЗ сделано скорее для галочки \"усвоил\", всё остальное ушло за его рамки.");
        }
    }
}
