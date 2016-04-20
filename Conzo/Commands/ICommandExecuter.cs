namespace Conzo.Commands
{
  interface ICommandExecuter<TCommand> where TCommand : CommandBase
  {
    void Execute();
  }
}
