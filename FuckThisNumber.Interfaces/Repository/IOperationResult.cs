using System;

namespace FuckThisNumber.Interfaces.Repository
{
    public interface IOperationResult<TEntity> : IOperationResult
    {
        TEntity Result { get; }
    }

    public interface IOperationResult
    {
        EOperationResultStatus Status { get; }
        Exception Exception { get; }
    }
}