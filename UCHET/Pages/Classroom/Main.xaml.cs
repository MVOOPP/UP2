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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        List<ClassroomContext> AllClassrooms = ClassroomContext.Select();
        public Main()
        {
            InitializeComponent();
            if (MainWindow.init.ActiveUser.Role == "Пользователь")
            {
                Add.Visibility = Visibility.Hidden;
            }
            else
            {
                foreach (ClassroomContext item in AllClassrooms)
                {
                    parent.Children.Add(new Items.Item(item, this));
                }
            }
        }
        private void AddRecord(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Classroom.Add());
        }

        private void SearchClassroom(object sender, KeyEventArgs e)
        {
            string search = tbSearch.Text;
            parent.Children.Clear();
            foreach (ClassroomContext classroom in AllClassrooms)
            {
                if (classroom.Code.ToLower().Contains(search.ToLower()) || classroom.Name.ToLower().Contains(search.ToLower()))
                {
                    parent.Children.Add(new Items.Item(classroom, this));
                }
            }
        }
    }
}
