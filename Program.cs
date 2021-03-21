using System;
using Topshelf;

namespace TestServiceKeepAwake
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            HostFactory.Run(serviceConfig =>
                            {

                                serviceConfig.UseNLog();

                                serviceConfig.Service<ConverterService>(serviceInstance =>
                                        {
                                            serviceInstance.ConstructUsing(
                                                () => new ConverterService());


                                            serviceInstance.WhenStarted(
                                                execute => execute.Start());

                                            serviceInstance.WhenStopped(
                                                execute => execute.Stop());

                                        });

                                serviceConfig.SetServiceName("AwesomeFileConverter");
                                serviceConfig.SetDisplayName("Awesome File Converter");
                                serviceConfig.SetDescription("A Pluralsight demo service");

                                serviceConfig.StartAutomatically();

                            }

            );


        }
    }
}
