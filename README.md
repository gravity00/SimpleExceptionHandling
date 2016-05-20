# Simple Exception Handling
Library that helps developers to handle exceptions outside catch blocks. Typical usages are global exception handlers.

## Installation 
This library can be installed via [NuGet](https://www.nuget.org/packages/SimpleExceptionHandling/) package. Just run the following command:

```powershell
Install-Package SimpleExceptionHandling -Pre
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
	public override bool ShouldHandle(ExceptionHandlerContext context)
	{
		return true;
	}

	public override void Handle(ExceptionHandlerContext context)
	{
		ResponseMessageResult result = null;

		Handling
			.On<ValidationException>(ex =>
			{
				var messages = new List<string>();
				foreach (var error in ex.ValidationErrors)
					messages.AddRange(error.Messages);

				result =
					context.Request.CreateBadRequestResult(
						new[]
						{
							new KeyValuePair<string, string[]>(string.Empty, messages.ToArray())
						});
			})
			.On<BusinessException>(ex =>
			{
				result = context.Request.CreateConflictResult(ex.Message);
			})
			.On<GenericException>(ex =>
			{
				result = context.Request.CreateInternalServerErrorResult(ex.Message);
			})
			.On<ExternalServiceException>(ex =>
			{
				result = context.Request.CreateBadGatewayResult();
			})
			.On<TimeoutException>(ex =>
			{
				result = context.Request.CreateGatewayTimeoutResult();
			})
			.On<NotImplementedException>(ex =>
			{
				result = context.Request.CreateNotImplementedResult();
			})
			.Catch(context.Exception, throwIfNotHandled: false);

		if (result == null)
		{
			base.Handle(context);
			return;
		}
		context.Result = result;
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
                Handling
                    .On<ArgumentNullException>(ex =>
                    {
                        handlerName = $"ArgumentNullException[ParamName={ex.ParamName}]";
                    })
                    .On<ArgumentException>(ex =>
                    {
                        handlerName = $"ArgumentException[ParamName={ex.ParamName}]";
                    });

            configuration.Catch(new ArgumentNullException(nameof(param01)));
            Console.WriteLine($"Handler -> '{handlerName}'");
            //  Handler -> 'ArgumentNullException[ParamName=param01]'

            handlerName = null;
            configuration.Catch(new ArgumentOutOfRangeException(nameof(param01)));
            Console.WriteLine($"Handler -> '{handlerName}'");
            //  Handler -> 'ArgumentException[ParamName=param01]'

            handlerName = null;
            configuration.Catch(new Exception(), throwIfNotHandled: false);
            Console.WriteLine($"Handler -> '{handlerName}'");
            //  Handler -> ''
        }
```

### Input Parameter and Result

In this example, a parameter is passed as an argument of the `Catch` method, and a result is returned by handlers. One of the handlers will be invoked but won't be considered to handle the exception:

```csharp
        public static void InputAndResultExceptionHandling(string param01)
        {
            var configuration =
                Handling
                    .On<ArgumentNullException>((ex, i) =>
                    {
                        //  this handler will be invoked, but says to be ignored
                        
                        //return new HandlingResult(false);
                        //return HandlingResult.False;
                        return false;
                    })
                    .On<ArgumentException>((ex, i) =>
                    {
                        var ret =
                            $"ArgumentException[ParamName={ex.ParamName}, InputParameter={i.Parameter<int>()}]";
                        return new HandlingResult(true, ret);
                    })
                    .On<Exception>((ex, i) =>
                    {
                        var ret =
                            $"Exception[InputParameter={i.Parameter}]";
                        return new HandlingResult(true, ret);
                    });

            var result = 
                configuration.Catch(
                    new ArgumentNullException(nameof(param01)), 987987);
            Console.WriteLine($"Handler -> '{result.Result<string>()}'");
            //  Handler -> 'ArgumentException[ParamName=param01, InputParameter=987987]'

            result = 
                configuration.Catch(
                    new ArgumentOutOfRangeException(nameof(param01)), 123123);
            Console.WriteLine($"Handler -> '{result.Result<string>()}'");
            //  Handler -> 'ArgumentException[ParamName=param01, InputParameter=123123]'

            result =
                configuration.Catch(new Exception(), 54321);
            Console.WriteLine($"Handler -> '{result.Result}'");
            //  Handler -> 'Exception[InputParameter=54321]'
        }
```

## Conditions

In this example, some handlers are conditionally invoked, even if the exception type match:

```csharp
        public static void ConditionalExceptionHandling(string param01, string param02, string param03)
        {
            string handlerName = null;
            var configuration =
                Handling
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

            configuration.Catch(new ArgumentNullException(nameof(param01)));
            Console.WriteLine($"Handler -> '{handlerName}'");
            //  Handler -> 'ArgumentException[ParamName=param01]'

            configuration.Catch(new ArgumentNullException(nameof(param03)));
            Console.WriteLine($"Handler -> '{handlerName}'");
            //  Handler -> 'ArgumentException[ParamName=param03]'
        }
```
