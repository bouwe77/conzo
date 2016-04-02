using Conzo.Screens;

namespace Conzo
{
   public interface IConsoleApplication
   {
      ScreenConfiguration AddOrUpdateScreen(Screen screen);
      void Start();
      void Stop();
   }
}
