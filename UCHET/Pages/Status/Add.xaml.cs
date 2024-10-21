using System;
using System.Collections.Generic;
using System.Text;
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
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        /// <summary> 
        /// Данный статус
        /// </summary>
        StatusContext status;
        public Add(StatusContext status = null)
        {
            InitializeComponent();

            if (status != null)
            {
                this.status = status;
                Name.Text = status.Name;
                btnAdd.Content = "Изменить";
            }
        }

        private void AddRecord(object sender, RoutedEventArgs e)
        {

            if (Name.Text == "" || !Classes.Common.CheckRegex.Match("^[А-Яа-я]*$", Name.Text))
            {
                MessageBox.Show("Введите статус корректно");
                return;
            }

            if (status == null)
            {
                StatusContext newStatus = new StatusContext(
                    0,
                    Name.Text);
                newStatus.Add();
                MessageBox.Show("Запись добавлена");
            }
            else
            {
                status = new StatusContext(
                    status.Id,
                    Name.Text);
                status.Update();
                MessageBox.Show("Запись обновлена");
            }
            MainWindow.init.OpenPages(new Pages.Status.Main());
        }
    }
}
