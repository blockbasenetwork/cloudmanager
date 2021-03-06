﻿using BlockBase.Dapps.CloudManager.Services;
using System;

using System.Threading.Tasks;
using System.Transactions;

namespace BlockBase.Dapps.CloudManager.Business
{
    public abstract class BaseBusinessObject
    {
        public async Task<OperationResult<T>> ExecuteFunction<T>(Func<Task<T>> func)
        {
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var result = await func.Invoke();
                    scope.Complete();
                    return new OperationResult<T>(result);
                }
            }
            catch (Exception e)
            {
                return new OperationResult<T>(e);
            }
        }

        public async Task<Operation> ExecuteAction(Func<Task> action)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await action.Invoke();
                    scope.Complete();
                    return new Operation() { HasSucceeded = true };
                }
            }
            catch (Exception e)
            {
                return new Operation() { HasSucceeded = false, Exception = e };
            }
        }

        protected ICloudPlugIn _cloudPlugin;
        public BaseBusinessObject()
        {
            _cloudPlugin = new CloudPlugInMock();
        }

    }
}
