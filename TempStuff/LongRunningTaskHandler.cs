using System;
using System.Timers;
using Timer = System.Timers.Timer;

namespace TempStuff
{
   public class LongRunningTaskHandler
   {
      private Timer _delayBeforeProgressTimer;
      private readonly double _delayInMilliseconds;

      private Timer _progressIndicationTimer;
      private readonly double _progressIndicationIntervalInMilliseconds;
      private readonly Action _indicateProgress;

      /// <summary>
      /// Initializes a new instance of the <see cref="LongRunningTaskHandler"/> class.
      /// </summary>
      /// <param name="indicateProgess">The action that indicates the progess.</param>
      /// <param name="progressIndicationIntervalInMilliseconds">The progress indication interval in milliseconds.</param>
      public LongRunningTaskHandler(Action indicateProgess, double progressIndicationIntervalInMilliseconds)
      {
         _indicateProgress = indicateProgess;
         _progressIndicationIntervalInMilliseconds = progressIndicationIntervalInMilliseconds;
         _delayInMilliseconds = 500;
      }

      public void ExecuteLongRunningTask(Action longRunningTask)
      {
         StartTimer();

         longRunningTask.Invoke();

         StopTimer();
      }

      private void StopTimer()
      {
         if (_progressIndicationTimer != null)
         {
            _progressIndicationTimer.Stop();
         }
      }

      private void StartTimer()
      {
         // Wait before indicating progress.
         _delayBeforeProgressTimer = new Timer();
         _delayBeforeProgressTimer.Elapsed += DelayBeforeProgressTimerElapsed;
         _delayBeforeProgressTimer.Interval = _delayInMilliseconds;
         _delayBeforeProgressTimer.Start();
      }

      private void DelayBeforeProgressTimerElapsed(object sender, ElapsedEventArgs e)
      {
         // The delay is over, stop that timer and start the timer that indicates progress.
         _delayBeforeProgressTimer.Stop();
         _delayBeforeProgressTimer.Elapsed -= DelayBeforeProgressTimerElapsed;

         _progressIndicationTimer = new Timer();
         _progressIndicationTimer.Elapsed += ProgressTimerElapsed;
         _progressIndicationTimer.Interval = _progressIndicationIntervalInMilliseconds;
         _progressIndicationTimer.Start();
      }

      private void ProgressTimerElapsed(object source, ElapsedEventArgs e)
      {
         // Invoke the action that indicates the progress.
         if (_indicateProgress != null)
         {
            _indicateProgress.Invoke();
         }
      }
   }
}
