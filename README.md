## Introduction

Conzo is a library for creating user friendly .NET console applications.

When you build a console application with Conzo you basically 
implement a *state machine*. In Conzo state transitions are triggered
by hitting a *key on the keyboard* (i.e. ConsoleKey in C#).
Then a transition to another state is triggered, which means a C# method
(i.e. a Func of string) is called which eventually will return a string 
that is displayed on screen. These transitions, the combination of a 
ConsoleKey and a Func of string, are called *commands*.

When creating a Conzo application you must at least define one command, 
the start command. It's the command that is executed when the application 
starts. A Conzo application also has one default command for quitting the
application and that is the Escape key. In the examples below you will see 
how to define another key for quitting.

As said before, each command at least returns a string that is displayed.
Conzo also consists of a default template provider that makes your 
application look good right away. By just configuring some settings or 
even by implementing your own template provider you have total control 
over how your GUI looks. 

For complex applications, with Conzo, you could write an MVC(ish) 
console application.

Unfortunately, input fields for free text entering are not supported yet.


## Examples  

Let's create a bare minimum Conzo application that only displays "Hello World":

```C#
static void Main()
{
   // Create a command that returns the text "Hello World".
   var helloWorldCommand = CommandFactory.Create(() => "Hello World");
   
   // Via the Settings object the start command is defined.
   var settings = new Settings(helloWorldCommand);
   
   // Create a new ConsoleApplication with the settings.
   var myApp = ConsoleApplication.Create(settings);
   
   // Run the application which means the start command will be invoked 
   // and the string value that is returned will be displayed.
   myApp.Run();
}
```

![alt text](https://github.com/bouwe77/conzo/blob/master/Documentation/img1.png?raw=true)

Let's replace the Hello World expression by a method that
returns the current time:

```C#
static string DisplayTime()
{
   return $"Time: {DateTime.Now.ToString("hh:mm:ss")}";
}

static void Main()
{
   var displayTimeCommand = CommandFactory.Create(DisplayTime);
   var settings = new Settings(displayTimeCommand);
   var myApp = ConsoleApplication.Create(settings);
   myApp.Run();
}
```

Now it is time for implementing our first command. The user
can press R to refresh the time:

```C#
static string DisplayTime()
{
   return $"Time: {DateTime.Now.ToString("hh:mm:ss")} (Press R to refresh)";
}

static void Main()
{
   var displayTimeCommand = CommandFactory.Create(DisplayTime);
   var settings = new Settings(displayTimeCommand);
   var myApp = ConsoleApplication.Create(settings);

   // Configure the displayTimeCommand by triggering the same command 
   // when the R key is pressed.
   myApp.Configure(displayTimeCommand)
      .AddNextCommand(ConsoleKey.R, displayTimeCommand);

   myApp.Run();
}
```

*TODO Screenshot*

In the code below the application title, quit key and text and 
background colors are changed.

```C#
static string DisplayTime()
{
   return $"Time: {DateTime.Now.ToString("hh:mm:ss")} (Press R to refresh)";
}

static void Main()
{
   var displayTimeCommand = CommandFactory.Create(DisplayTime);

   // Change the application settings. 
   var settings = new Settings(displayTimeCommand)
   {
      ApplicationTitle = "Hello World",
      QuitKey = ConsoleKey.F10,
      Layout = new LayoutSettings
      {
         BackgroundColor = ConsoleColor.White,
         TextColor = ConsoleColor.DarkBlue
      }
   };

   var myApp = ConsoleApplication.Create(settings);

   myApp.Configure(displayTimeCommand)
      .AddNextCommand(ConsoleKey.R, displayTimeCommand);

   myApp.Run();
}
```

*TODO screenshot*

*TODO Add example for adding conditional commands*

*TODO Add example for global commands*

*TODO Add example for a custom template provider*

*TODO Add example for a MVC-like approach*

## Installation
Download the source code and build it in Visual Studio. Then create a console application, add a reference to Conzo.dll and start coding!
