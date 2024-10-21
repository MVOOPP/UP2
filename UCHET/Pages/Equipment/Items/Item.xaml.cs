using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.IO;
using Microsoft.Win32;
using System.Windows.Media.Animation;
using Imaging = Aspose.Imaging;
using System.Diagnostics;
using UCHET.Classes;
using System;
using System.Collections.Generic;

namespace UCHET.Pages.Equipment.Items
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        /// <summary> 
        /// файловый диалог
        /// </summary>
        OpenFileDialog FileDialogImage = new OpenFileDialog();
        bool BSetImages = false;
        List<EquipmentContext> AllEquipments = EquipmentContext.Select();
        List<UsersContext> AllUsers = UsersContext.Select();
        List<StatusContext> AllStatus = StatusContext.Select();
        List<ClassroomContext> AllClassrooms = ClassroomContext.Select();
        EquipmentContext item;
        Main main;
        public Item(EquipmentContext item, Main main)
        {
            InitializeComponent();
            if (MainWindow.init.ActiveUser.Role == "Пользователь")
            {
                Edit.Visibility = Visibility.Hidden;
                Delete.Visibility = Visibility.Hidden;
            }
            Name.Text = item.Name;
            Inventory.Text = item.Code.ToString();
            ResponsibleUser.Text = AllUsers.Find(x => x.Id == item.Id_user).Surname;
            Price.Text = item.Price.ToString();
            Status.Text = AllStatus.Find(x => x.Id == item.Id_status).Name;
            Classroom.Text = AllClassrooms.Find(x => x.Id == item.Id_classroom).Code;
            this.item = item;
            this.main = main;
            try
            {
                BitmapImage biImg = new BitmapImage();
                MemoryStream ms = new MemoryStream(item.Photo);
                biImg.BeginInit();
                biImg.StreamSource = ms;
                biImg.EndInit();
                ImageSource imgSrc = biImg;
                Photo_equip.Source = imgSrc;
            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp);
            }
        }
        private void EditRecord(object sender, RoutedEventArgs e) =>
            MainWindow.init.OpenPages(new Pages.Equipment.Add(this.item));
        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить данную запись?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                item.Delete();
                main.parent.Children.Remove(this);
            }
        }
        private void SelectImage(object sender, MouseButtonEventArgs e)
        {
            if (FileDialogImage.ShowDialog() == true)
            {
                using (Imaging.Image image = Imaging.Image.Load(FileDialogImage.FileName))
                {
                    int NewWidth = 0;
                    int NewHeight = 0;
                    if (image.Width > image.Height)
                    {
                        NewWidth = (int)(image.Width * (256f / image.Height));
                        NewHeight = 256;
                    }
                    else
                    {
                        NewWidth = 256;
                        NewHeight = (int)(image.Height * (256f / image.Width));
                    }
                    image.Save("Photo_equip.jpg");
                }
                using (Imaging.RasterImage rasterImage = (Imaging.RasterImage)Imaging.Image.Load("Photo_equip.jpg"))
                {
                    if (!rasterImage.IsCached)
                    {
                        rasterImage.CacheData();
                    }
                    int X = 0;
                    int Width = 256;
                    int Y = 0;
                    int Height = 256;
                    if (rasterImage.Width > rasterImage.Height)
                        X = (int)((rasterImage.Width - 256f) / 2);
                    else
                        Y = (int)((rasterImage.Height - 256f) / 2);
                    Imaging.Rectangle rectangle = new Imaging.Rectangle(X, Y, Width, Height);
                    rasterImage.Crop(rectangle);
                    rasterImage.Save("Photo_equip.jpg");
                }
                DoubleAnimation StartAnimation = new DoubleAnimation();
                StartAnimation.From = 1;
                StartAnimation.To = 0;
                StartAnimation.Duration = TimeSpan.FromSeconds(0.6);
                StartAnimation.Completed += delegate
                {
                    Photo_equip.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Photo_equip.jpg"));
                    DoubleAnimation EndAnimation = new DoubleAnimation();
                    EndAnimation.From = 0;
                    EndAnimation.To = 1;
                    EndAnimation.Duration = TimeSpan.FromSeconds(1.2);
                    Photo_equip.BeginAnimation(Image.OpacityProperty, EndAnimation);
                };
                Photo_equip.BeginAnimation(Image.OpacityProperty, StartAnimation);
                BSetImages = true;
            }
            else
                BSetImages = false;
        }
        private void GoStory(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Equipment.Story(item));
        }
    }
}
