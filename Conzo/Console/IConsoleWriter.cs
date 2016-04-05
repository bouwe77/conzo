namespace Conzo.Console
{
   internal interface IConsoleWriter
   {
      void Initialize();
      void WriteToConsole(string stuffToWrite);
   }
}
