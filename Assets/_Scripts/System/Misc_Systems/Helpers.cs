using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Helpers
{
    /// <summary>
    /// a is ± 1 of b
    /// </summary>
    public static bool IsPM1(this float a, float b)
    {
        return a < b + 1 && a > b - 1;
    }

    /// <summary>
    /// a is ± n of b
    /// </summary>
    public static bool IsPOM(this float a, float n, float b)
    {
        return a < b + n && a > b - n;
    }

    /// <summary>
    /// a is ± n of b
    /// </summary>
    public static bool IsPOM(this int a, int n, int b)
    {
        return a < b + n && a > b - n;
    }

    /// <summary>
    /// A grid positions listed index.
    /// </summary>
    /// <param name="vector2">Vector2 grid position</param>
    /// <returns>(x * height) + y</returns>
    public static int Vec2ToInt(this Vector2 gridPosition, int boardSize)
    {
        return
     ((int)gridPosition.x * boardSize) + (int)gridPosition.y;
    }


    /// <summary>
    /// A grid positions listed index.
    /// </summary>
    /// <returns>(x * height) + y</returns>
    public static int Vec2ToInt(this Vector2Int gridPosition, int boardSize)
    { return (gridPosition.x * boardSize) + gridPosition.y; }

    /// <summary>
    /// A grid positions listed index.
    /// </summary>
    /// <returns>(x * height) + y</returns>
    public static int Vec2ToInt(int x, int y, int boardSize)
    {
        return (x * boardSize) + y;
    }
    //TODOThis really should be (X * height) + Y


    /// <summary>
    /// For flat maps in Unity3D. Uses X and Z and ignores the Y axis. Assumes an even square grid.
    /// </summary>
    /// <returns>(x * boardSize.z) + z</returns>
    public static int Vec3ToInt(this Vector3Int gridPosition, int boardSize) =>
        ((int)gridPosition.x * boardSize) + (int)gridPosition.z;


    /// <summary>
    /// Always returns a positive remainder.
    /// </summary>
    public static int SignedMod(this int a, int b)
    {
        b *= b < 0 ? -1 : 1;
        while (a < 0) a += b;
        return a % b;
    }

    /// <summary>
    /// Always returns a positive remainder.
    /// </summary>
    public static float SignedMod(this float a, float b)
    {
        b *= b < 0 ? -1 : 1;
        while (a < 0) a += b;
        return a % b;
    }

    /// <summary>
    /// Add T[] t2 to T[] t1. Neither arg needs to be initialized.
    /// </summary>
    public static T[] Combine<T>(ref T[] t1, T[] t2)
    {
        List<T> temp = new();
        if (t1 != null) foreach (T datum in t1) temp.Add(datum);
        if (t2 != null) foreach (T datum in t2) temp.Add(datum);
        return t1 = temp.ToArray();
    }

    /// <summary>
    /// Add T t2 to T[] t1. Neither arg needs to be initialized.
    /// </summary>
    public static T[] Combine<T>(ref T[] t1, T t2)
    {
        List<T> temp = new();
        if (t1 != null) foreach (T datum in t1) temp.Add(datum);
        if (t2 != null) temp.Add(t2);
        return t1 = temp.ToArray();
    }

    /// <summary>
    /// _thisIsStartCase => This Is Start Case
    /// </summary>
    public static string StartCase(this string s)
    {
        string temp = s[0] == '_' ? string.Empty : s[0].ToString().ToUpper();

        for (int i = 1; i < s.Length; i++)
        {
            if (char.IsUpper(s[i])) temp += " " + s[i];
            else if (s[i] == '_') temp += " ";
            else temp += s[i];
        }

        return temp;
    }

    /// <summary>
    /// _thisIsCapsCase => THIS IS CAPS CASE
    /// </summary>
    public static string CapsCase(this string s)
    {
        string temp = s[0] == '_' ? string.Empty : s[0].ToString().ToUpper();

        for (int i = 1; i < s.Length; i++)
        {
            if (char.IsUpper(s[i])) temp += " " + s[i];
            else if (s[i] == '_') temp += " ";
            else temp += s[i].ToString().ToUpper();
        }

        return temp;
    }

    /// <summary>
    /// _thisIsSentenceCase => This is sentence case
    /// </summary>
    public static string SentenceCase(this string s)
    {
        string temp = s[0] == '_' ? string.Empty : s[0].ToString().ToUpper();

        for (int i = 1; i < s.Length; i++)
        {
            if (char.IsUpper(s[i])) temp += ' ' + s[i].ToString().ToLower();
            else if (s[i] == '_') temp += ' ';
            else temp += s[i];
        }

        return temp;
    }


    // public static bool Contains<T>(this T[] ts, T item)
    // {
    //     foreach (T t in ts) if (item.Equals(t)) return true;
    //     return false;
    // }

    public static Vector3 NormalDirection(this Vector3 a, Vector3 b)
    {
        var AB = b - a;
        return AB.normalized;
    }
}
