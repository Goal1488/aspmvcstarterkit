using FuckThisNumber.Interfaces;

namespace Models.Models
{
    public class AuthentificateResult : IAuthentificateResult
    {
        public bool Authorized { get; set; }
        public string message { get; set; }
    }
}
