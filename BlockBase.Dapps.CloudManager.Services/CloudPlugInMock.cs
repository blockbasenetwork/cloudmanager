using BlockBase.Dapps.CloudManager.Data;
using BlockBase.Dapps.CloudManager.DataAccessLayer;
using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Services
{
    public class CloudPlugInMock : BaseDataAccessObject/*so para efeitos mock depois tirar a referencia para DAL*/, ICloudPlugIn
    {
       

        private async Task SetNodeStatus(string account, string status)
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                await (con.ExecuteAsync(
                   "UPDATE CloudNodes SET Status = @status WHERE Account = @account", new { account, status }));
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
        public async Task RemoveNode(string account)
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                var myTransaction = await con.BeginTransactionAsync();
                await (con.ExecuteAsync(
                    "DELETE FROM CloudNodes WHERE Account = @account", new { account }));
                await myTransaction.CommitAsync();
            }
        }
        public async Task Duplicate(string account)
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                var myTransaction = await con.BeginTransactionAsync();
                await (con.ExecuteAsync(
                    "INSERT INTO CloudNodes (Account, Servicer, Ip, Status, Type) VALUES(@Account, @Status, @Type, @Service)", new { account }));
                await myTransaction.CommitAsync();
            }
        }
        public async Task<List<Node>> GetAllAsync()
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                var output = await (con.QueryAsync<Node>("Select * from CloudNodes", new DynamicParameters()));
                return output.ToList();
            }

        }
        public async Task<Node> GetNode(string account)
        {
                using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
                {
                    var output = await (con.QueryAsync<Node>("Select * from CloudNodes where Account = @account", new { account}));
                    return output.First();
                }
        }

        public async Task InsertAppSettingAsync(string node, string appSettings)
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                await (con.ExecuteAsync(
                   "UPDATE CloudNodes SET AppSettings = @appSettings WHERE Account = @node", new { node, appSettings }));
            }
        }

        public async Task<string> GetNodeSettingsAsync(string node)
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                var output = await (con.QueryAsync<string>(
                    "Select AppSettings from CloudNodes where Account=@node", new { node }));
                return output.First();
            }
        }

        public async Task<string> GetNodeIP(string node)
        {
            using (var con = new SqliteConnection(GetConnectionStringBuilder().ConnectionString))
            {
                var output = await(con.QueryAsync<string>(
                    "Select IP from CloudNodes where Account=@node", new { node }));
                return output.First();
            }
        }
    }
}
