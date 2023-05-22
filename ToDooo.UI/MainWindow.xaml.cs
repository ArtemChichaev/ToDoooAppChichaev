using System.Windows;
using System.Windows.Controls;

using ToDooo.CommonAPI;

namespace ToDooo.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppState appState = new AppState();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = appState;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateToDoItemWindow createToDoItemWindow = new CreateToDoItemWindow();
            createToDoItemWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (createToDoItemWindow.ShowDialog() == true)
            {
                if (!string.IsNullOrEmpty(createToDoItemWindow.TaskName.Text))
                {
                    appState.CreateTask(createToDoItemWindow.TaskName.Text);
                }
            }
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var task = (sender as Button)?.DataContext as ToDoItem;
            if (task != null)
            {
                appState.DeleteTask(task);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ToDoItem? task = (sender as CheckBox)?.DataContext as ToDoItem;
            if (task != null)
            {
                appState.ChangeCompletition(task);
            }
        }
    }
}