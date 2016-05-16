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
        #region On

        /// <summary>
        /// Creates a new <see cref="IHandlingConfiguration"/> instance with the given handler.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The handling configuration</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IHandlingConfiguration On<TException>(
            Action<TException, IHandlingResult> handler, Func<TException, IHandlingResult, bool> condition = null)
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
        public static IHandlingConfiguration On<TException>(
            Action<TException> handler, Func<TException, IHandlingResult, bool> condition = null)
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
        public static IHandlingConfiguration On<TException>(
            Action<TException, IHandlingResult> handler, Func<TException, bool> condition = null)
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
        public static IHandlingConfiguration On<TException>(Action<TException> handler, Func<TException, bool> condition = null)
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            return new HandlingConfiguration().On(handler, condition);
        }

        #endregion

        #region IHandlingResult.State

        /// <summary>
        /// Gets the <see cref="IHandlingResult.State"/> as an extected type.
        /// </summary>
        /// <typeparam name="TState">The state type</typeparam>
        /// <param name="result">The result to use</param>
        /// <returns>The state object</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidCastException"></exception>
#if NET20
        public static TState State<TState>(IHandlingResult result)
#else
        public static TState State<TState>(this IHandlingResult result)
#endif
        {
            if (result == null) throw new ArgumentNullException(nameof(result));

            return (TState) result.State;
        }

        /// <summary>
        /// Sets the <see cref="IHandlingResult.State"/> with the given value.
        /// </summary>
        /// <typeparam name="TState">The state type</typeparam>
        /// <param name="result">The result to use</param>
        /// <param name="state">The state object</param>
        /// <returns>The result after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
#if NET20
        public static IHandlingResult State<TState>(IHandlingResult result, TState state)
#else
        public static IHandlingResult State<TState>(this IHandlingResult result, TState state)
#endif
        {
            if (result == null) throw new ArgumentNullException(nameof(result));

            result.State = state;
            return result;
        }

        #endregion
    }
}
