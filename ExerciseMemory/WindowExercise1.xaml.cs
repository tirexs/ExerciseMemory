using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace ExerciseMemory
{
    public partial class WindowExercise1 : Window
    {

        private int _count = 0;
        private int _count2 = 0;
        private int _count3 = 0;
        readonly private int[] idArr = new int[5];
        readonly private bool[] IsTranslate = new bool[5];
        readonly int[] idTranslate = new int[5];

        private readonly SomeAnimation animation = new SomeAnimation();
        private readonly Database database = new Database();
        public WindowExercise1()
        {
            InitializeComponent();

            database.OpenConnection();
            StartExercise1();

            Ex1DataGrid.IsReadOnly = true;

            animation.Center_Screen(this);
            animation.Opacity_Animation(this);
            
            animation.Opacity_Animation(LabelTest);
            animation.WidthAnimation(Next, 150);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            _count++;
            animation.Opacity_Animation(Ex1DataGrid);
            StartExercise1();
            animation.Opacity_Animation(Answers);
            animation.Opacity_Animation(Result1);
            animation.Opacity_Animation(Result2);
            animation.Opacity_Animation(Result3);
            animation.Opacity_Animation(Result4);
            animation.Opacity_Animation(Result5);
            animation.WidthAnimation(Finish, 186);
        }

        

        private void Finish_Click(object sender, RoutedEventArgs e)
        { 
            Hide();
            ExercisesWindow exercisesWindow = new ExercisesWindow();
            exercisesWindow.Show();
            Close();  
        }

        private void StartExercise1() 
        {
            if (_count < 5)
            {
                SqlCommand command = new SqlCommand("SELECT MAX(id) FROM [dates]", database.SqlConnection);
                int max = Convert.ToInt32(command.ExecuteScalar());

                Random rnd = new Random();
                int value = rnd.Next(1, max + 1);
                idArr[_count] = value;

                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT event, date FROM [dates] WHERE id = {value}", database.SqlConnection);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);

                DataView dataView = new DataView(dataSet.Tables[0]);
                Ex1DataGrid.ItemsSource = dataView;
            }
            else if (_count >= 5 && _count < 10)
            {
                
                animation.WidthAnimation(DateBox, 252);

                if (_count == 5)
                    Ex1DataGrid.Columns.RemoveAt(1);

                DateBox.Visibility = Visibility.Visible;
                LabelTest.Visibility = Visibility.Visible;


                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT event FROM [dates] WHERE id = {idArr[_count2]}", database.SqlConnection);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);

                DataView dataView = new DataView(dataSet.Tables[0]);
                Ex1DataGrid.ItemsSource = dataView;
                _count2++;
            }

            else if (_count >= 10)
            {
                Answers.Visibility = Visibility.Visible;
                Ex1DataGrid.Visibility = Visibility.Collapsed;
                DateBox.Visibility = Visibility.Collapsed;
                LabelTest.Visibility = Visibility.Collapsed;
                Finish.Visibility = Visibility.Visible;
                Next.Visibility = Visibility.Collapsed;
                Result1.Visibility = Visibility.Visible;
                Result2.Visibility = Visibility.Visible;
                Result3.Visibility = Visibility.Visible;
                Result4.Visibility = Visibility.Visible;
                Result5.Visibility = Visibility.Visible;
            }
            
            if (_count > 5 && _count <= 10)
            {
                SqlCommand command = new SqlCommand("SELECT id FROM [dates] WHERE date = @date", database.SqlConnection);
                command.Parameters.AddWithValue("@date", DateBox.Text);

                idTranslate[_count3] = Convert.ToInt32(command.ExecuteScalar());
                _count3++;
                DateBox.Clear();
            }

            for (int i = 0; i < 5; i++) 
            {
                IsTranslate[i] = CheckTranslate(idTranslate[i], idArr[i]);
            }
            
            Result1.Content = OutResult(IsTranslate[0], idArr[0]);
            Result2.Content = OutResult(IsTranslate[1], idArr[1]);
            Result3.Content = OutResult(IsTranslate[2], idArr[2]);
            Result4.Content = OutResult(IsTranslate[3], idArr[3]);
            Result5.Content = OutResult(IsTranslate[4], idArr[4]);
            
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
                
                SqlCommand command = new SqlCommand("SELECT event FROM [dates] WHERE id = @id", database.SqlConnection);
                command.Parameters.AddWithValue("@id", arr);
                SqlCommand command1 = new SqlCommand("SELECT date FROM [dates] WHERE id = @id", database.SqlConnection);
                command1.Parameters.AddWithValue("@id", arr);

                return (Convert.ToString(command.ExecuteScalar()) + "  -  " + Convert.ToString(command1.ExecuteScalar()));
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
