using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cronable
{
    public interface ICronJob
    {
        public MethodInfo Method { get; set; }
        public string Cron { get; set; }
        public Task StartAsync(CancellationToken cancellationToken);
    }
}
