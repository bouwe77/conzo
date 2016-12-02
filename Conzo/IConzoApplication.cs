using System;
using Conzo.Commands;

namespace Conzo
{
   public interface IConzoApplication
   {
      CommandConfiguration Configure(CommandBase command);
      void AddGlobalCommand(ConsoleKey key, CommandBase command);
      void Start();
      void Stop();
   }
}
