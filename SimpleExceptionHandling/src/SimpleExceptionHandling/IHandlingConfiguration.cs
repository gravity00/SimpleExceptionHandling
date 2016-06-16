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
    /// The exception handling configuration.
    /// </summary>
    /// <typeparam name="TParameter">The parameter type</typeparam>
    /// <typeparam name="TResult">The result type</typeparam>
    public interface IHandlingConfiguration<TParameter, TResult>
    {
        #region On

        /// <summary>
        /// Adds the given exception handler to this configuration. If this handler matches
        /// a given exception on <see cref="Catch"/>, it will be considered successfully handled.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        IHandlingConfiguration<TParameter, TResult> On<TException>(
            Action<TException, IHandlingInput<TParameter>> handler, 
            Func<TException, IHandlingInput<TParameter>, bool> condition = null)
            where TException : Exception;

        /// <summary>
        /// Adds the given exception handler to this configuration. If this handler matches
        /// a given exception on <see cref="Catch"/> and true is returned, it will be considered successfully handled.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        IHandlingConfiguration<TParameter, TResult> On<TException>(
            Func<TException, IHandlingInput<TParameter>, bool> handler, 
            Func<TException, IHandlingInput<TParameter>, bool> condition = null)
            where TException : Exception;

        /// <summary>
        /// Adds the given exception handler to this configuration. If this handler matches
        /// a given exception on <see cref="Catch"/> and <see cref="IHandlingResult{TResult}.Handled"/> is true, 
        /// it will be considered successfully handled.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        IHandlingConfiguration<TParameter, TResult> On<TException>(
            Func<TException, IHandlingInput<TParameter>, IHandlingResult<TResult>> handler, 
            Func<TException, IHandlingInput<TParameter>, bool> condition = null)
            where TException : Exception;

        /// <summary>
        /// Adds the given exception handler to this configuration. If this handler matches
        /// a given exception on <see cref="Catch"/>, it will be considered successfully handled.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        IHandlingConfiguration<TParameter, TResult> On<TException>(
            Action<TException> handler, Func<TException, IHandlingInput<TParameter>, bool> condition = null)
            where TException : Exception;

        /// <summary>
        /// Adds the given exception handler to this configuration. If this handler matches
        /// a given exception on <see cref="Catch"/> and true is returned, it will be considered successfully handled.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        IHandlingConfiguration<TParameter, TResult> On<TException>(
            Func<TException, bool> handler, Func<TException, IHandlingInput<TParameter>, bool> condition = null)
            where TException : Exception;

        /// <summary>
        /// Adds the given exception handler to this configuration. If this handler matches
        /// a given exception on <see cref="Catch"/> and <see cref="IHandlingResult{TResult}.Handled"/> is true, 
        /// it will be considered successfully handled.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        IHandlingConfiguration<TParameter, TResult> On<TException>(
            Func<TException, IHandlingResult<TResult>> handler, 
            Func<TException, IHandlingInput<TParameter>, bool> condition = null)
            where TException : Exception;

        #endregion

        #region FinalizeWith

        /// <summary>
        /// Sets the given handler as the finalization handler that will be executed even if not handlers
        /// matched the given exception.
        /// </summary>
        /// <param name="finalizationHandler">The handler to use</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        IHandlingConfiguration<TParameter, TResult> FinalizeWith(Action<Exception, IHandlingInput<TParameter>, IHandlingResult<TResult>> finalizationHandler);

        /// <summary>
        /// Sets the given handler as the finalization handler that will be executed even if not handlers
        /// matched the given exception.
        /// </summary>
        /// <param name="finalizationHandler">The handler to use</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        IHandlingConfiguration<TParameter, TResult> FinalizeWith(Action<Exception, IHandlingInput<TParameter>> finalizationHandler);

        /// <summary>
        /// Sets the given handler as the finalization handler that will be executed even if not handlers
        /// matched the given exception.
        /// </summary>
        /// <param name="finalizationHandler">The handler to use</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        IHandlingConfiguration<TParameter, TResult> FinalizeWith(Action<Exception> finalizationHandler);

        /// <summary>
        /// Sets the given handler as the finalization handler that will be executed even if not handlers
        /// matched the given exception.
        /// </summary>
        /// <param name="finalizationHandler">The handler to use</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        IHandlingConfiguration<TParameter, TResult> FinalizeWith(Action finalizationHandler);

        #endregion

        /// <summary>
        /// Passes the given exception through all the handlers until beeing successfully handled.
        /// </summary>
        /// <remarks>
        /// Order will be preserved when running the handlers.
        /// </remarks>
        /// <param name="exception">The exception to be catched</param>
        /// <param name="parameter">An optional handling parameter</param>
        /// <param name="throwIfNotHandled">If not handled, should the exception be thrown</param>
        /// <returns>The handling result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        IHandlingResult<TResult> Catch(
            Exception exception, TParameter parameter = default(TParameter), bool throwIfNotHandled = true);
    }

    /// <summary>
    /// The exception handling configuration.
    /// </summary>
    public interface IHandlingConfiguration : IHandlingConfiguration<object, object>
    {
        
    }
}
