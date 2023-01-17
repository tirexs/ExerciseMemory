using System.Data.SqlClient;
using System.IO;
using System.Windows;

namespace ExerciseMemory
{
    public partial class OptionsWindow : Window
    {
        readonly Database database = new Database();
        public string Soundname;
        static public byte[] SoundData;

        public OptionsWindow()
        {
            InitializeComponent();
            database.OpenConnection();
        }

        
        
        //Exercise 1
        private void Button_Add1_Click(object sender, RoutedEventArgs e)
        {

            SqlCommand command = new SqlCommand("INSERT INTO [dates] (id, event, date) VALUES (@id, @event, @date)", database.SqlConnection);

            command.Parameters.AddWithValue("id", TextBox5.Text);
            command.Parameters.AddWithValue("event", TextBox6.Text);
            command.Parameters.AddWithValue("date", TextBox7.Text);
            MessageBox.Show(command.ExecuteNonQuery().ToString());
        }

        private void Button_AllInfo1_Click(object sender, RoutedEventArgs e)
        {
            AllDates allDates = new AllDates();
            allDates.Show();
        }

        private void Button_Delete1_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAll1.IsChecked == false)
            {
                SqlCommand command = new SqlCommand("DELETE FROM [dates] WHERE id = @id", database.SqlConnection);

                command.Parameters.AddWithValue("id", TextBox8.Text);
                MessageBox.Show(command.ExecuteNonQuery().ToString());

            }
            else
            {
                SqlCommand command = new SqlCommand("DELETE FROM [dates]", database.SqlConnection);
                MessageBox.Show(command.ExecuteNonQuery().ToString());
            }
        }

        private void CheckAll1_Checked(object sender, RoutedEventArgs e)
        {
            TextBox8.IsEnabled = false;
        }

        private void CheckAll1_Unchecked(object sender, RoutedEventArgs e)
        {
            TextBox8.IsEnabled = true;
        }


        //Exercise 2
        private void Button_SelectAudio_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "",
                DefaultExt = ""
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                Soundname = dialog.FileName;
            }
        }

        private void Button_Add2_Click(object sender, RoutedEventArgs e)
        {

            SqlCommand command = new SqlCommand("INSERT INTO [Sound] VALUES (@Id, @FileName, @SoundData, @Answer)", database.SqlConnection);


            FileStream fileStream = new FileStream(Soundname, FileMode.Open);

            SoundData = new byte[fileStream.Length];
            fileStream.Read(SoundData, 0, SoundData.Length);

            command.Parameters.AddWithValue("@Id", TextBox9.Text);
            command.Parameters.AddWithValue("@FileName", TextBox10.Text);
            command.Parameters.AddWithValue("@SoundData", SoundData);
            command.Parameters.AddWithValue("@Answer", TextBox12.Text);
            command.ExecuteNonQuery().ToString();


        }

        private void CheckAll2_Checked(object sender, RoutedEventArgs e)
        {
            TextBox11.IsEnabled = false;
        }

        private void CheckAll2_Unchecked(object sender, RoutedEventArgs e)
        {
            TextBox11.IsEnabled = true;
        }

        private void Button_Delete2_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAll2.IsChecked == false)
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Sound] WHERE id = @id", database.SqlConnection);

                command.Parameters.AddWithValue("id", TextBox11.Text);
                MessageBox.Show(command.ExecuteNonQuery().ToString());

            }
            else
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Sound]", database.SqlConnection);
                MessageBox.Show(command.ExecuteNonQuery().ToString());
            }
        }
        private void Button_AllInfo2_Click(object sender, RoutedEventArgs e)
        {
            AllSound allSound = new AllSound();
            allSound.Show();
        }

        //Exercise 3
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {

            SqlCommand command = new SqlCommand("INSERT INTO [idioms] (id, phrase, translate) VALUES (@id, @phrase, @translate)", database.SqlConnection);

            command.Parameters.AddWithValue("id", TextBox1.Text);
            command.Parameters.AddWithValue("phrase", TextBox2.Text);
            command.Parameters.AddWithValue("translate", TextBox3.Text);
            MessageBox.Show(command.ExecuteNonQuery().ToString());
        }



        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAll.IsChecked == false)
            {
                SqlCommand command = new SqlCommand("DELETE FROM [idioms] WHERE id = @id", database.SqlConnection);

                command.Parameters.AddWithValue("id", TextBox4.Text);
                MessageBox.Show(command.ExecuteNonQuery().ToString());

            }
            else
            {
                SqlCommand command = new SqlCommand("DELETE FROM [idioms]", database.SqlConnection);
                MessageBox.Show(command.ExecuteNonQuery().ToString());
            }
        }

        private void Button_AllInfo_Click(object sender, RoutedEventArgs e)
        {
            AllPhrasesWindow allPhrasesWindow = new AllPhrasesWindow();
            allPhrasesWindow.Show();
        }

        private void CheckAll_Checked(object sender, RoutedEventArgs e)
        {
            TextBox4.IsEnabled = false;
        }

        private void CheckAll_Unchecked(object sender, RoutedEventArgs e)
        {
            TextBox4.IsEnabled = true;
        }

        private void Button_AddDatabase_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "",
                DefaultExt = ""
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                MainWindow.DatebasePath = dialog.FileName;
            }
        }
        

    }
}
