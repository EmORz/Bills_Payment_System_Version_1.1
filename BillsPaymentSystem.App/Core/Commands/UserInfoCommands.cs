using BillsPaymentSystem.App.Core.Commands.Contracts;
using BillsPaymentSystem.Data;
using BillsPaymentSystem.Models.Enum;
using System;
using System.Linq;
using System.Text;

namespace BillsPaymentSystem.App.Core.Commands
{
    public class UserInfoCommands : ICommands
    {
        private readonly BillsPaymentSystemContext context;

        public UserInfoCommands(BillsPaymentSystemContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            int userId = int.Parse(args[0]);

            var user = context.Users
                .Select(a => new
                {
                    ID = a.UserId,
                    FullName = a.FirstName+" "+a.LastName,
                    BankAccounts = a.PaymentMethods.Where( x=> x.Type== PaymentType.BankAccount)
                        .Select(x => x.BankAccount)
                        .ToArray(),
                    CreditCards = a.PaymentMethods.Where(x => x.Type== PaymentType.CreditCard)
                        .Select(x => x.CreditCard)
                        .ToArray()
                })
                .FirstOrDefault(x => x.ID == userId);
            if (user == null)
            {
                throw  new ArgumentNullException($"User with id {userId} not found!");
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(user.FullName);
            sb.AppendLine("Bank Accounts:");
            var tempBank = user.BankAccounts.ToArray();
            if (tempBank.Length>0)
            {
                foreach (var bankAccount in tempBank)
                {
                    if (bankAccount != null)
                    {
                        sb.AppendLine("--ID:" + bankAccount.BankAccountId);
                        sb.AppendLine($"--- Balance: {bankAccount.Balance:F2}");
                        sb.AppendLine($"--- Bank: {bankAccount.BankName}");
                        sb.AppendLine($"--- SWIFT: {bankAccount.SWIFT}");
                    }
                
                }
            }
            //
            sb.AppendLine("Credit Cards:");
            var tempCredit = user.CreditCards.ToArray();
            if (tempCredit.Length>0)
            {
                foreach (var creditCard in tempCredit)
                {
                    if (creditCard != null)
                    {
                        sb.AppendLine("--ID:" + creditCard.CreditCardId);
                        sb.AppendLine($"--- Limit: {creditCard.Limit:f2}");
                        sb.AppendLine($"--- Money Owed: {creditCard.MoneyOwed:f2}");
                        sb.AppendLine($"--- Limit Left: {creditCard.LimitLeft:f2}");
                        sb.AppendLine($"--- Limit Left: {creditCard.ExpirationDate.ToString("yyyy/mm")}");
                    }
                }
            }
           var result = sb.ToString().Trim();
           return result;
        }
    }
}