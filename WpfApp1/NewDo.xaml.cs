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

namespace TaskListApp
{
    public partial class NewDo : Window
  {  
        public NewDo()
        {
            InitializeComponent();
            dateToDo.SelectedDate = DateTime.Now;
        }
        public static RoutedCommand AddToDoCommand = new RoutedCommand();

        // GroupBox needs to be cleared for new task
        private void ClearGroupBoxToDo()
        {
            titleToDo.Text = null;
            dateToDo.SelectedDate = new DateTime(2024, 1, 10);
            descriptionToDo.Text = "Нет описания";
        }

        // Updating main list of tasks
        private void UpdateListToDo()
        {
            (this.Owner as MainWindow).listToDo.ItemsSource = null;
            (this.Owner as MainWindow).listToDo.ItemsSource = (this.Owner as MainWindow).toDos;
        }
        public void SaveDo(object sender, RoutedEventArgs e)
        {
            ToDo task = new ToDo(
            titleToDo.Text,
            dateToDo.SelectedDate.Value,
            descriptionToDo.Text
            );
            if (task.Name == null ||  task.Name.Length == 0 || task.Name == " ")
            {
                return;
            }
            else if (task.Description == null || task.Description.Length == 0 || task.Description == " ")
            {
                MessageBox.Show("Вы не ввели описание", " ", MessageBoxButton.OK);
                task.Description = "Нет описания";
                (this.Owner as MainWindow).toDos.Add(task);
            }
            else if (task.Date == null)
            {
                MessageBox.Show("Введите дату", " ", MessageBoxButton.OK);
                SaveDo(sender, e);
            }
            else
            {
                (this.Owner as MainWindow).toDos.Add(task);
            }

            ClearGroupBoxToDo();
            UpdateListToDo();
            this.Close();
            (this.Owner as MainWindow).UpdateTaskBar();
        }

        // Apply affair by pressing Enter(Return)
        private void CommandEnter(object sender, ExecutedRoutedEventArgs e)
        {
            ToDo task = new ToDo(
            titleToDo.Text,
            dateToDo.SelectedDate.Value,
            descriptionToDo.Text
            );
            (this.Owner as MainWindow).toDos.Add(task);

            ClearGroupBoxToDo();
            UpdateListToDo();
            this.Close();
            (this.Owner as MainWindow).UpdateTaskBar();
        }
    }
}
