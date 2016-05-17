#region License
// The MIT License (MIT)
// 
// Copyright (c) 2016 João Simões
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion
namespace SimpleExceptionHandling.Examples
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine();
            BasicExceptionHandling(null);

            Console.WriteLine();
            ResultExceptionHandling(null);

            Console.WriteLine();
            ConditionalExceptionHandling(null, null, null);
        }

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

        public static void ResultExceptionHandling(string param01)
        {
            var configuration =
                Handling
                    .On<ArgumentNullException>((ex, r) =>
                    {
                        r.State = $"ArgumentNullException[ParamName={ex.ParamName}]";
                        r.Handled = false;
                    })
                    .On<ArgumentException>((ex, r) =>
                    {
                        r.State = $"ArgumentException[ParamName={ex.ParamName}]";
                        r.Handled = true;
                    })
                    .On<Exception>((ex, r) =>
                    {
                        r.State = "Exception";
                        r.Handled = true;
                    });

            IHandlingResult result;

            result = configuration.Catch(new ArgumentNullException(nameof(param01)));
            Console.WriteLine($"Handler -> '{result.State}'");
            //  Handler -> 'ArgumentException[ParamName=param01]'

            result = configuration.Catch(new ArgumentOutOfRangeException(nameof(param01)));
            Console.WriteLine($"Handler -> '{result.State}'");
            //  Handler -> 'ArgumentException[ParamName=param01]'

            result = configuration.Catch(new Exception());
            Console.WriteLine($"Handler -> '{result.State}'");
            //  Handler -> 'Exception'
        }

        public static void ConditionalExceptionHandling(string param01, string param02, string param03)
        {
            string handlerName = null;
            var configuration =
                Handling
                    .On<ArgumentNullException>(ex =>
                    {
                        handlerName = $"ArgumentNullException[ParamName=param01]";
                    }, ex => ex.ParamName == nameof(param01))
                    .On<ArgumentNullException>(ex =>
                    {
                        handlerName = $"ArgumentNullException[ParamName=param02]";
                    }, ex => ex.ParamName == nameof(param02))
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
    }
}
