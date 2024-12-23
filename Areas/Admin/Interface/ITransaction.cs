using UsersApp.Areas.Admin.ViewModel;

namespace UsersApp.Areas.Admin.Interface
{
    public interface ITransaction
    {
        public Task<List<TransactionDetailsViewModel>> GetAllDetailsViewModel(string? from, string? to);
        public Task<TransactionDetailsViewModel> GetPaymentDetailsById(int id);
        public Task<bool> InsertPaymentDetails(TransactionDetailsViewModel model);

    }
}
