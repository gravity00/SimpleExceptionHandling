namespace SimpleExceptionHandling
{
    using System;

    /// <summary>
    /// The exception handling configuration.
    /// </summary>
    public interface IHandlingConfiguration
    {
        /// <summary>
        /// Adds the given exception handler to this configuration. If this handler matches
        /// a given exception on <see cref="Catch"/>, it will be considered successfully handled.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        IHandlingConfiguration On<TException>(Action<TException> handler, Func<TException, bool> condition = null)
            where TException : Exception;

        /// <summary>
        /// Adds the given exception handler to this configuration. If this handler matches
        /// a given exception on <see cref="Catch"/> and returns true, it will be considered successfully handled.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="handler">The handler to be added</param>
        /// <param name="condition">An optional condition to be checked if the handler must be used</param>
        /// <returns>The configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        IHandlingConfiguration On<TException>(Func<TException, bool> handler, Func<TException, bool> condition = null)
            where TException : Exception;

        /// <summary>
        /// Passes the given exception through all the handlers until beeing successfully handled.
        /// </summary>
        /// <remarks>
        /// Order will be preserved when running the handlers.
        /// </remarks>
        /// <param name="exception"></param>
        /// <param name="throwIfNotHandled"></param>
        /// <exception cref="ArgumentNullException"></exception>
        void Catch(Exception exception, bool throwIfNotHandled = true);
    }
}
