using System;
using System.Windows;
using System.Windows.Input;

namespace ExerciseMemory
{
    public partial class MainWindow : Window
    {
        public static string DatebasePath;

        private readonly SomeAnimation animation = new SomeAnimation();
        
        public MainWindow()
        {
            InitializeComponent();

            animation.Center_Screen(this);
            animation.Opacity_Animation(this);
            animation.Opacity_Animation(welcome);
            animation.Opacity_Animation(info);
            animation.WidthAnimation(start, 220);    
        }

        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            AddDatabase();
            Hide();
            ExercisesWindow exercisesWindow = new ExercisesWindow();
            exercisesWindow.Show();
            Close();
        }

        private void AddDatabase() 
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "",
                DefaultExt = ""
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                DatebasePath = dialog.FileName;
            }
        }

        private void DevelopersInfo(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ершов Даниил Игоревич\nГенке Сергей Викторович\nСарбуков Леонид Русланович", "Разработчики");
        }



        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        private void Toolbar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
