using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronable.Sample
{
    public interface ITest : ICronable
    {

    }
    public class Test : ITest
    {
        public List<ICronJob> CronJobs { get; set; } = new();
        private readonly IInjectClass _injectedClass;
        public Test(IInjectClass injectedClass)
        {
            _injectedClass = injectedClass;
        }

        [CronJob("* * * * *")]
        public void TestMethod()
        {
            _injectedClass.Customers.ForEach(Console.WriteLine);
        }

        [CronJob("* * * * *")]
        public Task Test2Method()
        {
            return Task.Run(()=>_injectedClass.Customers.ForEach(Console.WriteLine));
        }
    }
}
