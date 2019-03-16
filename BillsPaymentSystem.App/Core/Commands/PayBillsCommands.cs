using BillsPaymentSystem.App.Core.Commands.Contracts;
using BillsPaymentSystem.Data;
using BillsPaymentSystem.Models.Enum;
using System;
using System.Linq;

namespace BillsPaymentSystem.App.Core.Commands
{
    public class PayBillsCommands : ICommands
    {
        private readonly BillsPaymentSystemContext context;

        public PayBillsCommands(BillsPaymentSystemContext context)
        {
            this.context = context;
        }
        public string Execute(string[] args)
        {
            int userId = int.Parse(args[0]);
            var amount = decimal.Parse(args[1]);

            var user = context
                .Users.Select(users => new
                {
                    users.UserId,
                    BankAccounts = users.PaymentMethods
                        .Where(x => x.Type== PaymentType.BankAccount)
                        .Select(x => x.BankAccount)
                        .ToArray(),
                    CreditCards = users.PaymentMethods
                        .Where(x => x.Type == PaymentType.CreditCard)
                        .Select(x => x.CreditCard)
                        .ToArray()
                })
                .FirstOrDefault(x => x.UserId == userId);

            if (user == null)
            {
                throw new ArgumentNullException($"User with id {userId} not found!");
            }
            
            var bankAccountsTotal =
                user.BankAccounts.Sum(x => x.Balance);
   
            var creditCardTotal =
                user.CreditCards.Sum(x => x.LimitLeft);

            var totalAmount = bankAccountsTotal + creditCardTotal;

            if (totalAmount >= amount)
            {
                var bankAccounts =
                    user.BankAccounts.OrderBy(s => s.BankAccountId);
                foreach (var bankAccount in bankAccounts)
                {
                    if (bankAccount.Balance >= amount)
                    {
                        bankAccount.Withdraw(amount);
                        amount = 0;
                    }
                    else
                    {
                        amount -= bankAccount.Balance;
                        bankAccount.Withdraw(bankAccount.Balance);
                    }

                    if (amount == 0)
                    {
                        break;

                    }
                }
                var creditCards =
                    user.CreditCards.OrderBy(x => x.CreditCardId);

                foreach (var creditCard in creditCards)
                {
                    if (creditCard.LimitLeft >= amount)
                    {
                        creditCard.Withdraw(amount);
                        amount = 0;

                    }
                    else
                    {
                        amount -= creditCard.LimitLeft;
                        creditCard.Withdraw(creditCard.LimitLeft);
                    }

                    if (amount == 0)
                    {
                        break;

                    }
                }

            }

            context.SaveChanges();

            return "Bills are successfully payed. Thanks and have a nice day!";
        }
    }
}