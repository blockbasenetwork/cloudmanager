using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Data
{
    public class SidechainState
    {
        public String currentRequesterStake { get; set; }
        public String stakeDepletionEndDate { get; set; }
        public String state { get; set; }
        public bool inProduction { get; set; }
        public ReservedSeats reservedSeats { get; set; }
        public NodeInfo fullProducersInfo { get; set; }
        public NodeInfo historyProducersInfo { get; set; }
        public NodeInfo validatorProducersInfo { get; set; }
    }

    public class NodeInfo
    {
        public int numberOfProducersRequired { get; set; }
        public int numberOfProducersInChain { get; set; }
        public int candidatesWaitingForSeat { get; set; }

        public int numberOfSlotsTakenByReservedSeats { get; set; }


    }

    public class ReservedSeats
    {
        public int totalNumber { get; set; }
        public int slotsTaken { get; set; }
        public int slotsStillAvailable { get; set; }
    }
}
