namespace SimpleExceptionHandling
{
#if NET20
    
    public delegate TResult Func<in T, out TResult>(T arg);

#endif
}