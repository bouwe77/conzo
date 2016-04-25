namespace Conzo.Commands
{
   internal interface ICommandExecuter
   {
      bool TryExecute(out string commandContents);
   }
}
