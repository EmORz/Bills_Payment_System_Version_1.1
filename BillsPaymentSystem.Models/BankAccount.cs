using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BillsPaymentSystem.Models
{
    public class BankAccount
    {
        //•	BankAccount:
        //o BankAccountId
        //o Balance
        //o BankName(up to 50 characters, unicode)
        //o SWIFT Code(up to 20 characters, non-unicode)
        [Key]
        public int BankAccountId { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Balance { get; set; }

        [MinLength(3), MaxLength(50)]
        public string BankName { get; set; }

        [MinLength(3), MaxLength(20)]
        public string SWIFT { get; set; }

        public PaymentMethod PaymentMethod { get; set; }


        //Add Withdraw(int userId, decimal amount) and Deposit(int userId, decimal amount) 
        // public ICollection<PaymentMethod> PaymentMethods { get; set; }

        public void Withdraw(decimal amount)
        {
            if (this.Balance - amount>= 0)
            {
                this.Balance -= amount;
            }
        }

        public void Deposit(decimal amount)
        {
            if (amount >= 0)
            {
                this.Balance += amount;
            }
        }

    }
}