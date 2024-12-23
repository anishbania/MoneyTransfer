using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UsersApp.Areas.Admin.ViewModel
{
    public class TransactionDetailsViewModel
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string? FirstNameSender { get; set; }
        [DisplayName("Middle Name")]
        public string? MiddleNameSender { get; set; }
        [DisplayName("Last Name")]
        public string? LastNameSender { get; set; }
        [DisplayName("Address")]
        public string? AddressSender { get; set; }
        [DisplayName("Recipient Email")]
        public string? RecipientEmail { get; set; }

        [DisplayName("Country")]
        public string? CountrySender { get; set; }
        [DisplayName("First Name")]
        public string? FirstNameReceiver { get; set; }
        [DisplayName("Middle Name")]
        public string? MiddleNameReceiver { get; set; }
        [DisplayName("Last Name")]
        public string? LastNameReceiver { get; set; }
        [DisplayName("Address")]
        public string? AddressReceiver { get; set; }
        [DisplayName("Country")]
        public string? CountryReceiver { get; set; }

        [DisplayName("Bank Name")]
        public string? BankName { get; set; }
        [DisplayName("Account Number")]
        public long? AccountNumber { get; set; }
        [DisplayName("Transfer Amount")]
        public double? TransferAmount { get; set; }
        [DisplayName("Exchange Rate")]
        public double? ExchangeRate { get; set; }
        [DisplayName("Payout Amount")]
        public double? PayoutAmount { get; set; }
        public string? Description { get; set; }
        public DateTime TransactionDate { get; set; }

    }
}
