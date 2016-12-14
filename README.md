# Conzo

## Introduction

Conzo is a library for creating user friendly .NET console applications.

Conzo applications are keyboard driven applications. You define in your 
code which key executes which command and what output is displayed.

You could even use Conzo for building MVC console applications.

Unfortunately, input fields for free text entering are not supported yet,
but they are on the road map.

Conzo does not support command-line statements.

## How it works

When you build a console application with Conzo you basically 
implement a *state machine*. In Conzo state transitions are triggered
by hitting a *key on the keyboard* (i.e. ConsoleKey in C#).
Then a transition to another state is triggered, which means a C# method
(i.e. a Func of string) is called. This method is called a *Command* and
can do something useful, e.g. querying a database, but will always result 
in returning some kind of string that will be displayed on screen.

When creating a Conzo application you must at least define one command, 
the start command. It's the command that is executed when the application 
starts. A Conzo application also has one default command for quitting the
application and that is the Escape key. In the examples below you will see 
how to define another key for quitting.


## Examples  

### Example 1: Hello World

Let's create a bare minimum Conzo application that only displays "Hello World":

```C#
static void Main()
{
   // Create a command that returns the text "Hello World".
   var helloWorldCommand = new Command(() => "Hello World");

   // Create a new ConzoApplication with the Hello World command as start command.
   var myApp = new ConzoApplication(helloWorldCommand);

   // Start the application which means the start command will be invoked 
   // and the "Hello World" string that is returned will be displayed.
   myApp.Start();
}
```

When you run this example the output is as follows:

```
Hello World
```

### Example 2: Display current time

Let's replace the Hello World expression by a method that
returns the current time:

```C#
static string DisplayTime()
{
   return $"Time: {DateTime.Now.ToString("hh:mm:ss")}";
}

static void Main()
{
   var displayTimeCommand = new Command(DisplayTime);
   var myApp = new ConzoApplication(displayTimeCommand);
   myApp.Start();
}
```

```
Time: 12:34:56
```

### Example 3: Adding a command

Now it is time for implementing our first command. The user
can press R to refresh the time:

```C#
static string DisplayTime()
{
   return $"Time: {DateTime.Now.ToString("hh:mm:ss")} (Press R to refresh)";
}

static void Main()
{
   var displayTimeCommand = new Command(DisplayTime);
   var myApp = new ConzoApplication(displayTimeCommand);

   // Configure the displayTimeCommand by triggering the same command 
   // when the R key is pressed.
   myApp.Configure(displayTimeCommand)
      .AddNextCommand(ConsoleKey.R, displayTimeCommand);

   myApp.Start();
}
```

When you run this example (and after each time the R button is pressed) the output is as follows:

```
Time: 12:34:56 (Press R to refresh)
```


### Example 4: Modify display settings

In the code below the quit key and text and background colors are changed.

```C#
static string DisplayTime()
{
   return $"Time: {DateTime.Now.ToString("hh:mm:ss")} (Press R to refresh)";
}

static void Main()
{
   var displayTimeCommand = new Command(DisplayTime);
   var myApp = new ConzoApplication(displayTimeCommand);

   myApp.Configure(displayTimeCommand)
      .AddNextCommand(ConsoleKey.R, displayTimeCommand);

   // Change some settings:
   myApp.QuitKey = ConsoleKey.F10;
   myApp.Layout = new LayoutSettings
   {
      BackgroundColor = ConsoleColor.DarkBlue,
      TextColor = ConsoleColor.White
   };

   myApp.Start();
}
```

When you run this example the output is he same as the previous example, but the colors are different.


*TODO Add example for adding conditional commands*

*TODO Add example for global commands*

*TODO Add example for a custom template provider*

*TODO Add example for a MVC-like approach*

## Installation
Download the source code and build it in Visual Studio. Then create a console application, add a reference to Conzo.dll and start coding!
