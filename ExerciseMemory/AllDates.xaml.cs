using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ExerciseMemory
{
    public partial class AllDates : Window
    {
        readonly Database database = new Database();
        public AllDates()
        {
            InitializeComponent();
            database.OpenConnection();

            BaseGrid.IsReadOnly = true;

            SqlDataAdapter command = new SqlDataAdapter("SELECT * FROM [dates]", database.SqlConnection);

            DataSet dataSet = new DataSet();
            command.Fill(dataSet);

            DataView dataView = new DataView(dataSet.Tables[0]);
            BaseGrid.ItemsSource = dataView;
        }

        [System.Obsolete]
        private void Button_Find1_Click(object sender, RoutedEventArgs e)
        {
            SqlDataAdapter command = new SqlDataAdapter("SELECT * FROM [dates] WHERE event = @event ", database.SqlConnection);
            command.SelectCommand.Parameters.Add("@event", FindBox1.Text);
            DataSet dataSet = new DataSet();
            command.Fill(dataSet);

            DataView dataView = new DataView(dataSet.Tables[0]);
            BaseGrid.ItemsSource = dataView;
        }
    }
}
