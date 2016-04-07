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
  var config = new ConsoleApplicationConfiguration(startCommand);
  var myApp = ConsoleApplication.Create(config);
  myApp.Start();
}
```

## License

TODO: Write license
