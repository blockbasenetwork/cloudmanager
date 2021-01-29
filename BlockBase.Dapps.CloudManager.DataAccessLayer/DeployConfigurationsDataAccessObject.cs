using Dapper;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.DataAccessLayer
{
    public class DeployConfigurationsDataAccessObject : BaseDataAccessObject
    {
        public async Task<int> RemoveNode(string account)
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                return await (con.ExecuteAsync(
                    "DELETE FROM DeployConfigurations WHERE Account = @account", new { account }));
            }
        }
    }
}
