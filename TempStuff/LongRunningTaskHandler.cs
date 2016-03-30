using System;
using System.Timers;
using Timer = System.Timers.Timer;

namespace TempStuff
{
   public class LongRunningTaskHandler
   {
      private Timer _timer;
      private readonly Action _indicateProgress;
      private readonly double _intervalInMilliseconds;

      public LongRunningTaskHandler(Action indicateProgess, double intervalInMilliseconds)
      {
         _indicateProgress = indicateProgess;
         _intervalInMilliseconds = intervalInMilliseconds;
      }

      public void ExecuteLongRunningTask(Action longRunningTask)
      {
         StartTimer();

         longRunningTask.Invoke();

         StopTimer();
      }

      private void StopTimer()
      {
         _timer.Stop();
      }

      private void StartTimer()
      {
         _timer = new Timer();
         _timer.Elapsed += TimerElapsed;
         _timer.Interval = _intervalInMilliseconds;
         _timer.Start();
      }

      private void TimerElapsed(object source, ElapsedEventArgs e)
      {
         _indicateProgress.Invoke();
      }
   }
}
