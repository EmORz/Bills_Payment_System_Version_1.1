using System;
using BillsPaymentSystem.App.Core;
using BillsPaymentSystem.App.Core.Contract;
using BillsPaymentSystem.App.Initializer;
using BillsPaymentSystem.Data;

namespace BillsPaymentSystem.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            //using (BillsPaymentSystemContext context = new BillsPaymentSystemContext())
            //{
            //    DbInitializer.Seed(context);
            //}

            //Console.WriteLine("Good Job!");
            ICommandInterpreter commandInterpreter = new CommandInterpreter();
            IEngine engine = new Engine(commandInterpreter);
            engine.Run();
        }
    }
}
