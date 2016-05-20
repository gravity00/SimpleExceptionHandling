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
    /// <summary>
    /// The handling result.
    /// </summary>
    public class HandlingResult<TResult> : IHandlingResult<TResult>
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// The property <see cref="Handled"/> will be set to true.
        /// </summary>
        public HandlingResult()
        {
            Handled = true;
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="handled">Was the exception handled?</param>
        /// <param name="result">An optional result object</param>
        public HandlingResult(bool handled, TResult result = default(TResult))
        {
            Handled = handled;
            Result = result;
        }

        #endregion

        /// <summary>
        /// Was the exception handled?
        /// </summary>
        public bool Handled { get; }

        /// <summary>
        /// The result object for the handling operation
        /// </summary>
        public TResult Result { get; }
    }

    /// <summary>
    /// The handling result.
    /// </summary>
    public class HandlingResult : HandlingResult<object>, IHandlingResult
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// The property <see cref="IHandlingResult.Handled"/> will be set to true.
        /// </summary>
        public HandlingResult()
        {

        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="handled">Was the exception handled?</param>
        /// <param name="result">An optional result object</param>
        public HandlingResult(bool handled, object result = null) : base(handled, result)
        {

        }

        #endregion
    }
}