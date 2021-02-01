using BlockBase.Dapps.CloudManager.DataAccessLayer.Properties;
using Microsoft.Data.Sqlite;
using System.IO;


namespace BlockBase.Dapps.CloudManager.DataAccessLayer
{
    public abstract class BaseDataAccessObject
    {
        private string connectionString = Directory.GetCurrentDirectory() + Resources.DBLocation;

        public void TestDB() => connectionString = "C:\\Users\\User01\\source\\repos\\cloudmanager\\UnitTests\\DB.db";

        public SqliteConnectionStringBuilder GetConnectionStringBuilder()
        {
        
            var csb = new SqliteConnectionStringBuilder();
      
            csb.DataSource = connectionString;
            return csb;
        }
    }
}
