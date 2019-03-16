using BillsPaymentSystem.Data;

namespace BillsPaymentSystem.App.Core.Contract
{
    public interface ICommandInterpreter
    {
        string Read(string[] args, BillsPaymentSystemContext context);
    }
}