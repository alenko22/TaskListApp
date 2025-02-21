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
    public partial class NewDo : Window
  {  
        public NewDo()
        {
            InitializeComponent();
            dateToDo.SelectedDate = DateTime.Now;
        }
        public static RoutedCommand AddToDoCommand = new RoutedCommand();
        private void ClearGroupBoxToDo()
        {
            titleToDo.Text = null;
            dateToDo.SelectedDate = new DateTime(2024, 1, 10);
            descriptionToDo.Text = "Нет описания";
        }

        private void UpdateListToDo()
        {
            (this.Owner as MainWindow).listToDo.ItemsSource = null;
            (this.Owner as MainWindow).listToDo.ItemsSource = (this.Owner as MainWindow).ToDos;
        }
        public void SaveDo(object sender, RoutedEventArgs e)
        {
            ToDo task = new ToDo(
            titleToDo.Text,
            dateToDo.SelectedDate.Value,
            descriptionToDo.Text
            );
            (this.Owner as MainWindow).ToDos.Add(task);

            ClearGroupBoxToDo();
            UpdateListToDo();
            this.Close();
            (this.Owner as MainWindow).EndToDo();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ToDo task = new ToDo(
            titleToDo.Text,
            dateToDo.SelectedDate.Value,
            descriptionToDo.Text
            );
            (this.Owner as MainWindow).ToDos.Add(task);

            ClearGroupBoxToDo();
            UpdateListToDo();
            this.Close();
            (this.Owner as MainWindow).EndToDo();
        }
    }
}
