using System.ComponentModel.DataAnnotations;

namespace UsersApp.Areas.Admin.Models
{
    public class TransactionDetails
    {
        [Key]
        public int Id { get; set; }
        public string? FirstNameSender { get; set; }
        public string? MiddleNameSender { get; set; }
        public string? LastNameSender { get; set; }
        public string? AddressSender { get; set; }
        public string? CountrySender { get; set; }

        public string? FirstNameReceiver { get; set; }
        public string? MiddleNameReceiver { get; set; }
        public string? LastNameReceiver { get; set; }
        public string? RecipientEmail { get; set; }
        public string? AddressReceiver { get; set; }
        public string? CountryReceiver { get; set; }

        public string? BankName{ get; set; }
        public long? AccountNumber{ get; set; }
        public double? TransferAmount{ get; set; }
        public double? ExchangeRate{ get; set; }
        public double? PayoutAmount{ get; set; }
        public string? Description { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
