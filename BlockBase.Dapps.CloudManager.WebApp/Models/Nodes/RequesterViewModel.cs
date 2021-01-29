
using BlockBase.Dapps.CloudManager.DataAccessLayer;
using System;


namespace BlockBase.Dapps.CloudManager.WebApp.Models.Nodes
{
    public class RequesterViewModel
    {
        public RequesterViewModel(RequesterPoco node)
        {
            this.Account = node.Account;
            this.Ip = node.Ip;
            this.Type = node.Type;
            this.Balance = node.Balance;
            this.MonthlyCost = node.MonthlyCost;
            this.State = node.State;
            this.Stake = node.Stake;



        }
        public String Account { get; set; }
        public String Type { get; set; }
        public String Ip { get; set; }
        public String MonthlyCost { get; set; }
        public String State { get; set; }
        public String Balance { get; set; }
        public String Stake { get; set; }

    }
}
