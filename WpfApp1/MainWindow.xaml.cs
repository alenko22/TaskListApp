using System.Security.Cryptography.X509Certificates;
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

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public List<ToDo> ToDos;
        public MainWindow()
        {
            InitializeComponent();

            descriptionToDo.Text = "Описания нет";
            dateToDo.SelectedDate = new DateTime(day: 10, month: 1, year: 2024);

            ToDos = new List<ToDo>
            {
                new ToDo("Помыть пол", new DateTime(1111, 12, 22), "Просто помыть пол"),
                new ToDo("Слетать на Луну", new DateTime(3111, 11, 1), "Посмотреть как тоам"),
                new ToDo("Посмотреть начало Нашей Эры", new DateTime(1, 12, 22), "Интересно же")
            };

        }

        private void buttonToDo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            groupBoxToDo.Visibility = Visibility.Visible;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            groupBoxToDo.Visibility=Visibility.Collapsed;
        }
    }
}