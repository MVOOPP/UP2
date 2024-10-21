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

namespace UCHET.Pages.Status.Items
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        List<StatusContext> AllStatuses = StatusContext.Select();
        List<EquipmentContext> AllEquipment = EquipmentContext.Select();
        StatusContext item;
        Main main;

        public Item(StatusContext item, Main main)
        {
            InitializeComponent();
            if (MainWindow.init.ActiveUser.Role == "Пользователь")
            {
                Edit.Visibility = Visibility.Hidden;
                Delete.Visibility = Visibility.Hidden;
            }
            Name.Text = item.Name;
            this.item = item;
            this.main = main;
        }

        private void EditRecord(object sender, RoutedEventArgs e) =>
            MainWindow.init.OpenPages(new Pages.Status.Add(this.item));

        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            bool empty = false;
            foreach (EquipmentContext equip in AllEquipment)
            {
                if (equip.Id_status == item.Id) empty = true;
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
    }
}
