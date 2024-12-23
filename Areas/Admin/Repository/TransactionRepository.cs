using Azure;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using UsersApp.Areas.Admin.Interface;
using UsersApp.Areas.Admin.Models;
using UsersApp.Areas.Admin.ViewModel;
using UsersApp.Data;
using UsersApp.Models;

namespace UsersApp.Areas.Admin.Repository
{
    public class TransactionRepository : ITransaction
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TransactionRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<TransactionDetailsViewModel>> GetAllDetailsViewModel(string? from , string? to)
        {
            var suchi = await (from x in _context.TransactionDetails
                               select new TransactionDetailsViewModel
                               {
                                   Id = x.Id,
                                   FirstNameSender = x.FirstNameSender,
                                   MiddleNameSender = x.MiddleNameSender,
                                   LastNameSender = x.LastNameSender,
                                   AddressSender = x.AddressSender,
                                   CountrySender = x.CountrySender,

                                   FirstNameReceiver = x.FirstNameReceiver,
                                   MiddleNameReceiver = x.MiddleNameReceiver,
                                   LastNameReceiver = x.LastNameReceiver,
                                   RecipientEmail = x.RecipientEmail,
                                   AddressReceiver = x.AddressReceiver,
                                   CountryReceiver = x.CountryReceiver,

                                   BankName = x.BankName,
                                   AccountNumber = x.AccountNumber,
                                   TransferAmount = x.TransferAmount,
                                   ExchangeRate = x.ExchangeRate,
                                   PayoutAmount = x.PayoutAmount,
                                   Description = x.Description,
                                   TransactionDate = x.TransactionDate
                               }).ToListAsync();

            if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to) && suchi.Count > 0)
                if (DateTime.Parse(from) <= DateTime.Parse(to))
                    suchi = suchi.Where(x => (x.TransactionDate) >= DateTime.Parse(from) && (x.TransactionDate) <= DateTime.Parse(to)).ToList();

            return suchi;
        }
        public async Task<TransactionDetailsViewModel> GetPaymentDetailsById(int id)
        {
            return (await _context.TransactionDetails.Where(x => x.Id == id).Select(x => new TransactionDetailsViewModel()
            {
                Id = x.Id,
                FirstNameSender = x.FirstNameSender,
                MiddleNameSender = x.MiddleNameSender,
                LastNameSender = x.LastNameSender,
                AddressSender = x.AddressSender,
                CountrySender = x.CountrySender,

                FirstNameReceiver = x.FirstNameReceiver,
                MiddleNameReceiver = x.MiddleNameReceiver,
                LastNameReceiver = x.LastNameReceiver,
                RecipientEmail = x.RecipientEmail,
                AddressReceiver = x.AddressReceiver,
                CountryReceiver = x.CountryReceiver,

                BankName = x.BankName,
                AccountNumber = x.AccountNumber,
                TransferAmount = x.TransferAmount,
                ExchangeRate = x.ExchangeRate,
                PayoutAmount = x.PayoutAmount,
                Description = x.Description,
                TransactionDate = x.TransactionDate


            }).FirstOrDefaultAsync() ?? new TransactionDetailsViewModel());
        }

        public async Task<bool> InsertPaymentDetails(TransactionDetailsViewModel model)
        {
            using (var transection = _context.Database.BeginTransaction())
            {
                try { 
                if (model.Id == 0)
                {
                    TransactionDetails data = new TransactionDetails()
                    {
                        Id = model.Id,
                        FirstNameSender = model.FirstNameSender,
                        MiddleNameSender = model.MiddleNameSender,
                        LastNameSender = model.LastNameSender,
                        AddressSender = model.AddressSender,
                        CountrySender = model.CountrySender,

                        FirstNameReceiver = model.FirstNameReceiver,
                        MiddleNameReceiver = model.MiddleNameReceiver,
                        LastNameReceiver = model.LastNameReceiver,
                        RecipientEmail = model.RecipientEmail,
                        AddressReceiver = model.AddressReceiver,
                        CountryReceiver = model.CountryReceiver,

                        BankName = model.BankName,
                        AccountNumber = model.AccountNumber,
                        TransferAmount = model.TransferAmount,
                        ExchangeRate = model.ExchangeRate,
                        PayoutAmount = model.PayoutAmount,
                        Description = model.Description,
                        TransactionDate = DateTime.UtcNow,


                    };
                    await _context.TransactionDetails.AddAsync(data);
                    await _context.SaveChangesAsync();

                }
                await transection.CommitAsync();
            }
                catch (Exception)
                {
                   
                    await transection.RollbackAsync();
                    return false;
                }
                return true;

            }
        }

    }
}
