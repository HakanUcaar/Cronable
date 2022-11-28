using Cronos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cronable
{
    public class CronJob : ICronJob, IDisposable
    {
        private System.Timers.Timer timer;

        public object Parent { get; set; }
        public MethodInfo Method { get; set; }
        public string Cron { get; set; }

        private readonly CronExpression cronExpression;

        public CronJob(string cron, object parent)
        {
            this.Cron = cron;
            this.cronExpression = CronExpression.Parse(cron);
            this.Parent = parent;
        }

        protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
        {
            var next = cronExpression.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Local);
            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;
                if (delay.TotalMilliseconds <= 0)   // prevent non-positive values from being passed into Timer
                {
                    await ScheduleJob(cancellationToken).ConfigureAwait(true);
                }
                timer = new System.Timers.Timer(delay.TotalMilliseconds);
                timer.Elapsed += async (sender, args) =>
                {
                    timer.Dispose();  // reset and dispose timer
                    timer = null;

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await DoWork(cancellationToken).ConfigureAwait(true);
                    }

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await ScheduleJob(cancellationToken).ConfigureAwait(true);    // reschedule next
                    }
                };
                timer.Start();
            }
            await Task.CompletedTask.ConfigureAwait(true);
        }

        public virtual async Task DoWork(CancellationToken cancellationToken)
        {
            Method.Invoke(Parent, null);
            await Task.Delay(50, cancellationToken).ConfigureAwait(true);  // do the work
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            await ScheduleJob(cancellationToken).ConfigureAwait(true);
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Stop();
            await Task.CompletedTask.ConfigureAwait(true);
        }
    }
}
