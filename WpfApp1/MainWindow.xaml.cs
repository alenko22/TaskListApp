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
        public List<ToDo> ToDos { get; set; }
        private readonly string jsonPath;
        public MainWindow()
        {
            InitializeComponent();

            Loaded += Window_Loaded;
            Closed += Window_Closed;

            string directory = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Files");
            if (!Directory.Exists(directory))
            { 
                Directory.CreateDirectory(directory); 
            }
            jsonPath = System.IO.Path.Combine(directory, "jsonlog.json");

            if (!File.Exists(jsonPath))
            {
                ToDos.Add(new ToDo("Помыть пол", new DateTime(1111, 12, 22), "Просто помыть пол"));
                ToDos.Add(new ToDo("Слетать на Луну", new DateTime(3111, 11, 1), "Посмотреть как там"));
                ToDos.Add(new ToDo("Посмотреть начало Нашей Эры", new DateTime(1, 12, 22), "Интересно же"));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadFromJSON();
            UpdateWindow();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SaveJSON();
        }

        internal void UpdateWindow()
        {
            listToDo.ItemsSource = null;
            listToDo.ItemsSource = ToDos;
            EndToDo();
            SaveJSON();
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
            UpdateWindow();
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
            UpdateWindow();
        }
        public void SaveJSON()
        {
            JsonSerializerOptions options = new() { WriteIndented = true };
            FileStream stream = new(jsonPath, FileMode.Create);
            JsonSerializer.Serialize(stream, ToDos, options);
            stream.Close();
        }
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить дело?", "Удаление дела", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                if (sender != null)
                {
                    ToDos.Remove(((Button)sender).DataContext as ToDo);
                }
                else
                {
                    ToDos.Remove((ToDo)listToDo.SelectedItem);
                }

                listToDo.ItemsSource = null;
                listToDo.ItemsSource = ToDos;
                EndToDo();
                SaveJSON();
            }
            UpdateWindow();
        }

        // Добавление нового дела на CTRL + N
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NewDo newDo = new NewDo();
            newDo.Owner = this;
            newDo.Show();
            UpdateWindow();
        }

        // Удаление дела на Delete
        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить дело?", "Удаление дела", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                ToDos.Remove((ToDo)listToDo.SelectedItem);

                listToDo.ItemsSource = null;
                listToDo.ItemsSource = ToDos;
                EndToDo();
                SaveJSON();
            }
        }

        // Save affairs to TXT by pressing CTRL + S
        private void CommandBinding_Executed_2(object sender, ExecutedRoutedEventArgs e)
        {
            if (ToDos.Count == 0)
            {
                MessageBoxResult result = MessageBox.Show("В списке нет дел", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            string[] content = listToDo.Items.OfType<string>().ToArray();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Normal text file (*.txt)|*.txt";
            StringBuilder sb = new StringBuilder();
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
                string path1 = saveFileDialog.FileName;
                File.WriteAllText(path1, Convert.ToString(sb));
            }
            
        }
        private void LoadFromJSON()
        {
            if (File.Exists(jsonPath))
            {
                FileStream stream = new(jsonPath, FileMode.Open);
                List<ToDo>? loadedList = JsonSerializer.Deserialize<List<ToDo>>(stream);
                stream.Close();
                if (loadedList != null)
                {
                    ToDos = loadedList;
                }
            }
        }
    }
}