﻿using BlockBase.Dapps.CloudManager.DataAccessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Business.Nodes
{
    public interface INodesBusinessObject
    {
        Task<OperationResult<List<RequesterPoco>>> GetAllRequestersAsync();
    }
}
