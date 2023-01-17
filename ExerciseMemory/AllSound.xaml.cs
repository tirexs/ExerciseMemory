using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ExerciseMemory
{
    public partial class AllSound : Window
    {
        readonly Database database = new Database();
        public AllSound()
        {
            InitializeComponent();
            database.OpenConnection();

            BaseGrid.IsReadOnly = true;

            SqlDataAdapter command = new SqlDataAdapter("SELECT * FROM [Sound]", database.SqlConnection);

            DataSet dataSet = new DataSet();
            command.Fill(dataSet);

            DataView dataView = new DataView(dataSet.Tables[0]);
            BaseGrid.ItemsSource = dataView;
        }

        [System.Obsolete]
        private void Button_Find_Click(object sender, RoutedEventArgs e)
        {
            SqlDataAdapter command = new SqlDataAdapter("SELECT * FROM [Sound] WHERE Answer = @Answer ", database.SqlConnection);
            command.SelectCommand.Parameters.Add("@Answer", FindBox.Text);
            DataSet dataSet = new DataSet();
            command.Fill(dataSet);

            DataView dataView = new DataView(dataSet.Tables[0]);
            BaseGrid.ItemsSource = dataView;
        }  
    }
}
