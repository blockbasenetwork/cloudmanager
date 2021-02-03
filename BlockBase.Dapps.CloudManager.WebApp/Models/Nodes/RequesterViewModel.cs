

using BlockBase.Dapps.CloudManager.DataAccessLayer;

namespace BlockBase.Dapps.CloudManager.WebApp.Models.Nodes
{
    public class RequesterViewModel
    {
        public string Title { get; set; }
        public string State { get; set; }
        public double AvgBlockReward { get; set; }
        public string CreatedAt { get; set; }
        public int NeededWorkers { get; set; }
        public int Working { get; set; }
        public string Network { get; set; }
        public int Version { get; set; }
        public int TotalBlocks { get; set; }
        public int BlockHead { get; set; }
        public bool InProduction { get; set; }


        public RequesterViewModel(DetailedRequesterPOCO dr)
        {
            this.Title = dr.Account;
            this.State = dr.State;
            this.AvgBlockReward = dr.AvgBlockReward;
            this.NeededWorkers = dr.NeededWorkers;
            this.Working = dr.Working;
            this.InProduction = dr.InProduction;
        }
    }
}
