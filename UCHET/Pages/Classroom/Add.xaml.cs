using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UCHET.Classes;

namespace UCHET.Pages.Classroom
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        ClassroomContext classroom;
        public Add(ClassroomContext classroom = null)
        {
            InitializeComponent();
            if (classroom != null)
            {
                this.classroom = classroom;
                Name.Text = classroom.Name;
                Code.Text = classroom.Code;
                btnAdd.Content = "Изменить";
            }
        }
        private void AddRecord(object sender, RoutedEventArgs e)
        {
            if (Code.Text == "" || !Classes.Common.CheckRegex.Match("^[А-Яа-я]{1}[0-9]{3}||[0-9]{2}$", Code.Text))
            {
                MessageBox.Show("Необходимо указать шифр аудитории");
                return;
            }
            if (classroom == null)
            {
                ClassroomContext newClassroom = new ClassroomContext(
                    0,
                    Name.Text,
                    Code.Text
                    );
                newClassroom.Add();
                MessageBox.Show("Запись добавлена");
            }
            else
            {
                classroom = new ClassroomContext(
                    classroom.Id,
                    Name.Text,
                    Code.Text
                    );
                classroom.Update();
                MessageBox.Show("Запись обновлена");
            }
            MainWindow.init.OpenPages(new Pages.Classroom.Main());
        }
    }
}
