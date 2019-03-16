using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BillsPaymentSystem.Models.Attribute;

namespace BillsPaymentSystem.Models
{
    public class CreditCard
    {
        //•	CreditCard:
        //o CreditCardId
        //o Limit
        //o MoneyOwed
        //o LimitLeft(calculated property, not included in the database)
        //o ExpirationDate..

        public int CreditCardId { get; set; }


        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Limit { get; set; }


        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal MoneyOwed { get; set; }

        public decimal LimitLeft =>
            this.Limit - this.MoneyOwed;

        [ExpirationDate]
        public DateTime ExpirationDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        //public ICollection<PaymentMethod> PaymentMethods { get; set; }
        public void Withdraw(decimal amount)
        {
            if (this.LimitLeft - amount >= 0)
            {
                this.MoneyOwed += amount;
            }
        }

        public void Deposit(decimal amount)
        {
            if (amount >= 0)
            {
                this.MoneyOwed -= amount;
            }
        }

    }
}