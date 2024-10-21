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
using UCHET.Models;


namespace UCHET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow init;
        public Users ActiveUser = new Users();
        public MainWindow()
        {
            InitializeComponent();
            OpenPages(new Pages.Authentication());
            init = this;
        }

        public void OpenPages(Page Page)
        {
            frame.Navigate(Page);
        }

        private void OpenEquipment(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Equipment.Main());
        }

        private void OpenUsers(object sender, RoutedEventArgs e)
        {
            //переход на страницу пользователей
            MainWindow.init.OpenPages(new Pages.Users.Main());
        }
        private void OpenClassrooms(object sender, RoutedEventArgs e)
        {
            //переход на страницу аудиторий
            MainWindow.init.OpenPages(new Pages.Classroom.Main());
        }
        private void OpenStatus(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Status.Main());
        }
    }
}