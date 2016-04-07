# conzo

Conzo is a framework for creating user friendly .NET console applications.

## Installation

TODO: Describe the installation process

## Usage

```C#
var command = new Command(() => "Hello World");
var config = new ConsoleApplicationConfiguration(command);
var myApp = ConsoleApplication.Create(config);
myApp.Start();
```

## License

TODO: Write license
