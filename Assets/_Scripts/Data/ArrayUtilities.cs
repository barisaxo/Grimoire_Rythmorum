using System.Collections.Generic;
using System.Diagnostics;

public static class ArrayUtilities
{
    public static T[] Add<T>(this T[] t1, T[] t2)
    {
        List<T> temp = new();
        if (t1 != null) foreach (T datum in t1) temp.Add(datum);
        if (t2 != null) foreach (T datum in t2) temp.Add(datum);
        return temp.ToArray();
    }

    public static T[] Add<T>(this T[] t1, T t2)
    {
        List<T> temp = new();
        if (t1 != null) foreach (T datum in t1) temp.Add(datum);
        if (t2 != null) temp.Add(t2);
        return temp.ToArray();
    }
}