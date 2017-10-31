using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuckThisNumber.Interfaces
{
    public interface IAuthentificateResult
    {
        bool Authorized { get; set; }
        string message { get; set; }
    }
}
