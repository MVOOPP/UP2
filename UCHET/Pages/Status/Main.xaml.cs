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

namespace UCHET.Pages.Status
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        List<StatusContext> AllStatuses = StatusContext.Select();

        public Main()
        {
            InitializeComponent();

            foreach (StatusContext item in AllStatuses)
            {
                parent.Children.Add(new Items.Item(item, this));
            }

            if (MainWindow.init.ActiveUser.Role == "Пользователь")
            {
                Add.Visibility = Visibility.Hidden;
            }
        }

        private void AddRecord(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Status.Add());
        }

        private void SearchStatus(object sender, KeyEventArgs e)
        {
            string search = tbSearch.Text;
            parent.Children.Clear();
            foreach (StatusContext classroom in AllStatuses)
            {
                if (classroom.Name.ToLower().Contains(search.ToLower()))
                {
                    parent.Children.Add(new Items.Item(classroom, this));
                }
            }
        }
    }
}
