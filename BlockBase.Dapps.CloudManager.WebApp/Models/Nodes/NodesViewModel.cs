﻿
using BlockBase.Dapps.CloudManager.DataAccessLayer;
using System;
using System.Collections.Generic;

namespace BlockBase.Dapps.CloudManager.WebApp.Models.Nodes
{
    public class NodesViewModel
    {
        public NodesViewModel(List<RequesterPOCO> requesters, List<ProducerPOCO> producers)
        {
            this.Producers = producers;
            this.Requesters = requesters;
        }
        public List<RequesterPOCO> Requesters { get; set; }
        public List<ProducerPOCO> Producers { get; set; }

    }
}