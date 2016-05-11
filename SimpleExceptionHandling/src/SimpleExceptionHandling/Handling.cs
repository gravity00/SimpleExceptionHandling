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

namespace SimpleExceptionHandling
{
    using System;

    /// <summary>
    /// Exposes static methods to fluently configure <see cref="IHandlingConfiguration"/> instances.
    /// </summary>
    public static class Handling
    {
        /// <summary>
        /// Creates a new <see cref="IHandlingConfiguration"/> instance with the given handler.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The handling configuration</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IHandlingConfiguration On<TException>(Action<TException> handler, Func<TException, bool> condition = null)
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            return new HandlingConfiguration().On(handler, condition);
        }

        /// <summary>
        /// Creates a new <see cref="IHandlingConfiguration"/> instance with the given handler.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The handling configuration</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IHandlingConfiguration On<TException>(Func<TException, bool> handler, Func<TException, bool> condition = null)
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            return new HandlingConfiguration().On(handler, condition);
        }
    }
}
