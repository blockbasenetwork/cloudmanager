

namespace BlockBase.Dapps.CloudManager.DataAccessLayer
{
    public class RequesterPOCO
    {
        public string Account { get; set; }
        public string Ip { get; set; }
        public string MonthlyCost { get; set; }
        public string State { get; set; }
        public string Balance { get; set; }
        public string Stake { get; set; }
        public double Health { get; set; }
        public override bool Equals(object obj)
        {
            var node = obj as RequesterPOCO;
            return this.Account == node.Account && this.MonthlyCost == node.MonthlyCost && this.Balance == node.Balance && this.Stake == node.Stake && this.Ip == node.Ip && this.State == node.State;
        }


    }
}
