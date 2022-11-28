using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cronable
{
    public static class CronableExtension
    {
        public static ICronable AddJob(this ICronable context, ICronJob job)
        {
            context.CronJobs.Add(job);
            return context;
        }

        public static ICronable RegisterJobs(this ICronable context)
        {
            IEnumerable<MethodInfo> methodInfos = context.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(x=>x.GetCustomAttribute<CronJobAttribute>() != null);
            foreach (MethodInfo methodInfo in methodInfos)
            {
                if(methodInfo.GetParameters().Length > 0)
                {
                    throw new MethodParameterException(methodInfo);
                }

                var job = new CronJob(methodInfo.GetCustomAttribute<CronJobAttribute>().Cron,context);
                job.Method = methodInfo;

                context.AddJob(job);
            }

            return context;
        }

        public static async Task StartJobsAsync(this ICronable context, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var job in context.CronJobs)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    try
                    {
                        await job.StartAsync(cancellationToken);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
