using System;
using System.Collections.Generic;
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
using UCHET.Classes;
using System.Diagnostics;


namespace UCHET.Pages.Equipment
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        OpenFileDialog FileDialogImage = new OpenFileDialog();
        bool BSetImages = false;
        public string Code_old;
        public string User_old;
        EquipmentContext equipment;
        List<EquipmentContext> AllEquipments = EquipmentContext.Select();
        List<StatusContext> AllStatuses = StatusContext.Select();
        List<UsersContext> AllUsers = UsersContext.Select();
        List<ClassroomContext> AllClassrooms = ClassroomContext.Select();

        public Add(EquipmentContext equipment = null)
        {
            InitializeComponent();
            FileDialogImage.Filter = "JPG (*.jpg)|*.jpg|PNG (*.png)|*.png|GIF (*.gif)|*.gif";
            FileDialogImage.RestoreDirectory = true;
            foreach (var item in AllUsers)
                ResponsibleUser.Items.Add(item.Surname);
            ResponsibleUser.Items.Add("Выберите...");
            foreach (var item in AllStatuses)
                Status.Items.Add(item.Name);
            Status.Items.Add("Выберите...");
            foreach (var item in AllClassrooms)
                Classroom.Items.Add(item.Code);
            Classroom.Items.Add("Выберите...");

            if (equipment != null)
            {
                this.equipment = equipment;
                Name.Text = equipment.Name;
                Inventory.Text = equipment.Code;
                try
                {
                    BitmapImage biImg = new BitmapImage();
                    MemoryStream ms = new MemoryStream(equipment.Photo);
                    biImg.BeginInit();
                    biImg.StreamSource = ms;
                    biImg.EndInit();
                    ImageSource imgSrc = biImg;
                }
                catch (Exception exp)
                {
                    Debug.WriteLine(exp.Message);
                };
                ResponsibleUser.SelectedIndex = AllEquipments.FindIndex(x => x.Id_user == equipment.Id_user);
                price.Text = equipment.Price.ToString();
                Status.SelectedIndex = AllEquipments.FindIndex(x => x.Id_status == equipment.Id_status);
                Classroom.SelectedIndex = AllEquipments.FindIndex(x => x.Id_classroom == equipment.Id_classroom);
                btnAdd.Content = "Изменить";
                Code_old = AllClassrooms.Where(x => x.Id == equipment.Id_classroom).First().Code;
                User_old = AllUsers.Where(x => x.Id == equipment.Id_user).First().Surname;
            }
            else
            {
                Comment.Visibility = Visibility.Hidden;
                txtComment.Visibility = Visibility.Hidden;
                ResponsibleUser.SelectedIndex = ResponsibleUser.Items.Count - 1;
                Status.SelectedIndex = Status.Items.Count - 1;
                Classroom.SelectedIndex = Classroom.Items.Count - 1;
            }
        }

        private void AddRecord(object sender, RoutedEventArgs e)
        {
            if (Name.Text == "")
            {
                MessageBox.Show("Необходимо указать наименование оборудования");
                return;
            }
            if (Inventory.Text == "" || !Classes.Common.CheckRegex.Match("^[0-9]*$", Inventory.Text))
            {
                MessageBox.Show("Необходимо указать инвентарный номер");
                return;
            }
            if (!Classes.Common.CheckRegex.Match("^[0-9]{0,}$", price.Text))
            {
                MessageBox.Show("Необходимо правильно указать стоимость оборудования оборудования");
                return;
            }

            if (equipment == null)
            {
                byte[] sss = null;
                if (BSetImages)
                    sss = File.ReadAllBytes(Directory.GetCurrentDirectory() + @"\Photo_equip.jpg");
                EquipmentContext newEquipment = new EquipmentContext(
                    0,
                    AllUsers.Find(x => x.Surname == ResponsibleUser.SelectedItem).Id,
                    AllClassrooms.Find(x => x.Code == Classroom.SelectedItem).Id,
                    AllStatuses.Find(x => x.Name == Status.SelectedItem).Id,
                    Name.Text,
                    sss,
                    Inventory.Text,
                    int.Parse(price.Text));
                newEquipment.Add();
                MessageBox.Show("Запись добавлена");
            }
            else
            {
                equipment = new EquipmentContext(
                    equipment.Id,
                    AllUsers.Find(x => x.Surname == ResponsibleUser.SelectedItem).Id,
                    AllClassrooms.Find(x => x.Code == Classroom.SelectedItem).Id,
                    AllStatuses.Find(x => x.Name == Status.SelectedItem).Id,
                    Name.Text,
                    equipment.Photo,
                    Inventory.Text,
                    int.Parse(price.Text)
                );
                equipment.Update();

                string Code_new = AllClassrooms.Where(x => x.Id == equipment.Id_classroom).First().Code;
                string User_new = AllUsers.Where(x => x.Id == equipment.Id_user).First().Surname;

                if (Code_new != Code_old || User_new != User_old)
                {
                    StoryContext newStory = new StoryContext(
                        0,
                        equipment.Id,
                        AllClassrooms.Where(x => x.Code == Code_old).First().Id,
                        AllClassrooms.Where(x => x.Code == Code_new).First().Id,
                        DateTime.Now,
                        AllUsers.Where(x => x.Surname == User_old).First().Id,
                        AllUsers.Where(x => x.Surname == User_new).First().Id,
                        Comment.Text
                    );
                    newStory.Add();
                }
                MessageBox.Show("Запись обновлена");
            }

            MainWindow.init.OpenPages(new Pages.Equipment.Main());
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
                    image.Resize(NewWidth, NewHeight);
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
    }
}
