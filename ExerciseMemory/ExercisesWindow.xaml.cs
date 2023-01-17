using System;
using System.Windows;
using System.Windows.Input;

namespace ExerciseMemory
{
    public partial class ExercisesWindow : Window
    {
        private readonly SomeAnimation animation = new SomeAnimation();
        public ExercisesWindow()
        {
            InitializeComponent();

            animation.Center_Screen(this);
            animation.Opacity_Animation(this);
            animation.Opacity_Animation(options);
            animation.Opacity_Animation(Chioce_ex);
            animation.WidthAnimation(Button_ex1, 209);
            animation.WidthAnimation(Button_ex2, 209);
            animation.WidthAnimation(Button_ex3, 209);
        }


        private void Button_Exercise1_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            WindowExercise1 windowExercise1 = new WindowExercise1();
            windowExercise1.Show();
            Close();
        }

        private void Button_Exercise3_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            WindowExercise3 windowExercise3 = new WindowExercise3();
            windowExercise3.Show();
            Close();
        }

        private void Button_Options_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow optionsWindow = new OptionsWindow();
            optionsWindow.Show();
        }

        private void Button_Exercise2_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            WindowExercise2 windowExercise2 = new WindowExercise2();
            windowExercise2.Show();
            Close();
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
