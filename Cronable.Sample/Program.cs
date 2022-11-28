using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cronable.Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                                   .AddTransient<IInjectClass, InjectClass>()
                                   .AddTransient<ITest, Test>()
                                   .BuildServiceProvider();

            var test = serviceProvider.GetService<ITest>();
            test.RegisterJobs().StartJobsAsync(CancellationToken.None);

            var input = "";
            
            while (true)
            {
                Console.WriteLine("Bir değer girin");
                input = Console.ReadLine();
                if(input == "exit")
                {
                    break;
                }
                Console.WriteLine(input);
            }            
            
        }
    }
}
