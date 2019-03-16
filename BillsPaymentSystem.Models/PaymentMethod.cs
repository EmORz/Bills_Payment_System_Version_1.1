using BillsPaymentSystem.Models.Attribute;
using BillsPaymentSystem.Models.Enum;

namespace BillsPaymentSystem.Models
{
    public class PaymentMethod
    {
        //•	PaymentMethod:
        //o Id - PK
        //o   Type – enum (BankAccount, CreditCard)
        //o   UserId
        //o   BankAccountId
        //o   CreditCardId

        public int Id { get; set; }

        public PaymentType Type { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [Xor(nameof(CreditCardId))]
        public int? BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        public int? CreditCardId { get; set; }
        public CreditCard CreditCard { get; set; }



    }
}