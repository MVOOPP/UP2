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

namespace UCHET.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authentication.xaml
    /// </summary>
    public partial class Authentication : Page
    {
        public List<LoginContext> allLogins = LoginContext.Select();
        public List<UsersContext> allUsers = UsersContext.Select();

        public Authentication()
        {
            InitializeComponent();
        }

        private void OpenGeneral(object sender, RoutedEventArgs e)
        {
            Authorization();
        }

        public void Authorization()
        {
            if (TbLogin.Text == "" || !Classes.Common.CheckRegex.Match("^[А-Яа-яA-Za-z]*$", TbLogin.Text))
            {
                MessageBox.Show("Необходимо правильно указать логин (без цифр)");
                return;
            }

            LoginContext login = allLogins.Find(x => x.Name == TbLogin.Text && x.Password == TbPassword.Password);
            UsersContext user = allUsers.Find(x => x.login == TbLogin.Text && x.Password == TbPassword.Password);

            if (login != null)
            {
                if (login.Id == 1)
                {
                    MessageBox.Show("Добро пожаловать, админ!");
                    MainWindow.init.OpenPages(new Pages.Equipment.Main());
                }
            }

            if (user != null)
            {
                if (user.login == TbLogin.Text)
                {
                    MessageBox.Show("Добро пожаловать, " + $"{this.TbLogin.Text}");
                    MainWindow.init.ActiveUser = user;
                    MainWindow.init.OpenPages(new Pages.Equipment.Main());
                }
            }

            if (login == null && user == null)
            {
                MessageBox.Show("Не заполнены некоторые поля или введены неправильные данные");
            }
        }
    }
}
