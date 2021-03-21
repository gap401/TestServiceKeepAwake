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

                                            serviceInstance.WhenPaused(
                                                execute => execute.Pause());

                                            serviceInstance.WhenContinued(
                                                execute => execute.Continue());

                                            serviceInstance.WhenCustomCommandReceived(
                                                (execute, hostControl, commandNumber) =>
                                                 execute.CustomCommand(commandNumber));

                                        });

                                serviceConfig.EnableServiceRecovery(recoveryOption =>
                                        {
                                            recoveryOption.RestartService(1);
                                            recoveryOption.RestartComputer(60, "Service Demo");
                                            recoveryOption.RunProgram(5, @"c:\temp\someprogram.exe");
                                        });

                                serviceConfig.EnablePauseAndContinue();

                                serviceConfig.SetServiceName("AwesomeFileConverter");
                                serviceConfig.SetDisplayName("Awesome File Converter");
                                serviceConfig.SetDescription("A Pluralsight demo service");

                                serviceConfig.StartAutomatically();

                            }

            );


        }
    }
}
