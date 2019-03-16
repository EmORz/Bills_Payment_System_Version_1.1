namespace BillsPaymentSystem.App.Core.Commands.Contracts
{
    public interface ICommands
    {
        string Execute(string[] args);

    }
}