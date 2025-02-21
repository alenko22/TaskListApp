using Microsoft.Win32;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Intrinsics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
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
            listToDo.ItemsSource = null;
            listToDo.ItemsSource = ToDos;
            EndToDo();
        }

        private void UpdateListToDo()
        {
            listToDo.ItemsSource = null;
            listToDo.ItemsSource = ToDos;
            SaveJSON();
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
        private void SaveJSON()
        {
            string json = JsonSerializer.Serialize(ToDos);
            string Path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Files", "jsonlog.json");
            File.WriteAllText(Path, json);
        }

        private void SaveTxtFile(object sender, RoutedEventArgs e)
        {
            string[] content = listToDo.Items.OfType<string>().ToArray();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Normal text file (*.txt)|*.txt";
            saveFileDialog.ShowDialog();
            saveFileDialog.OverwritePrompt = true;
            StringBuilder sb = new StringBuilder();
            if (saveFileDialog.ShowDialog() == true)
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    for (int i = 0; i < ToDos.Count; i++)
                    {
                        if (ToDos[i].Doing == true)
                        {
                            sb.Append("✓");
                        }
                        else
                        {
                            sb.Append(" ");
                        }
                        sb.AppendLine(Convert.ToString(ToDos[i].Name));
                        sb.AppendLine(" ");
                        sb.AppendLine(Convert.ToString(ToDos[i].Description));
                        sb.AppendLine(" ");
                        sb.AppendLine(Convert.ToString(ToDos[i].Date));
                        sb.AppendLine(" ");
                        sb.AppendLine(" ");
                    }
                }
            }
            string path = saveFileDialog.FileName;

            File.WriteAllText(path, Convert.ToString(sb));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SaveJSON();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NewDo newDo = new NewDo();
            newDo.Show();
        }

        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            ToDos.Remove((ToDo)listToDo.SelectedItem);
            UpdateListToDo();
            EndToDo();
        }
    }
}