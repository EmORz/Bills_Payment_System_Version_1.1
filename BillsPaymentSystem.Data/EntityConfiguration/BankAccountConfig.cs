using BillsPaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillsPaymentSystem.Data.EntityConfiguration
{
    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            //o BankName(up to 50 characters, unicode)
            //o SWIFT Code(up to 20 characters, non-unicode)

            builder.Property(x => x.BankName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.SWIFT)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsRequired();
        }
    }
}