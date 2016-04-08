## Introduction

Conzo is a framework for creating user friendly .NET console applications.

## Features


## Installation

TODO: Describe the installation process

## Usage

### Quickstart

```C#
static void Main()
{
  // The command that must be invoked when the application starts.
  // This is a method that (at least) returns a string but also could do something useful like quering a database.
  var startCommand = new Command(() => "Hello World");
  
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

### More advanced example: next commands

```C#
static void Main()
{
  var startCommand = new Command(() => "Hello World");
  
  var settings = new Settings(startCommand)

  var myApp = ConsoleApplication.Create(settings);
  
  // Create a next command. After the startCommand is invoked and displayed you can press A to continue to the next command.
  var nextCommand = new Command(() => "This is the next command");
  myApp.Configure(startCommand)
    .AddNextCommand(ConsoleKey.A, nextCommand);
  
  // When the next command is invoked and displayed, you can press B to return to the startCommand.
  myApp.Configure(nextCommand)
    .AddNextCommand(ConsoleKey.B, startCommand);
  
  // Run the application which means the startAction will be invoked and the string value will be displayed.
  myApp.Run();
}
```

## License

TODO: Write license
