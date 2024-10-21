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

namespace UCHET.Pages.Roles.Items
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        List<RolesContext> AllRoles = RolesContext.Select();
        RolesContext item;
        Main main;
        public Item(RolesContext item, Main main)
        {
            InitializeComponent();
            Role.Text = item.roles;
            this.item = item;
            this.main = main;
        }
    }
}
