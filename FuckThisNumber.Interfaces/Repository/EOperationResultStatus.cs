using FuckThisNumber.Interfaces.Repository;
using System.Text;

namespace FuckThisNumber.Interfaces.Repository
{
    public enum EOperationResultStatus
    {
        DataSaved,
        DataNotSaved,
        EntryAdded,
        EntryDeleted,
        EntryUpdated,
        WrongOperation
    }
}
