namespace BlockBase.Dapps.CloudManager.DataAccessLayer
{
    public class DetailedRequesterPOCO
    {
        public string Account { get; set; }
        public string State { get; set; }
        public string IP { get; set; }
        public double AvgBlockReward { get; set; }
        public string CreatedAt { get; set; }
        public int NeededWorkers { get; set; }
        public bool InProduction { get; set; }
        public int Working { get; set; }

    }
}
