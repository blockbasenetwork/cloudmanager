using System;
using System.Collections.Generic;
using System.Text;
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
                using (TransactionScope scope = new TransactionScope())
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


    }
}
