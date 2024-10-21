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
    /// Логика взаимодействия для Story.xaml
    /// </summary>
    public partial class Story : Page
    {
        List<EquipmentContext> AllEquipments = EquipmentContext.Select();
        List<StoryContext> AllStory = StoryContext.Select();
        List<ClassroomContext> AllClass = ClassroomContext.Select();
        List<UsersContext> AllUsers = UsersContext.Select();

        public class Order
        {
            public string Code_Old { get; set; }
            public string Code_New { get; set; }
            public DateTime Data { get; set; }
            public string User_Old { get; set; }
            public string User_New { get; set; }
            public string Comment { get; set; }
            public Order(string Code_Old, string Code_New, DateTime Data, string User_Old, string User_New, string Comment)
            {
                this.Data = Data;
                this.Code_Old = Code_Old;
                this.Code_New = Code_New;
                this.User_Old = User_Old;
                this.User_New = User_New;
                this.Comment = Comment;
            }
        }

        public Story(Models.Equipment equipment)
        {
            InitializeComponent();
            foreach (StoryContext story in AllStory)
            {
                if (story.Id_pc == equipment.Id)
                {
                    Order order = new Order(AllClass.Where(x => x.Id == story.Code_Old).First().Code, AllClass.Where(x => x.Id == story.Code_New).First().Code, story.Data,
                        AllUsers.Where(x => x.Id == story.User_Old).First().Surname, AllUsers.Where(x => x.Id == story.User_New).First().Surname, story.Comment);
                    pc_story.Items.Add(order);
                }
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Equipment.Main());
        }
    }
}
