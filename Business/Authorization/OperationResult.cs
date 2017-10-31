using System;
using FuckThisNumber.Interfaces.Repository;

namespace Business
{
    public class OperationResult<TEntityResult> : IOperationResult<TEntityResult>
    {
        public TEntityResult Result { get; private set; }
        public EOperationResultStatus Status { get; private set; }
        public Exception Exception { get; private set; }

        public OperationResult(EOperationResultStatus eOperationResultStatus, TEntityResult result)
        {
            Result = result;
            Status = eOperationResultStatus;
        }

        public OperationResult(Exception exception)
        {
            Status = EOperationResultStatus.DataNotSaved;
            Exception = exception;
        }
    }

    public class OperationResult : IOperationResult
    {
        public EOperationResultStatus Status { get; private set; }
        public Exception Exception { get; private set; }

        public OperationResult(EOperationResultStatus operationResult)
        {
            Status = operationResult;
        }

        public OperationResult(Exception exception)
        {
            Status = EOperationResultStatus.DataNotSaved;
            Exception = exception;
        }
    }
}