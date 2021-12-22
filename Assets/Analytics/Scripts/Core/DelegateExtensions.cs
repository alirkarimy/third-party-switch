using System;
using System.Collections.Generic;
using System.Linq;

public static class DelegateExtensions
{
    public static void SafeInvoke(this Delegate del, params object[] args)
    {
        var exceptions = new List<Exception>();

        foreach (Delegate handler in del.GetInvocationList())
        {
            try
            {
                handler.Method.Invoke(handler.Target, args);
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
        }

        if (exceptions.Any())
        {
            throw new AggregateException(exceptions);
        }
    }
}
