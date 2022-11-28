# Cronable
Add abilities to your classes

## Usage
Implement
``` csharp
    public interface ITest : ICronable
    {
    }
    
    public class Test : ITest
    {
        public List<ICronJob> CronJobs { get; set; } = new();
      
        //My Attribute trigger every minute
        [CronJob("* * * * *")]
        public void TestMethod()
        {
            Console.WriteLine("TestMethod execute");
        }
        
        //My Attribute trigger every minute
        [CronJob("* * * * *")]
        public Task Test2Method()
        {
            return Task.Run(()=> Console.WriteLine("TestMethod2 execute"));
        }
    }  
```

Start jobs
``` csharp
  testClass.RegisterJobs().StartJobsAsync(CancellationToken.None);
```

Output
```
  TestMethod execute
  TestMethod2 execute
```

