using Microsoft.Data.Sqlite;
using System.IO;


namespace BlockBase.Dapps.CloudManager.Dal
{
    public abstract class BaseDataAccessObject
    {
        public SqliteConnectionStringBuilder GetConnectionStringBuilder()
        {
            var csb = new SqliteConnectionStringBuilder();
            csb.DataSource = Directory.GetCurrentDirectory() + "\\..\\BlockBase.Dapps.CloudManager.Data\\DB.db";
            return csb;
        }
    }
}
