using System;
using Conzo.Commands;

namespace Conzo
{
   public interface IConsoleApplication
   {
      CommandConfiguration Configure(Command command);
      void AddGlobalCommand(ConsoleKey key, Command command);
      void Start();
      void Stop();
   }
}
