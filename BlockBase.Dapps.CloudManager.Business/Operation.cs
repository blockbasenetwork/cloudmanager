using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Business
{
    public class Operation
    {
        public bool HasSucceeded { get; set; }
        public Exception Exception { get; set; }
        public Operation()
        {

        }

        public Operation(Exception e)
        {
            Exception = e;
            this.HasSucceeded = false;
        }
    }
}
