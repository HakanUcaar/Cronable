using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronable
{
    public interface ICronable
    {
        public List<ICronJob> CronJobs { get; set; }
    }
}
