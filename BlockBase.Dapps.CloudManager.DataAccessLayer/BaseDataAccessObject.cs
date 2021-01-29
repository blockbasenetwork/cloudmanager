using Microsoft.Data.Sqlite;
using System.IO;


namespace BlockBase.Dapps.CloudManager.DataAccessLayer
{
    public abstract class BaseDataAccessObject
    {
        public SqliteConnectionStringBuilder GetConnectionStringBuilder()
        {
        
            var csb = new SqliteConnectionStringBuilder();
            //csb.DataSource = Directory.GetCurrentDirectory() + "\\..\\BlockBase.Dapps.CloudManager.Data\\DB.db";
            csb.DataSource = "C:\\Users\\User01\\Source\\Repos\\cloudmanager\\BlockBase.Dapps.CloudManager.Data\\DB.db";
            return csb;
        }
    }
}
