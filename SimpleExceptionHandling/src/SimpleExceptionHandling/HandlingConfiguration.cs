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
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IHandlingConfiguration On<TException>(Action<TException> handler)
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            AddHandler(ex =>
            {
                var castedException = ex as TException;
                if (castedException == null)
                    return false;

                handler(castedException);
                return true;
            });

            return this;
        }

        /// <summary>
        /// Adds the given exception handler to this configuration. If this handler matches
        /// a given exception on <see cref="Catch"/> and returns true, it will be considered successfully handled.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IHandlingConfiguration On<TException>(Func<TException, bool> handler)
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            AddHandler(ex =>
            {
                var castedException = ex as TException;
                return castedException != null && handler(castedException);
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