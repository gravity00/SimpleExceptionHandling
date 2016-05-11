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
