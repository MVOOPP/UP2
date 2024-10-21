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


namespace UCHET.Pages.Users.Items
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        List<RolesContext> AllRoles = RolesContext.Select();
        List<EquipmentContext> AllEquipment = EquipmentContext.Select();
        UsersContext item;
        Main main;

        public Item(UsersContext item, Main main)
        {
            InitializeComponent();

            if (MainWindow.init.ActiveUser.Role == "Пользователь")
            {
                Delete.Visibility = Visibility.Hidden;
                MainWindow.init.status.IsEnabled = false;
            }

            Login.Text = item.login;
            Password.Password = item.Password;
            role.Text = AllRoles.Find(x => x.roles == item.Role).roles;
            email.Text = item.Email;
            Surname.Text = item.Surname;
            Name.Text = item.Name;
            Patronymic.Text = item.Patronymic;
            Telephone.Text = item.Phone;

            this.item = item;
            this.main = main;
        }

        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            bool empty = false;
            foreach (EquipmentContext equip in AllEquipment)
            {
                if (equip.Id_user == item.Id) empty = true;
            }

            if (empty)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить данную запись? При ее удалении пропадут данные об оборудовании с таким статусом", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    item.Delete();
                    main.parent.Children.Remove(this);
                }
            }
            else
            {
                item.Delete();
                main.parent.Children.Remove(this);
            }
        }

        private void EditRecord(object sender, RoutedEventArgs e) =>
            MainWindow.init.OpenPages(new Pages.Users.Add(this.item));
    }
}
