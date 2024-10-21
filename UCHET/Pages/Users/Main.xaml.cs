using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using UCHET.Models;
namespace UCHET.Pages.Users
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        List<UsersContext> AllUsers = UsersContext.Select();

        public Main()
        {
            InitializeComponent();

            if (MainWindow.init.ActiveUser.Role == "Пользователь")
            {
                parent.Children.Add(new Items.Item(AllUsers.Where(x => x.login == MainWindow.init.ActiveUser.login).First(), this));
                Add.Visibility = Visibility.Hidden;
            }
            else
            {
                foreach (UsersContext item in AllUsers)
                {
                    parent.Children.Add(new Items.Item(item, this));
                }
            }
        }

        private void AddRecord(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Users.Add());
        }

        private void SearchUser(object sender, KeyEventArgs e)
        {
            string search = tbSearch.Text;
            parent.Children.Clear();
            foreach (UsersContext classroom in AllUsers)
            {
                if (classroom.Surname.ToLower().Contains(search.ToLower()) || classroom.Name.ToLower().Contains(search.ToLower()))
                {
                    parent.Children.Add(new Items.Item(classroom, this));
                }
            }
        }
    }
}
