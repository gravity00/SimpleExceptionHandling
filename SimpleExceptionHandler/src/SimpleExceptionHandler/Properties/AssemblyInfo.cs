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

using System;
using System.Reflection;

#if NET40 || NET45

using System.Runtime.InteropServices;

#endif

[assembly: AssemblyTitle("SimpleExceptionHandler")]
[assembly: AssemblyDescription("Library that helps developers to handle exceptions outside catch blocks.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Net.JoaoSimoes")]
[assembly: AssemblyProduct("SimpleExceptionHandler")]
[assembly: AssemblyCopyright("Copyright © 2016 João Simões")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

#if NET40 || NET45

[assembly: ComVisible(false)]

[assembly: Guid("7c547964-762f-4e2a-9473-e3fb5d1adccb")]

#endif

[assembly: CLSCompliant(true)]

[assembly: AssemblyVersion("1.0.0")]
[assembly: AssemblyInformationalVersion("1.0.0-alpha01")]
