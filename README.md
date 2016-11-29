## Introduction

Conzo is a library for creating user friendly .NET console applications.

When you implement a console application with Conzo you basically create a state machine, where the states are the "screens" that are displayed. The user moves from one screen to another by pressing keyboard keys. So it is suitable for creating "menu driven" (i.e. multiple choice) applications. Ofcourse, when you press a key not only a screen is shown, you could also implement an action that must be executed, for example a database query.

At this moment input fields for free text entering are not supported yet.

<!---
## Features
TODO
--->

## Installation
Download the source code and build in Visual Studio. Then add a reference to Conzo.dll in your code and start coding.

## Examples

### Hello World

```C#
static void Main()
{
  // The command that must be invoked when the application starts.
  // This is a method that (at least) returns a string that will be displayed on the console.
  // However, besides that it could also do something useful like quering a database.
  var startCommand = CommandFactory.Create(() => "Hello World");
  
  // We need a Settings object containing at least the startCommand, but you could configure more if you want.
  var settings = new Settings(startCommand)
  {
    // configure more stuff...
  };
  
  // Create a new ConsoleApplication with the settings.
  var myApp = ConsoleApplication.Create(settings);
  
  // Run the application which means the startAction will be invoked and the string value will be displayed.
  myApp.Run();
}
```
TODO add screenshot

### Add more commands

```C#
static void Main()
{
  // Start with the application from the HelloWorld example.
  var startCommand = CommandFactory.Create(() => "Hello World, press A to continue...");
  var settings = new Settings(startCommand);
  var myApp = ConsoleApplication.Create(settings);
  
  // Now we create a new command and add it as the next command for startCommand.
  // After the startCommand is invoked and displayed the user can press A to continue to the next command.
  var nextCommand = CommandFactory.Create(() => "This is the next command, press B to continue...");
  myApp.Configure(startCommand)
    .AddNextCommand(ConsoleKey.A, nextCommand);
  
  // When the next command is invoked and displayed, the user can press B to return to the startCommand.
  myApp.Configure(nextCommand)
    .AddNextCommand(ConsoleKey.B, startCommand);
  
  // Run the application which means the startAction will be invoked and the string value will be displayed.
  myApp.Run();
}
```

TODO add screenshot
