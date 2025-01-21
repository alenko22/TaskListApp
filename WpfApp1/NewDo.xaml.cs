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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для NewDo.xaml
    /// </summary>
    public partial class NewDo : Window
  {  
        public List<ToDo> ToDos { get; set; }
        public NewDo()
        {
            InitializeComponent();
            ToDos =
            [
                new ToDo("Помыть пол", new DateTime(1111, 12, 22), "Просто помыть пол"),
                new ToDo("Слетать на Луну", new DateTime(3111, 11, 1), "Посмотреть как там"),
                new ToDo("Посмотреть начало Нашей Эры", new DateTime(1, 12, 22), "Интересно же")
            ];
        }
        private void ClearGroupBoxToDo()
        {
            titleToDo.Text = null;
            dateToDo.SelectedDate = new DateTime(2024, 1, 10);
            descriptionToDo.Text = "Нет описания";
        }

        private void UpdateListToDo()
        {
            (this.Owner as MainWindow).listToDo.ItemsSource = null;
            (this.Owner as MainWindow).listToDo.ItemsSource = ToDos;
        }
        public void SaveDo(object sender, RoutedEventArgs e)
        {
            ToDo task = new ToDo(
            titleToDo.Text,
            dateToDo.SelectedDate.Value,
            descriptionToDo.Text
            );
            ToDos.Add(task);

            ClearGroupBoxToDo();
            UpdateListToDo();
        }
    }
}
