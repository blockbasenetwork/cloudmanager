using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Business
{
    public class OperResult<T> : Operation
    {
        public T Result { get; set; }

       
        public OperResult(T result)
        {
            Result = result;
            this.HasSucceeded = true;
        }

        public OperResult(Exception e) : base(e) { }
    }
}
