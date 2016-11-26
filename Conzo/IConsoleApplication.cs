using System;
using Conzo.Commands;

namespace Conzo
{
   public interface IConsoleApplication
   {
      CommandConfiguration Configure(CommandBase command);
      void AddGlobalCommand(ConsoleKey key, CommandBase command);
      void Run();
      void Stop();
   }
}
