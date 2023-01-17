using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace ExerciseMemory
{
    public partial class WindowExercise3 : Window
    {
        private int _count = 0;
        private int _count2 = 0;
        private int _count3 = 0;
        readonly private int[] idArr = new int[3];
        readonly private bool[] IsTranslate = new bool[3];
        readonly int[] idTranslate = new int[3];
        private readonly SomeAnimation animation = new SomeAnimation();
        private readonly Database database = new Database();
        public WindowExercise3()
        {
            InitializeComponent();
            database.OpenConnection();
            StartExercise3();
            animation.Opacity_Animation(this);
            animation.Center_Screen(this);

            Ex3DataGrid.IsReadOnly = true;

            animation.WidthAnimation(Next, 150);
            animation.Opacity_Animation(LinkInfo);
            animation.Opacity_Animation(ExtInfoBox);
        }


        private void StartExercise3()
        {


            if (_count < 3)
            {
                SqlCommand command = new SqlCommand("SELECT MAX(id) FROM [idioms]", database.SqlConnection);
                int max = Convert.ToInt32(command.ExecuteScalar());

                Random rnd = new Random();
                int value = rnd.Next(1, max + 1);
                idArr[_count] = value;

                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT phrase, translate FROM [idioms] WHERE id = {value}", database.SqlConnection);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);

                DataView dataView = new DataView(dataSet.Tables[0]);
                Ex3DataGrid.ItemsSource = dataView;
            }
            else if (_count >= 3 && _count < 6)
            {
                if (_count == 3)
                    Ex3DataGrid.Columns.RemoveAt(1);
                Ex3DataGrid.HorizontalAlignment = HorizontalAlignment.Left;
                Ex3DataGrid.HorizontalContentAlignment = HorizontalAlignment.Left;
                TranslateBox.Visibility = Visibility.Visible;
                TestLabel.Visibility = Visibility.Visible;
                LinkInfo.Visibility = Visibility.Collapsed;
                ExtInfoBox.Visibility = Visibility.Collapsed;


                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT phrase FROM [idioms] WHERE id = {idArr[_count2]}", database.SqlConnection);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);

                DataView dataView = new DataView(dataSet.Tables[0]);
                Ex3DataGrid.ItemsSource = dataView;
                _count2++;
            }

            else if (_count >= 6)
            {
                Ex3DataGrid.Visibility = Visibility.Collapsed;
                TranslateBox.Visibility = Visibility.Collapsed;
                TestLabel.Visibility = Visibility.Collapsed;
                Finish.Visibility = Visibility.Visible;
                Next.Visibility = Visibility.Collapsed;
                Answers.Visibility = Visibility.Visible;
                ResultLabel1.Visibility = Visibility.Visible;
                ResultLabel2.Visibility = Visibility.Visible;
                ResultLabel3.Visibility = Visibility.Visible;



            }
            if (_count > 3 && _count <= 6)
            {
                
                SqlCommand command = new SqlCommand("SELECT id FROM [idioms] WHERE translate = @translate", database.SqlConnection);
                command.Parameters.AddWithValue("@translate", TranslateBox.Text);

                idTranslate[_count3] = Convert.ToInt32(command.ExecuteScalar());
                _count3++;
                TranslateBox.Clear();

            }

            for (int i = 0; i < 3; i++)
            {
                IsTranslate[i] = CheckTranslate(idTranslate[i], idArr[i]);
            }

            ResultLabel1.Content = OutResult(IsTranslate[0], idArr[0]);
            ResultLabel2.Content = OutResult(IsTranslate[1], idArr[1]);
            ResultLabel3.Content = OutResult(IsTranslate[2], idArr[2]);

            animation.Opacity_Animation(Answers);
            animation.Opacity_Animation(ResultLabel1);
            animation.Opacity_Animation(ResultLabel2);
            animation.Opacity_Animation(ResultLabel3);
            animation.Opacity_Animation(TestLabel);
            animation.WidthAnimation(Finish, 186);
        }
        private void Button_Options_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow optionsWindow = new OptionsWindow();
            optionsWindow.Show();
        }

        private void Button_Next_Click(object sender, RoutedEventArgs e)
        {
            _count++;
            animation.Opacity_Animation(Ex3DataGrid);
            StartExercise3();
            animation.WidthAnimation(TranslateBox, 252);
        }

        private bool CheckTranslate(int id, int idArr)
        {
            return id == idArr;
        }

        private string OutResult(bool translate, int arr)
        {
            if (translate)
            {
                return "Верный ответ";
            }
            else
            {
                SqlCommand command = new SqlCommand("SELECT phrase FROM [idioms] WHERE id = @id", database.SqlConnection);
                command.Parameters.AddWithValue("@id", arr);
                SqlCommand command1 = new SqlCommand("SELECT translate FROM [idioms] WHERE id = @id", database.SqlConnection);
                command1.Parameters.AddWithValue("@id", arr);
                return (Convert.ToString(command.ExecuteScalar()) + "  -  " + Convert.ToString(command.ExecuteScalar()));
            }
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            ExercisesWindow exercisesWindow = new ExercisesWindow();
            exercisesWindow.Show();
            Close();
        }

        private void LinkInfo_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://uchitel76.ru/latinskie-krylatye-vyrazheniya-istoriya-perevod-transkripciya/");
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
