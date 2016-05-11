namespace SimpleExceptionHandler
{
    using System;

    /// <summary>
    /// Exposes static methods to fluently configure <see cref="HandlingConfiguration"/> instances.
    /// </summary>
    public static class Handling
    {
        /// <summary>
        /// Creates a new <see cref="HandlingConfiguration"/> instance with the given handler.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <returns>The handling configuration</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static HandlingConfiguration On<TException>(Action<TException> handler)
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            return new HandlingConfiguration().On(handler);
        }

        /// <summary>
        /// Creates a new <see cref="HandlingConfiguration"/> instance with the given handler.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <returns>The handling configuration</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static HandlingConfiguration On<TException>(Func<TException, bool> handler)
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            return new HandlingConfiguration().On(handler);
        }
    }
}
