using BlockBase.Dapps.CloudManager.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.DataAccessLayer
{
    public class NodeAppSettingsDataAccessObject : BaseDataAccessObject
    {

        public async Task InsertAppSettingAsync(string node, string appSettings)
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                await (con.ExecuteAsync(
                   "UPDATE NodeAppSettings SET AppSettings = @appSettings WHERE Node = @node", new { node, appSettings }));
            }
        }

        public async Task<NodeAppSettings> GetNodeSettingsAsync(string node)
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                var output = await (con.QueryAsync<NodeAppSettings>(
                    "Select * from NodeAppSettings where Node=@node", new { node }));
                return output.First();
            }
        }



    }
}
