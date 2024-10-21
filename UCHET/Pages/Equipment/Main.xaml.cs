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

namespace UCHET.Pages.Equipment
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        List<EquipmentContext> AllEquipments = EquipmentContext.Select();
        List<UsersContext> AllUsers = UsersContext.Select();

        public Main()
        {
            InitializeComponent();

            if (MainWindow.init.ActiveUser.Role == "Пользователь")
            {
                foreach (EquipmentContext item in AllEquipments)
                {
                    if (item.Id_user == MainWindow.init.ActiveUser.Id)
                        parent.Children.Add(new Items.Item(item, this));
                }
                Add.Visibility = Visibility.Hidden;
            }
            else
            {
                foreach (EquipmentContext item in AllEquipments)
                {
                    parent.Children.Add(new Items.Item(item, this));
                }
            }

            MainWindow.init.equipment.IsEnabled = true;
            MainWindow.init.users.IsEnabled = true;
            MainWindow.init.classrooms.IsEnabled = true;
            MainWindow.init.status.IsEnabled = true;
        }

        private void AddRecord(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Equipment.Add());
        }

        private void SearchEquip(object sender, KeyEventArgs e)
        {
            string search = tbSearch.Text;
            parent.Children.Clear();
            foreach (EquipmentContext classroom in AllEquipments)
            {
                if (classroom.Name.ToLower().Contains(search.ToLower()))
                {
                    parent.Children.Add(new Items.Item(classroom, this));
                }
            }
        }
    }
}
