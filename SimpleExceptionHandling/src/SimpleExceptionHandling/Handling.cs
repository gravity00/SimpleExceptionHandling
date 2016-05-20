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
        private static readonly IHandlingResult<object> HandledResult = new HandlingResult(true);

        private static readonly IHandlingResult<object> IgnoredResult = new HandlingResult(false);

        #region Prepare

        /// <summary>
        /// Prepares a new <see cref="IHandlingConfiguration{TParameter,TResult}"/> instance to be configured.
        /// </summary>
        /// <typeparam name="TParameter">The parameter type</typeparam>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <returns>The handling configuration instance</returns>
        public static IHandlingConfiguration<TParameter, TResult> Prepare<TParameter, TResult>()
        {
            return new HandlingConfiguration<TParameter, TResult>();
        }

        /// <summary>
        /// Prepares a new <see cref="IHandlingConfiguration"/> instance to be configured.
        /// </summary>
        /// <returns>The handling configuration instance</returns>
        public static IHandlingConfiguration<object, object> Prepare()
        {
            return new HandlingConfiguration();
        }

        #endregion

        #region IHandlingResult.Result

        /// <summary>
        /// Gets the <see cref="IHandlingResult{TResult}.Result"/> as an extected type.
        /// </summary>
        /// <typeparam name="TResult">The state type</typeparam>
        /// <param name="result">The handling result to use</param>
        /// <returns>The result object</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidCastException"></exception>
#if NET20
        public static TResult Result<TResult>(IHandlingResult<object> result)
#else
        public static TResult Result<TResult>(this IHandlingResult<object> result)
#endif
        {
            if (result == null) throw new ArgumentNullException(nameof(result));

            return (TResult)result.Result;
        }

        #endregion

        #region IHandlingInput.Parameter

        /// <summary>
        /// Gets the <see cref="IHandlingInput{TParameter}.Parameter"/> as an extected type.
        /// </summary>
        /// <typeparam name="TResult">The state type</typeparam>
        /// <param name="input">The handling input to use</param>
        /// <returns>The parameter object</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidCastException"></exception>
#if NET20
        public static TResult Parameter<TResult>(IHandlingInput<object> input)
#else
        public static TResult Parameter<TResult>(this IHandlingInput<object> input)
#endif
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            return (TResult)input.Parameter;
        }

        #endregion

        #region Handled

        /// <summary>
        /// Returns an handling result with the flag <see cref="IHandlingResult{TResult}.Handled"/>
        /// set to true.
        /// </summary>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <returns>The handling result</returns>
        public static IHandlingResult<TResult> Handled<TResult>()
        {
            return new HandlingResult<TResult>(true);
        }

        /// <summary>
        /// Returns an handling result with the flag <see cref="IHandlingResult{TResult}.Handled"/>
        /// set to true.
        /// </summary>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="result">The result value</param>
        /// <returns>The handling result</returns>
        public static IHandlingResult<TResult> Handled<TResult>(TResult result)
        {
            return new HandlingResult<TResult>(true, result);
        }

        /// <summary>
        /// Returns an handling result with the flag <see cref="IHandlingResult{TResult}.Handled"/>
        /// set to true.
        /// </summary>
        /// <returns>The handling result</returns>
        public static IHandlingResult<object> Handled()
        {
            return HandledResult;
        }

        /// <summary>
        /// Returns an handling result with the flag <see cref="IHandlingResult{TResult}.Handled"/>
        /// set to true.
        /// </summary>
        /// <param name="result">The result value</param>
        /// <returns>The handling result</returns>
        public static IHandlingResult<object> Handled(object result)
        {
            return new HandlingResult(true, result);
        }

        #endregion

        #region Ignore

        /// <summary>
        /// Returns an handling result with the flag <see cref="IHandlingResult{TResult}.Handled"/>
        /// set to false.
        /// </summary>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <returns>The handling result</returns>
        public static IHandlingResult<TResult> Ignore<TResult>()
        {
            return new HandlingResult<TResult>(false);
        }

        /// <summary>
        /// Returns an handling result with the flag <see cref="IHandlingResult{TResult}.Handled"/>
        /// set to false.
        /// </summary>
        /// <returns>The handling result</returns>
        public static IHandlingResult<object> Ignore()
        {
            return IgnoredResult;
        }

        #endregion
    }
}
