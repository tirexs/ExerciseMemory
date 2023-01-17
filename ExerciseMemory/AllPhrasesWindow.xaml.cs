using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ExerciseMemory
{
    /// <summary>
    /// Логика взаимодействия для AllPhrasesWindow.xaml
    /// </summary>
    public partial class AllPhrasesWindow : Window
    {
        readonly Database database = new Database();

        public AllPhrasesWindow()
        {
            InitializeComponent();

            database.OpenConnection();

            BaseGrid.IsReadOnly = true;

            SqlDataAdapter command = new SqlDataAdapter("SELECT * FROM [idioms]", database.SqlConnection);
            
            DataSet dataSet = new DataSet();
            command.Fill(dataSet);
            
            DataView dataView = new DataView(dataSet.Tables[0]);
            BaseGrid.ItemsSource = dataView;
        }

        [System.Obsolete]
        private void Button_Find_Click(object sender, RoutedEventArgs e)
        {
            
            //SqlDataAdapter command = new SqlDataAdapter($"SELECT * FROM [idioms] WHERE translate = '{FindBox.Text}' ", database.SqlConnection);
            SqlDataAdapter command = new SqlDataAdapter("SELECT * FROM [idioms] WHERE translate = @translate ", database.SqlConnection);
            command.SelectCommand.Parameters.Add("@translate", FindBox.Text);
            DataSet dataSet = new DataSet();
            command.Fill(dataSet);

            DataView dataView = new DataView(dataSet.Tables[0]);
            BaseGrid.ItemsSource = dataView;
        }
    }
}
