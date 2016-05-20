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
    public class HandlingConfiguration<TParameter, TResult> : IHandlingConfiguration<TParameter, TResult>
    {
        private Func<Exception, IHandlingInput<TParameter>, IHandlingResult<TResult>>[] _handlers = 
            new Func<Exception, IHandlingInput<TParameter>, IHandlingResult<TResult>>[10];
        private int _handlerCount;

        #region On

        /// <summary>
        /// Adds the given exception handler to this configuration. If this handler matches
        /// a given exception on <see cref="IHandlingConfiguration{TParameter,TResult}.Catch"/>, 
        /// it will be considered successfully handled.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IHandlingConfiguration<TParameter, TResult> On<TException>(
            Action<TException, IHandlingInput<TParameter>> handler, 
            Func<TException, IHandlingInput<TParameter>, bool> condition = null) 
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            AddHandler((ex, i) =>
            {
                var castedException = ex as TException;
                if (castedException == null || (condition != null && !condition(castedException, i)))
                    return new HandlingResult<TResult>(false);

                handler(castedException, i);
                return new HandlingResult<TResult>(true);
            });

            return this;
        }

        /// <summary>
        /// Adds the given exception handler to this configuration. If this handler matches
        /// a given exception on <see cref="IHandlingConfiguration{TParameter,TResult}.Catch"/> and 
        /// true is returned, it will be considered successfully handled.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IHandlingConfiguration<TParameter, TResult> On<TException>(
            Func<TException, IHandlingInput<TParameter>, bool> handler, 
            Func<TException, IHandlingInput<TParameter>, bool> condition = null) 
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            AddHandler((ex, i) =>
            {
                var castedException = ex as TException;
                if (castedException == null || (condition != null && !condition(castedException, i)))
                    return new HandlingResult<TResult>(false);
                
                return new HandlingResult<TResult>(handler(castedException, i));
            });

            return this;
        }

        /// <summary>
        /// Adds the given exception handler to this configuration. If this handler matches
        /// a given exception on <see cref="IHandlingConfiguration{TParameter,TResult}.Catch"/> 
        /// and <see cref="IHandlingResult{TResult}.Handled"/> is true, 
        /// it will be considered successfully handled.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IHandlingConfiguration<TParameter, TResult> On<TException>(
            Func<TException, IHandlingInput<TParameter>, IHandlingResult<TResult>> handler, 
            Func<TException, IHandlingInput<TParameter>, bool> condition = null) 
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            AddHandler((ex, i) =>
            {
                var castedException = ex as TException;
                if (castedException == null || (condition != null && !condition(castedException, i)))
                    return new HandlingResult<TResult>(false);

                return handler(castedException, i);
            });

            return this;
        }

        /// <summary>
        /// Adds the given exception handler to this configuration. If this handler matches
        /// a given exception on <see cref="IHandlingConfiguration{TParameter,TResult}.Catch"/>, 
        /// it will be considered successfully handled.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IHandlingConfiguration<TParameter, TResult> On<TException>(
            Action<TException> handler, 
            Func<TException, IHandlingInput<TParameter>, bool> condition = null) 
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            AddHandler((ex, i) =>
            {
                var castedException = ex as TException;
                if (castedException == null || (condition != null && !condition(castedException, i)))
                    return new HandlingResult<TResult>(false);

                handler(castedException);
                return new HandlingResult<TResult>(true);
            });

            return this;
        }

        /// <summary>
        /// Adds the given exception handler to this configuration. If this handler matches
        /// a given exception on <see cref="IHandlingConfiguration{TParameter,TResult}.Catch"/> 
        /// and true is returned, it will be considered successfully handled.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IHandlingConfiguration<TParameter, TResult> On<TException>(
            Func<TException, bool> handler, 
            Func<TException, IHandlingInput<TParameter>, bool> condition = null) 
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            AddHandler((ex, i) =>
            {
                var castedException = ex as TException;
                if (castedException == null || (condition != null && !condition(castedException, i)))
                    return new HandlingResult<TResult>(false);

                return new HandlingResult<TResult>(handler(castedException));
            });

            return this;
        }

        /// <summary>
        /// Adds the given exception handler to this configuration. If this handler matches
        /// a given exception on <see cref="IHandlingConfiguration{TParameter,TResult}.Catch"/> 
        /// and <see cref="IHandlingResult{TResult}.Handled"/> is true, 
        /// it will be considered successfully handled.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IHandlingConfiguration<TParameter, TResult> On<TException>(
            Func<TException, IHandlingResult<TResult>> handler, 
            Func<TException, IHandlingInput<TParameter>, bool> condition = null) 
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            AddHandler((ex, i) =>
            {
                var castedException = ex as TException;
                if (castedException == null || (condition != null && !condition(castedException, i)))
                    return new HandlingResult<TResult>(false);

                return handler(castedException);
            });

            return this;
        }

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
        public IHandlingResult<TResult> Catch(
            Exception exception, TParameter parameter = default(TParameter), bool throwIfNotHandled = true)
        {
            if (exception == null) throw new ArgumentNullException(nameof(exception));

            var input = new HandlingInput<TParameter>(exception, parameter);
            for (var i = 0; i < _handlerCount; i++)
            {
                var result = _handlers[i](exception, input);
                if (result.Handled)
                    return result;
            }

            if (throwIfNotHandled)
                throw exception;
            return new HandlingResult<TResult>(false);
        }

        #region Private

        private void AddHandler(Func<Exception, IHandlingInput<TParameter>, IHandlingResult<TResult>> handler)
        {
            if (_handlerCount == _handlers.Length)
            {
                var newHandlerCollection = 
                    new Func<Exception, IHandlingInput<TParameter>, IHandlingResult<TResult>>[_handlers.Length * 2];
                _handlers.CopyTo(newHandlerCollection, 0);
                _handlers = newHandlerCollection;
            }

            _handlers[_handlerCount] = handler;
            ++_handlerCount;
        }

        #endregion
    }

    /// <summary>
    /// The exception handling configuration.
    /// </summary>
    public class HandlingConfiguration : HandlingConfiguration<object, object>, IHandlingConfiguration
    {
        
    }
}