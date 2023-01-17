using System;
using System.Data.SqlClient;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Input;

namespace ExerciseMemory
{
   
    public partial class WindowExercise2 : Window
    {
        private readonly Database database = new Database();
        readonly static string audioFile = @"test.wav";
        readonly SoundPlayer soundPlayer = new SoundPlayer(audioFile);
        private readonly SomeAnimation animation = new SomeAnimation();
        bool IsRightAnswer;
        int value;
        int idRightAnswer;
        public WindowExercise2()
        {
            InitializeComponent();
            database.OpenConnection();

            animation.Center_Screen(this);
            animation.Opacity_Animation(this);
            animation.Opacity_Animation(Info);
            animation.WidthAnimation(Next2, 150);
            animation.WidthAnimation(AnswerBox,219);
            animation.WidthAnimation(PlayAudioButton, 306);
        }

        private void Next2_Click(object sender, RoutedEventArgs e)
        {
            Next2.Visibility = Visibility.Collapsed;
            Finish2.Visibility = Visibility.Visible;
            PlayAudioButton.Visibility = Visibility.Collapsed;
            AnswerBox.Visibility = Visibility.Collapsed;
            Info.Visibility = Visibility.Collapsed;
            AnswerCheck.Visibility = Visibility.Visible;

            animation.Opacity_Animation(AnswerCheck);
            animation.Opacity_Animation(AnswerLabel);
            animation.WidthAnimation(Finish2, 186);

            soundPlayer.Stop();
            File.Delete(audioFile);

            SqlCommand command = new SqlCommand("SELECT id FROM [Sound] WHERE Answer = @Answer", database.SqlConnection);
            command.Parameters.AddWithValue("@Answer", AnswerBox.Text);

            idRightAnswer = Convert.ToInt32(command.ExecuteScalar());

            IsRightAnswer =  CheckAnswer(idRightAnswer, value);
            AnswerLabel.Visibility = Visibility.Visible; 
            AnswerLabel.Content = OutRightAnswer(IsRightAnswer, value);
        }

        private void Finish2_Click(object sender, RoutedEventArgs e)
        {

            Hide();
            ExercisesWindow exercisesWindow = new ExercisesWindow();
            exercisesWindow.Show();
            Close();
        }

        private void Button_Play_Click(object sender, RoutedEventArgs e)
        {
            Next2.IsEnabled = true;
            SqlCommand command = new SqlCommand("SELECT MAX(id) FROM [Sound]", database.SqlConnection);
            int max = Convert.ToInt32(command.ExecuteScalar());

            Random rnd = new Random();
            value = rnd.Next(1, max + 1);

            SqlCommand command1 = new SqlCommand($"SELECT SoundData FROM [Sound] WHERE Id = {value}", database.SqlConnection);

            byte[] data = (byte[])command1.ExecuteScalar();
            //string f = @"D:\programming\ExerciseMemory\test.wav";
            //audioFile = @"test.wav";

            File.WriteAllBytes(audioFile, data);


            //SoundPlayer soundPlayer = new SoundPlayer(audioFile);
            soundPlayer.Load();
            soundPlayer.Play();
            //File.Delete(f);
            PlayAudioButton.IsEnabled = false;
        }


        private bool CheckAnswer(int id, int idAnswer)
        {
            return id == idAnswer;
        }

        private string OutRightAnswer(bool answer, int idAnswer)
        {
            if (answer)
            {
                return "Вы дали верный ответ";
            }
            else
            {
                SqlCommand command = new SqlCommand("SELECT Answer FROM [Sound] WHERE id = @idAnswer", database.SqlConnection);
                command.Parameters.AddWithValue("idAnswer", idAnswer);
                
                return ("Вы ошиблись. Верный ответ: " + Convert.ToString(command.ExecuteScalar()));
            }
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
