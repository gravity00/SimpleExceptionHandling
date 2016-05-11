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
    public class HandlingConfiguration : IHandlingConfiguration
    {
        private Func<Exception, bool>[] _handlers = new Func<Exception, bool>[10];
        private int _handlerCount;

        /// <summary>
        /// Adds the given exception handler to this configuration. If this handler matches
        /// a given exception on <see cref="Catch"/>, it will be considered successfully handled.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IHandlingConfiguration On<TException>(Action<TException> handler, Func<TException, bool> condition = null)
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            AddHandler(ex =>
            {
                var castedException = ex as TException;
                if (castedException == null)
                    return false;

                if (condition == null || condition(castedException))
                {
                    handler(castedException);
                    return true;
                }
                return false;
            });

            return this;
        }

        /// <summary>
        /// Adds the given exception handler to this configuration. If this handler matches
        /// a given exception on <see cref="Catch"/> and returns true, it will be considered successfully handled.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IHandlingConfiguration On<TException>(Func<TException, bool> handler, Func<TException, bool> condition = null)
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            AddHandler(ex =>
            {
                var castedException = ex as TException;
                if (castedException == null)
                    return false;

                if (condition == null || condition(castedException))
                    return handler(castedException);

                return false;
            });

            return this;
        }

        /// <summary>
        /// Passes the given exception through all the handlers until beeing successfully handled.
        /// </summary>
        /// <remarks>
        /// Order will be preserved when running the handlers.
        /// </remarks>
        /// <param name="exception"></param>
        /// <param name="throwIfNotHandled"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Catch(Exception exception, bool throwIfNotHandled = true)
        {
            if (exception == null) throw new ArgumentNullException(nameof(exception));

            var handled = false;
            for (var i = 0; !handled && i < _handlerCount; i++)
            {
                handled = _handlers[i](exception);
            }

            if (!handled && throwIfNotHandled)
                throw exception;
        }

        #region Private

        private void AddHandler(Func<Exception, bool> handler)
        {
            if (_handlerCount == _handlers.Length)
            {
                var newHandlerCollection = new Func<Exception, bool>[_handlers.Length*2];
                _handlers.CopyTo(newHandlerCollection, 0);
                _handlers = newHandlerCollection;
            }

            _handlers[_handlerCount] = handler;
            ++_handlerCount;
        }

        #endregion
    }
}