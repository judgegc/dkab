using dkab.Extension;
using System;
using System.Threading;

namespace EmuClient
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandsProcessor proc = new CommandsProcessor();

            ICommand version = proc.Parse("version");
            Console.WriteLine(version.exec());


            proc.Parse("connect judgeGC LoadLibrary").exec();


            Console.WriteLine("Подключаемся...");
            ICommand isConnected = proc.Parse("iscon");
            while (isConnected.exec() != true.ToString().ToLower())
            {
                Thread.Sleep(250);
            }
            Console.WriteLine("Подключение установлено");

            while (true)
            {
                try
                {
                    ICommand events = proc.Parse("events");
                    string eventsList = events.exec();
                    if (eventsList.Length > 0)
                        Console.WriteLine(eventsList);

                    Thread.Sleep(200);

                    ICommand mypos = proc.Parse("mypos 5 5");
                    mypos.exec();

                    Thread.Sleep(300);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }             
            }
        }
    }
}
