using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.Intrinsics;
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

            ToDos = new List<ToDo>
            {
                new ToDo("Помыть пол", new DateTime(1111, 12, 22), "Просто помыть пол"),
                new ToDo("Слетать на Луну", new DateTime(3111, 11, 1), "Посмотреть как там"),
                new ToDo("Посмотреть начало Нашей Эры", new DateTime(1, 12, 22), "Интересно же")
            };
            listToDo.ItemsSource = ToDos;
            EndToDo();
        }

        private void UpdateListToDo()
        {
            listToDo.ItemsSource = null;
            listToDo.ItemsSource = ToDos;
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            ToDos.Remove((sender as Button).DataContext as ToDo);
            UpdateListToDo();
            EndToDo();
        }
        private void ButtonToDoClick(object sender, RoutedEventArgs e)
        {
            NewDo newDo = new NewDo();
            newDo.Owner = this;
            newDo.Show();
            
        }
        internal void EndToDo()
        {
            int Max = 0;
            int Val = 0;


            Max = ToDos.Count;
            for (int i = 0; i < Max; i++)
            {
                if (ToDos[i].Doing == true)
                {
                    Val++;
                }
            }

            ProgressToDo.Value = Val;
            ProgressToDo.Maximum = Max;

            TextProgressToDo.Text = Val + "/" + Max;
        }

        private void CheckBoxDoing_Checked(object sender, RoutedEventArgs e)
        {
            ToDo itemToDo = listToDo.SelectedItem as ToDo;
            if (itemToDo == null)
            {
                EndToDo();
            }
            else
            {
                itemToDo.Doing = true;
                EndToDo();
            }
        }

        private void CheckBoxDoing_Unchecked(object sender, RoutedEventArgs e)
        {
            ToDo itemToDo = listToDo.SelectedItem as ToDo;
            if (itemToDo == null)
            {
                EndToDo();
            }
            else
            {
                itemToDo.Doing = false;
                EndToDo();
            }
        }
    }
}