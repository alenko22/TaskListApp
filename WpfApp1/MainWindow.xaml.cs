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

namespace TaskListApp
{
    public partial class MainWindow : Window
    {
        public List<ToDo> toDos { get; set; }
        private readonly string jsonPath;
        public MainWindow()
        {
            InitializeComponent();

            Loaded += WindowLoaded;
            Closed += WindowClosed;

            string directory = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Files");
            if (!Directory.Exists(directory))
            { 
                Directory.CreateDirectory(directory); 
            }
            jsonPath = System.IO.Path.Combine(directory, "jsonlog.json");

            if (!File.Exists(jsonPath))
            {
                toDos.Add(new ToDo("Помыть пол", new DateTime(1111, 12, 22), "Просто помыть пол"));
                toDos.Add(new ToDo("Слетать на Луну", new DateTime(3111, 11, 1), "Посмотреть как там"));
                toDos.Add(new ToDo("Посмотреть начало Нашей Эры", new DateTime(1, 12, 22), "Интересно же"));
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadFromJSON();
            UpdateWindow();
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            SaveToJSON();
        }

        internal void UpdateWindow()
        {
            listToDo.ItemsSource = null;
            listToDo.ItemsSource = toDos;
            UpdateTaskBar();
            SaveToJSON();
        }
        internal void UpdateTaskBar()
        {
            int Max = 0;
            int Val = 0;


            Max = toDos.Count;
            for (int i = 0; i < Max; i++)
            {
                if (toDos[i].Doing == true)
                {
                    Val++;
                }
            }

            ProgressToDo.Value = Val;
            ProgressToDo.Maximum = Max;

            TextProgressToDo.Text = Val + "/" + Max;
        }

        private void CheckBoxDoingChecked(object sender, RoutedEventArgs e)
        {
            ToDo itemToDo = listToDo.SelectedItem as ToDo;
            if (itemToDo == null)
            {
                UpdateTaskBar();
            }
            else
            {
                itemToDo.Doing = true;
                UpdateTaskBar();
            }
            UpdateWindow();
        }

        private void CheckBoxDoingUnchecked(object sender, RoutedEventArgs e)
        {
            ToDo itemToDo = listToDo.SelectedItem as ToDo;
            if (itemToDo == null)
            {
                UpdateTaskBar();
            }
            else
            {
                itemToDo.Doing = false;
                UpdateTaskBar();
            }
            UpdateWindow();
        }
        public void SaveToJSON()
        {
            JsonSerializerOptions options = new() { WriteIndented = true };
            FileStream stream = new(jsonPath, FileMode.Create);
            JsonSerializer.Serialize(stream, toDos, options);
            stream.Close();
        }
        private void ButtonDeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить дело?", "Удаление дела", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                if (sender != null)
                {
                    toDos.Remove(((Button)sender).DataContext as ToDo);
                }
                else
                {
                    toDos.Remove((ToDo)listToDo.SelectedItem);
                }

                listToDo.ItemsSource = null;
                listToDo.ItemsSource = toDos;
                UpdateTaskBar();
                SaveToJSON();
            }
            UpdateWindow();
        }

        // Добавление нового дела на CTRL + N
        private void CommandNew(object sender, ExecutedRoutedEventArgs e)
        {
            NewDo newDo = new NewDo();
            newDo.Owner = this;
            newDo.Show();
            UpdateWindow();
        }

        // Удаление дела на Delete
        private void CommandDelete(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить дело?", "Удаление дела", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                toDos.Remove((ToDo)listToDo.SelectedItem);

                listToDo.ItemsSource = null;
                listToDo.ItemsSource = toDos;
                UpdateTaskBar();
                SaveToJSON();
            }
        }

        // Save affairs to TXT by pressing CTRL + S
        private void CommandSaveTXT(object sender, ExecutedRoutedEventArgs e)
        {
            if (toDos.Count == 0)
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
                for (int i = 0; i < toDos.Count; i++)
                {
                    if (toDos[i].Doing == true)
                    {
                        sb.Append("✓");
                    }
                    else
                    {
                        sb.Append(" ");
                    }
                    sb.AppendLine(Convert.ToString(toDos[i].Name));
                    sb.AppendLine(" ");
                    sb.AppendLine(Convert.ToString(toDos[i].Description));
                    sb.AppendLine(" ");
                    sb.AppendLine(Convert.ToString(toDos[i].Date));
                    sb.AppendLine(" ");
                    sb.AppendLine(" ");
                }
                string patha = saveFileDialog.FileName;
                File.WriteAllText(patha, Convert.ToString(sb));
            }
            
        }
        private void LoadFromJSON()
        {
            // Only if file exists
            if (File.Exists(jsonPath))
            {
                FileStream stream = new(jsonPath, FileMode.Open);
                List<ToDo>? loadedList = JsonSerializer.Deserialize<List<ToDo>>(stream);
                stream.Close();
                if (loadedList != null)
                {
                    toDos = loadedList;
                }
            }
        }
    }
}