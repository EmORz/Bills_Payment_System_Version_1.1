using System;
using System.Threading;
using BillsPaymentSystem.App.Core.Commands.Contracts;
using BillsPaymentSystem.Data;

namespace BillsPaymentSystem.App.Core.Commands
{
    public class ExitCommands : ICommands
    {
        private readonly BillsPaymentSystemContext context;

        public ExitCommands(BillsPaymentSystemContext context)
        {
            this.context = context;
        }
        public string Execute(string[] args)
        {
            //Add aditional command for exit
            for (int i = 5 - 1; i >= 0; i--)
            {
                Console.WriteLine($"Program will close after {i} seconds!");
                Thread.Sleep(1000);
            }
            Environment.Exit(0);
            return null;
        }
    }
}