using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronable
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CronJobAttribute : Attribute
    {
        public string Cron { get; set; }
        public CronJobAttribute(string cron) 
        { 
            Cron = cron;
        }
    }
}
