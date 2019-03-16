using BillsPaymentSystem.App.Core.Commands.Contracts;
using BillsPaymentSystem.App.Core.Contract;
using BillsPaymentSystem.Data;
using System;
using System.Linq;
using System.Reflection;

namespace BillsPaymentSystem.App.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private const string Suffix = "Commands";
        public string Read(string[] args, BillsPaymentSystemContext context)
        {
            string command = args[0];
            string[] commandAgs = args.Skip(1).ToArray();

            var type = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(n => n.Name == command + Suffix);
            if (type == null)
            {
                throw  new ArgumentNullException("Invalid command!");
            }
            var typeInstance = Activator.CreateInstance(type, context);

            var result = ((ICommands) typeInstance).Execute(commandAgs);

            return result;
        }
    }
}