using BillsPaymentSystem.Data;
using BillsPaymentSystem.Models;
using BillsPaymentSystem.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BillsPaymentSystem.App.Initializer
{
    public class DbInitializer
    {
        public static void Seed(BillsPaymentSystemContext context)
        {
            SeedUser(context);
            SeedCreditCard(context);
            SeedBankAccounts(context);
            SeedPaymentMethod(context);
        }

        private static void SeedPaymentMethod(BillsPaymentSystemContext context)
        {
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();
            for (int i = 0; i < 3; i++)
            {
                var paymentMethod = new PaymentMethod
                {
                    UserId = new Random().Next(1, 5),
                    Type =  (PaymentType)new Random().Next(0, 2)
                };
                if (i % 3 == 0)
                {
                    paymentMethod.CreditCardId = new Random().Next(1, 5);
                    paymentMethod.BankAccountId = new Random().Next(1, 5);

                }
                else if (i % 2 == 0)
                {
                    paymentMethod.CreditCardId = new Random().Next(1, 5);
                }
                else
                {
                    paymentMethod.BankAccountId = new Random().Next(1, 5);
                }
                if (!IsValid(paymentMethod))
                {
                    continue;
                }
                paymentMethods.Add(paymentMethod);
            }
            context.PaymentMethods.AddRange(paymentMethods);
            context.SaveChanges();

        }

        private static void SeedBankAccounts(BillsPaymentSystemContext context)
        {
            List<BankAccount> bankAccounts = new List<BankAccount>();
            for (int i = 0; i < 8; i++)
            {
                var bankAccount = new BankAccount
                {
                    Balance = new Random().Next(-200,200),
                    BankName = "PostBank"+i,
                    SWIFT = "SWIFT"+i+1
                };
                if (!IsValid(bankAccount))
                {
                    continue;
                }
                bankAccounts.Add(bankAccount);
            }
            context.bankAccounts.AddRange(bankAccounts);
            context.SaveChanges();
        }

        private static void SeedCreditCard(BillsPaymentSystemContext context)
        {
            List<CreditCard> creditCards = new List<CreditCard>();
            for (int i = 0; i < 8; i++)
            {
               var creditCard = new CreditCard
               {
                   Limit =  new Random().Next(-25000, 25000),
                   MoneyOwed =  new Random().Next(-25000, 25000),
                   ExpirationDate =  DateTime.Now.AddDays(new Random().Next(-200, 200))
               };
                if (!IsValid(creditCard))
                {
                    continue;
                }
                creditCards.Add(creditCard);
            }
            context.CreditCards.AddRange(creditCards);
            context.SaveChanges();
        }

        private static void SeedUser(BillsPaymentSystemContext context)
        {
            string[] firstName = { "Gosho", "Pesho", "Tosho", "Nikolai", null, ""};
            string[] lastName = { "Goshev", "Peshev", "Toshev", "Nikolaiev", null, "ErRoR" };
            string[] email = { "Goshev@cia.com", "Phev@cia.com", "Tev@cia.com", "Niaiev@cia.com", null, "Error" };
            string[] password = { "Goshcom9", "Pecom9", "Tecom9", "Nascom9", null, "Error" };

            List<User> users = new List<User>();

            for (int i = 0; i < firstName.Length; i++)
            {
                User user = new User
                {
                    FirstName = firstName[i],
                    LastName = lastName[i],
                    Email = email[i],
                    Password = password[i]
                };

                if (!IsValid(user))
                {
                    continue;
                }
                users.Add(user);

            }

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        private static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity, validationContext,validationResult, true );

            return isValid;
        }





    }
}