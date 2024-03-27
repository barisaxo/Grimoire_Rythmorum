using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Helpers
{
    /// <summary>
    /// a is ± 1 of b (exclusive)
    /// </summary>
    public static bool IsPM1(this float a, float b)
    {
        return a < b + 1 && a > b - 1;
    }

    /// <summary>
    /// a is ± 1 of b (exclusive)
    /// </summary>
    public static bool IsPM1(this Vector3 a, Vector3 b)
    {
        return a.x < b.x + 1 && a.x > b.x - 1 &&
               a.y < b.y + 1 && a.y > b.y - 1 &&
               a.z < b.z + 1 && a.z > b.z - 1;
    }


    /// <summary>
    /// a is ± n of b (exclusive)
    /// </summary>
    public static bool IsPOM(this float a, float n, float b)
    {
        return a < b + n && a > b - n;
    }
    /// <summary>
    /// a is ± n of b (exclusive)
    /// </summary>
    public static bool IsPOM(this int a, int n, int b)
    {
        return a < b + n && a > b - n;
    }
    /// <summary>
    /// a is ± n of b (exclusive)
    /// </summary>
    public static bool IsPOM(this int a, float n, float b)
    {
        return a < b + n && a > b - n;
    }
    /// <summary>
    /// a is ± n of b (exclusive)
    /// </summary>
    public static bool IsPOM(this Vector3 a, float n, Vector3 b)
    {
        return a.x < b.x + n && a.x > b.x - n &&
               a.y < b.y + n && a.y > b.y - n &&
               a.z < b.z + n && a.z > b.z - n;
    }

    /// <summary>
    /// a is ± n of b (exclusive)
    /// </summary>
    public static bool IsPOM(this Vector3 a, Vector3 n, Vector3 b)
    {
        return a.x < b.x + n.x && a.x > b.x - n.x &&
               a.y < b.y + n.x && a.y > b.y - n.x &&
               a.z < b.z + n.x && a.z > b.z - n.x;
    }
    /// <summary>
    /// a is ± n of b (exclusive)
    /// </summary>
    public static bool IsPOM(this Vector2 a, float n, Vector2 b)
    {
        return a.x < b.x + n && a.x > b.x - n &&
               a.y < b.y + n && a.y > b.y - n;
    }

    /// <summary>
    /// a is ± n of b
    /// </summary>
    public static bool IsPOM(this Vector2 a, Vector2 n, Vector2 b)
    {
        return a.x < b.x + n.x && a.x > b.x - n.x &&
               a.y < b.y + n.x && a.y > b.y - n.x;
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
    /// A grid position's listed index.
    /// </summary>
    /// <returns>(x * height) + y</returns>
    public static int Vec2ToInt(this Vector2Int gridPosition, int boardSize)
    { return (gridPosition.x * boardSize) + gridPosition.y; }

    /// <summary>
    /// A grid position's listed index.
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
    /// (SignedMod) Always returns a positive remainder.
    /// </summary>
    public static int Smod(this int a, int b)
    {
        if (b == 0) return 0;
        b *= b < 0 ? -1 : 1;
        while (a < 0) a += b;
        return a % b;
    }

    /// <summary>
    /// (SignedMod) Always returns a positive remainder.
    /// </summary>
    public static float Smod(this float a, float b)
    {
        if (b == 0) return 0;
        b *= b < 0 ? -1 : 1;
        while (a < 0) a += b;
        return a % b;
    }

    /// <summary>
    /// (SignedMod) Always returns a positive remainder.
    /// </summary>
    public static Vector2 Smod(this Vector2 a, Vector2 b)
    {
        if (b == Vector2.zero) return Vector2.zero;
        if (b.x == 0) a.x = 0;
        if (b.y == 0) a.y = 0;

        b.x *= b.x < 0 ? -1 : 1;
        while (a.x < 0) a.x += b.x;

        b.y *= b.y < 0 ? -1 : 1;
        while (a.y < 0) a.y += b.y;

        return new Vector2(a.x % b.x, a.y % b.y);
    }

    /// <summary>
    /// (SignedMod) Always returns a positive remainder.
    /// </summary>
    public static Vector2Int Smod(this Vector2Int a, Vector2Int b)
    {
        if (b == Vector2Int.zero) return Vector2Int.zero;
        if (b.x == 0) a.x = 0;
        if (b.y == 0) a.y = 0;

        b.x *= b.x < 0 ? -1 : 1;
        while (a.x < 0) a.x += b.x;

        b.y *= b.y < 0 ? -1 : 1;
        while (a.y < 0) a.y += b.y;

        return new Vector2Int(a.x % b.x, a.y % b.y);
    }
    /// <summary>
    /// (SignedMod) Always returns a positive remainder.
    /// </summary>
    public static Vector2Int Smod(this Vector2 a, Vector2Int b)
    {
        if (b == Vector2Int.zero) return Vector2Int.zero;
        if (b.x == 0) a.x = 0;
        if (b.y == 0) a.y = 0;

        b.x *= b.x < 0 ? -1 : 1;
        while (a.x < 0) a.x += b.x;

        b.y *= b.y < 0 ? -1 : 1;
        while (a.y < 0) a.y += b.y;

        return new Vector2Int((int)(a.x % b.x), (int)(a.y % b.y));
    }
    /// <summary>
    /// (SignedMod) Always returns a positive remainder.
    /// </summary>
    public static Vector2 Smod(this Vector2 a, float b)
    {
        if (b == 0) return Vector2Int.zero;
        b *= b < 0 ? -1 : 1;
        while (a.x < 0) a.x += b;
        while (a.y < 0) a.y += b;

        return new Vector2(a.x % b, a.y % b);
    }

    /// <summary>
    /// (SignedMod) Always returns a positive remainder.
    /// </summary>
    public static Vector2Int Smod(this Vector2Int a, int b)
    {
        if (b == 0) return Vector2Int.zero;
        b *= b < 0 ? -1 : 1;
        while (a.x < 0) a.x += b;
        while (a.y < 0) a.y += b;

        return new Vector2Int(a.x % b, a.y % b);
    }

    /// <summary>
    /// Add T[] t2 to T[] t1. Neither arg needs to be initialized. Refs t1!
    /// </summary>
    public static T[] Combine<T>(ref T[] t1, T[] t2)
    {
        List<T> temp = new();
        if (t1 is not null) foreach (T datum in t1) temp.Add(datum);
        if (t2 is not null) foreach (T datum in t2) temp.Add(datum);
        return t1 = temp.ToArray();
    }

    /// <summary>
    /// Add T t2 to T[] t1. Neither arg needs to be initialized. Refs t1!
    /// </summary>
    public static T[] Combine<T>(ref T[] t1, T t2)
    {
        List<T> temp = new();
        if (t1 is not null) foreach (T datum in t1) temp.Add(datum);
        if (t2 is not null) temp.Add(t2);
        return t1 = temp.ToArray();
    }

    /// <summary>
    /// Returns T t2 added to T[] t1. Neither arg needs to be initialized. Does not ref t1!
    /// </summary>
    public static T[] Added<T>(this T[] t1, T[] t2)
    {
        List<T> temp = new();
        if (t1 is not null) foreach (T datum in t1) temp.Add(datum);
        if (t2 is not null) foreach (T datum in t2) temp.Add(datum);
        return temp.ToArray();
    }

    /// <summary>
    /// Returns T t2 removed from T[] t1. Neither arg needs to be initialized. Does not ref t1!
    /// </summary>
    public static T[] Removed<T>(this T[] t1, T t2)
    {
        List<T> temp = new();
        if (t1 is not null) foreach (T t in t1) if (!t.Equals(t2)) temp.Add(t);
        return temp.ToArray();
    }

    /// <summary>
    /// Removes T t2 to T[] t1. Refs t1!
    /// </summary>
    public static T[] Removed<T>(ref T[] t1, T t2)
    {
        List<T> temp = new();
        if (t1 is not null) foreach (T datum in t1) if (!datum.Equals(t2)) temp.Add(datum);
        return t1 = temp.ToArray();
    }

    /// <summary>
    /// Returns T t2 added to T[] t1. Neither arg needs to be initialized. Does not ref t1!
    /// </summary>
    public static T[] Added<T>(this T[] t1, T t2)
    {
        List<T> temp = new();
        if (t1 is not null) foreach (T datum in t1) temp.Add(datum);
        if (t2 is not null) temp.Add(t2);
        return temp.ToArray();
    }

    public static bool Contains<T>(this T[] t1, T comparer)
    {
        foreach (T t in t1) if (t.Equals(comparer)) return true;
        return false;
    }

    public static T[] Flatten<T>(this T[][] values)
    {
        if (values == null) throw new System.ArgumentNullException();

        int index = 0;
        int length = 0;
        foreach (T[] d1Value in values) length += d1Value.Length;
        T[] flatArray = new T[length];

        foreach (T[] d1Value in values)
            for (int i = 0; i < d1Value.Length; i++)
                flatArray[index++] = d1Value[i];

        return flatArray;
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
        bool firstCharCapped = false;
        string temp = "";

        for (int i = 0; i < s.Length; i++)
        {
            if (!firstCharCapped)
            {
                if (s[i] == '_') temp += string.Empty;
                else
                {
                    temp += s[i].ToString().ToUpper();
                    firstCharCapped = true;
                }
            }

            else if (char.IsUpper(s[i]))
                temp += ' ' + s[i].ToString().ToLower();

            else if (s[i] == '_') temp += ' ';

            else temp += s[i];
        }

        return temp;
    }



    public static Vector3 NormalDirection(this Vector3 a, Vector3 b)
    {
        var AB = b - a;
        return AB.normalized;
    }

    public static int WeightedRandomInt(float rand, float solveRate, int puzzleCount)
    {
        // Calculate the weighted index based on the solve rate
        float weightedIndex = Mathf.Pow(rand, Mathf.Lerp(1.0f, solveRate, solveRate)) * puzzleCount;

        // Convert the weighted index to an integer
        int weightedRand = Mathf.RoundToInt(weightedIndex);

        // Ensure the weightedRand is within the valid range
        weightedRand = Mathf.Clamp(weightedRand, 0, puzzleCount - 1);

        return weightedRand;
    }
    // int WeightedRandomNumber(float rand, float solveRate, int puzzleCount)
    // {
    //     // Calculate the weighted index based on the solve rate
    //     float weightedIndex = Mathf.Pow(rand, solveRate) * puzzleCount;

    //     // Convert the weighted index to an integer
    //     int weightedRand = Mathf.RoundToInt(weightedIndex);

    //     // Ensure the weightedRand is within the valid range
    //     weightedRand = Mathf.Clamp(weightedRand, 0, puzzleCount - 1);

    //     return weightedRand;
    // }    //         int c = 0;
    // for (int i = 0; i < 11; i++)
    // {
    //     for (int ii = 0; ii < 11; ii++)
    //     {
    //         for (int iii = 1; iii < 11; iii++)
    //         {
    //             float fakeRand = (float)i * .1f;
    //             float percent = ((float)ii * .1f);
    //             float count = iii;

    //             int weightedRand = WeightedRandomNumber(fakeRand, percent, (int)count); //(int)Mathf.Round(((((float)fakeRand / (float)percent)) * (float)count));
    //             Debug.Log(weightedRand + " " + fakeRand + " " + percent + " " + count + " " + c++);
    //         }
    //     }
    // }


}


