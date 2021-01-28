using BlockBase.Dapps.CloudManager.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlockBase.Dapps.CloudManager.Dal
{
    public class NodesDataAccessObject : BaseDataAccessObject
    {
        public async Task<List<Node>> GetAllAsync()
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                var output = await (con.QueryAsync<Node>("Select * from Nodes", new DynamicParameters()));
                return output.ToList();
            }

        }

        public async Task RemoveNode(string account)
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                await (con.ExecuteAsync(
                    "DELETE FROM Nodes WHERE Account = @account", new { account }));
            }
        }
        private async Task SetNodeStatus(string account, string status)
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                 await (con.ExecuteAsync(
                    "UPDATE Nodes SET Status = @status WHERE Account = @account", new { account, status}));
            }
        }
        public async Task StartNodeAsync(string account)
        {
              await SetNodeStatus(account, "ON");
        }
        public async Task StopNodeAsync(string account)
        {
               await SetNodeStatus(account, "OFF");
        }
        public async Task<List<RequesterPoco>> GetAllRequestersAssync()
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                var output = await (con.QueryAsync<RequesterPoco>("Select Account, IP, Type from Nodes where Type = 'Full' or Type = 'Requester'", new DynamicParameters()));
                return output.ToList();
            }
        }

        public async void InsertAsync(Node n)
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                await con.ExecuteAsync("insert into Nodes (Account, Status, Type, Service) values (@Account, @Status, @Type, @Service)", n);
            }
        }


    }
}
