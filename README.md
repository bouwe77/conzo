## Introduction

Conzo is a framework for creating user friendly .NET console applications.

## Features


## Installation

TODO: Describe the installation process

## Usage

```C#
static void Main()
{
  var startCommand = new Command(() => "Hello World");
  var settings = new Settings(startCommand);
  var myApp = ConsoleApplication.Create(settings);
  myApp.Run();
}
```

## License

TODO: Write license
