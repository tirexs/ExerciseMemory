using System.Data.SqlClient;

namespace ExerciseMemory
{
    internal class Database
    {
        //public SqlConnection SqlConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Владелец\source\repos\ExerciseMemory\ExerciseMemory\Dictionary.mdf;Integrated Security = True");
        public SqlConnection SqlConnection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename={MainWindow.DatebasePath};Integrated Security = True");
        public void OpenConnection()
        {
            if (SqlConnection.State == System.Data.ConnectionState.Closed)
            {
                SqlConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (SqlConnection.State == System.Data.ConnectionState.Open)
            {
                SqlConnection.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            return SqlConnection;
        }
    }
}
