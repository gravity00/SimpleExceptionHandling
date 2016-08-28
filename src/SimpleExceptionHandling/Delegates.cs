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
#if NET20

    /// <summary>
    /// Encapsulates a method that has no parameters and does not return a value.
    /// </summary>
    public delegate void Action();

    /// <summary>
    /// Encapsulates a method that has two parameters and does not return a value.
    /// </summary>
    /// <typeparam name="T01">The first parameter type</typeparam>
    /// <typeparam name="T02">The second parameter type</typeparam>
    /// <param name="arg01">The first parameter</param>
    /// <param name="arg02">The second parameter</param>
    public delegate void Action<in T01, in T02>(T01 arg01, T02 arg02);

    /// <summary>
    /// Encapsulates a method that has three parameters and does not return a value.
    /// </summary>
    /// <typeparam name="T01">The first parameter type</typeparam>
    /// <typeparam name="T02">The second parameter type</typeparam>
    /// <typeparam name="T03">The third parameter type</typeparam>
    /// <param name="arg01">The first parameter</param>
    /// <param name="arg02">The second parameter</param>
    /// <param name="arg03">The third parameter</param>
    public delegate void Action<in T01, in T02, in T03>(T01 arg01, T02 arg02, T03 arg03);

    /// <summary>
    /// Encapsulates a method that has a single parameter and returns a value.
    /// </summary>
    /// <typeparam name="T">The parameter type</typeparam>
    /// <typeparam name="TResult">The result type</typeparam>
    /// <param name="arg">The parameter</param>
    /// <returns>The result value</returns>
    public delegate TResult Func<in T, out TResult>(T arg);

    /// <summary>
    /// Encapsulates a method that has two parameters and returns a value.
    /// </summary>
    /// <typeparam name="T01">The first parameter type</typeparam>
    /// <typeparam name="T02">The second parameter type</typeparam>
    /// <typeparam name="TResult">The result type</typeparam>
    /// <param name="arg01">The first parameter</param>
    /// <param name="arg02">The second parameter</param>
    /// <returns>The result value</returns>
    public delegate TResult Func<in T01, in T02, out TResult>(T01 arg01, T02 arg02);

#endif
}