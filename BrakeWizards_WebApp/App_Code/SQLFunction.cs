namespace BrakeWizards_WebApp.App_Code
{
    public class SQLFunction
    {
        private SQLFunction()
        {

        }

        public static string sqlConnection()
        {
            string connection = @"Server=localhost;Port=3306;Database=brakewizards_db;Uid=bwAdmin;Pwd=adminPass;";
            return connection;
        }
    }
}