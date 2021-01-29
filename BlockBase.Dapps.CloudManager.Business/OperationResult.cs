using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Business
{
    public class OperationResult<T> : Operation
    {
        public T Result { get; set; }

       
        public OperationResult(T result)
        {
            Result = result;
            this.HasSucceeded = true;
        }

        public OperationResult(Exception e) : base(e) { }
    }
}
