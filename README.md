# Simple Exception Handling
Library that helps developers to handle exceptions outside catch blocks.
Typical usages are global exception handlers.

## Installation 
This library can be installed via [NuGet](https://www.nuget.org/packages/SimpleExceptionHandling/) package. Just run the following command:

```powershell
Install-Package SimpleExceptionHandling
```

## Compatibility

This library is compatible with the folowing frameworks:

* .NET for Windows Store apps (> .NETCore 4.5);
* .NET Framework 2.0
* .NET Framework 3.5
* .NET Framework 4.0
* .NET Framework 4.5
* .NET Platform (> DotNET 5.0)
* .NET Portable Subset 4 (.NETPortable,Version=v4.0,Profile=Profile328)
* DNX Core (> DNXCore 5.0)

## Tipical usage

This is a usage example for Web API 2:

```csharp
using SimpleExceptionHandling;

public class GlobalExceptionHandler : ExceptionHandler
{
	private static readonly IHandlingConfiguration<ExceptionHandlerContext, ResponseMessageResult>
		HandlingConfiguration =
			Handling.Prepare<ExceptionHandlerContext, ResponseMessageResult>()
				.On<ValidationException>((ex, i) =>
				{
		                        return Handling.Handled(
		                            i.Parameter.Request.CreateBadRequestResult(
		                                ex.ValidationErrors.Select(
		                                    e => new KeyValuePair<string, string[]>(
		                                        e.Key, e.Messages.ToArray()))));
				})
				.On<BusinessException>((ex, i) =>
				{
					return Handling.Handled(
						i.Parameter.Request.CreateConflictResult(ex.Message));
				})
				.On<GenericException>((ex, i) =>
				{
					return Handling.Handled(
						i.Parameter.Request.CreateInternalServerErrorResult(ex.Message));
				})
				.On<ExternalServiceException>((ex, i) =>
				{
					return Handling.Handled(
						i.Parameter.Request.CreateBadGatewayResult());
				})
				.On<TimeoutException>((ex, i) =>
				{
					return Handling.Handled(
						i.Parameter.Request.CreateGatewayTimeoutResult());
				})
				.On<NotImplementedException>((ex, i) =>
				{
					return Handling.Handled(
						i.Parameter.Request.CreateNotImplementedResult());
				});

	public override bool ShouldHandle(ExceptionHandlerContext context)
	{
		return true;
	}

	public override void Handle(ExceptionHandlerContext context)
	{
		var result = HandlingConfiguration.Catch(context.Exception, context, false);
		if (result.Handled)
		{
			context.Result = result.Result;
		}
		else
		{
			base.Handle(context);
		}
	}
}
```

### Basic usage

Here is a simple example of handling exceptions by their types:

```csharp
public static void BasicExceptionHandling(string param01)
{
	string handlerName = null;
	var configuration =
		Handling.Prepare()
			.On<ArgumentNullException>(ex =>
			{
				handlerName = $"ArgumentNullException[ParamName={ex.ParamName}]";
			})
			.On<ArgumentException>(ex =>
			{
				handlerName = $"ArgumentException[ParamName={ex.ParamName}]";
			});

	var result = configuration.Catch(new ArgumentNullException(nameof(param01)));
	Console.WriteLine($"Handler[Handled={result.Handled}] -> '{handlerName}'");
	//  Handler[Handled=True] -> 'ArgumentNullException[ParamName=param01]'

	handlerName = null;
	result = configuration.Catch(new ArgumentOutOfRangeException(nameof(param01)));
	Console.WriteLine($"Handler[Handled={result.Handled}] -> '{handlerName}'");
	//  Handler[Handled=True] -> 'ArgumentException[ParamName=param01]'

	handlerName = null;
	result = configuration.Catch(new Exception(), throwIfNotHandled: false);
	Console.WriteLine($"Handler[Handled={result.Handled}] -> '{handlerName}'");
	//  Handler[Handled=False] -> ''
}
```

### Input Parameter and Result

In this example, a parameter is passed as an argument of the `Catch` method, and a result is returned by handlers. One of the handlers will be invoked but won't be considered to handle the exception:

```csharp
public static void InputAndResultExceptionHandling(string param01)
{
	var configuration =
		Handling.Prepare<int, string>()
			.On<ArgumentNullException>((ex, i) =>
			{
				//  this handler will be invoked, but says to be ignored

				//return new HandlingResult<string>(false);
				//return Handling.Ignore<string>();
				return false;
			})
			.On<ArgumentException>((ex, i) =>
			{
				var ret = $"ArgumentException[ParamName={ex.ParamName}, InputParameter={i.Parameter}]";
				
				//return new HandlingResult<string>(true, ret);
				return Handling.Handled(ret);
			})
			.On<Exception>((ex, i) =>
			{
				var ret = $"Exception[InputParameter={i.Parameter}]";

				//return new HandlingResult<string>(true, ret);
				return Handling.Handled(ret);
			});

	var result = 
		configuration.Catch(
			new ArgumentNullException(nameof(param01)), 987987);
	Console.WriteLine($"Handler[Handled={result.Handled}] -> '{result.Result}'");
	//  Handler[Handled=True] -> 'ArgumentException[ParamName=param01, InputParameter=987987]'

	result = 
		configuration.Catch(
			new ArgumentOutOfRangeException(nameof(param01)), 123123);
	Console.WriteLine($"Handler[Handled={result.Handled}] -> '{result.Result}'");
	//  Handler[Handled=True] -> 'ArgumentException[ParamName=param01, InputParameter=123123]'

	result =
		configuration.Catch(new Exception(), 54321);
	Console.WriteLine($"Handler[Handled={result.Handled}] -> '{result.Result}'");
	//  Handler[Handled=True] -> 'Exception[InputParameter=54321]'
}
```

## Conditions

In this example, some handlers are conditionally invoked, even if the exception type match:

```csharp
public static void ConditionalExceptionHandling(string param01, string param02, string param03)
{
	string handlerName = null;
	var configuration =
		Handling.Prepare()
			.On<ArgumentNullException>(ex =>
			{
				handlerName = "ArgumentNullException[ParamName=param01]";
			}, (ex, i) => ex.ParamName == nameof(param01))
			.On<ArgumentNullException>(ex =>
			{
				handlerName = "ArgumentNullException[ParamName=param02]";
			}, (ex, i) => ex.ParamName == nameof(param02))
			.On<ArgumentNullException>(ex =>
			{
				handlerName = $"ArgumentNullException[ParamName={ex.ParamName}]";
			});

	var result = configuration.Catch(new ArgumentNullException(nameof(param01)));
	Console.WriteLine($"Handler[Handled={result.Handled}] -> '{handlerName}'");
	//  Handler[Handled=True] -> 'ArgumentException[ParamName=param01]'

	result = configuration.Catch(new ArgumentNullException(nameof(param03)));
	Console.WriteLine($"Handler[Handled={result.Handled}] -> '{handlerName}'");
	//  Handler[Handled=True] -> 'ArgumentException[ParamName=param03]'
}
```
