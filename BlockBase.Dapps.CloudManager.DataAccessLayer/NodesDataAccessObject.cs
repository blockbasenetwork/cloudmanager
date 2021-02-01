using BlockBase.Dapps.CloudManager.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlockBase.Dapps.CloudManager.DataAccessLayer
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

        public async Task<Node> GetNodeAsync(string node)
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                var output = await (con.QueryAsync<Node>("Select * from Nodes where Account = @node", new { node}));
                return output.First();
            }
        }

        public async Task RemoveNode(string account)
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                var myTransaction = await con.BeginTransactionAsync();
                await (con.ExecuteAsync(
                    "DELETE FROM Nodes WHERE Account = @account", new { account }));
                await myTransaction.CommitAsync();
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
        public async Task<List<RequesterPOCO>> GetAllRequestersAsync()
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                var output = await (con.QueryAsync<RequesterPOCO>("Select Account, IP from Nodes where Type = 'Full' or Type = 'Requester'", new DynamicParameters()));
                return output.ToList();
            }
        }

        public async Task<List<ProducerPOCO>> GetAllProducersAsync()
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                var output = await (con.QueryAsync<ProducerPOCO>("Select Account, IP, Type from Nodes where Type = 'Full' or Type = 'Producer'", new DynamicParameters()));
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
