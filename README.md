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
			.Catch(context.Exception, false);

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

```
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
            configuration.Catch(new Exception(), false);
            Console.WriteLine($"Handler -> '{handlerName}'");
            //  Handler -> ''
        }
```
