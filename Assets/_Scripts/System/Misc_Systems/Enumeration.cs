using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

[Serializable]
public abstract class Enumeration
{
    /// <summary>
    /// SN & Id are separate.
    /// </summary>
    protected Enumeration(int sn, int id, string name) => (SN, Id, Name) = (sn, id, name);
    /// <summary>
    /// SN & Id are the same.
    /// </summary>
    protected Enumeration(int snId, string name) => (SN, Id, Name) = (snId, snId, name);

    /// <summary>
    /// Serial Number, should always be unique from other similar typed enums.
    /// </summary>
    public int SN { get; private set; }
    /// <summary>
    /// Identifiable value, may overlap with other enums.
    /// </summary>
    public int Id { get; private set; }
    public string Name { get; private set; }

    /// <summary>
    /// Matches SN only.
    /// </summary>
    public static int operator +(Enumeration a, int b) => a.SN + b;
    /// <summary>
    /// Matches SN only.
    /// </summary>
    public static int operator -(Enumeration a, int b) => a.SN - b;
    /// <summary>
    /// Matches SN only.
    /// </summary>
    public static int operator +(Enumeration a, Enumeration b) => a.SN + b.SN;
    /// <summary>
    /// Matches SN only.
    /// </summary>
    public static int operator -(Enumeration a, Enumeration b) => a.SN - b.SN;

    /// <summary>
    /// Matches SN only.
    /// </summary>
    public static bool operator ==(Enumeration a, int b) => a.SN == b;
    /// <summary>
    /// Matches SN only.
    /// </summary>
    public static bool operator !=(Enumeration a, int b) => a.SN != b;
    /// <summary>
    /// Matches SN && name.
    /// </summary>
    public static bool operator ==(Enumeration a, Enumeration b) => a.SN == b.SN && a.Name == b.Name;
    /// <summary>
    /// Matches SN && name.
    /// </summary>
    public static bool operator !=(Enumeration a, Enumeration b) => a.SN != b.SN || a.Name != b.Name;

    /// <summary>
    /// Matches SN only.
    /// </summary>
    public static bool operator <=(Enumeration a, int b) => a.SN <= b;
    /// <summary>
    /// Matches SN only.
    /// </summary>
    public static bool operator >=(Enumeration a, int b) => a.SN >= b;
    /// <summary>
    /// Matches SN only.
    /// </summary>
    public static bool operator <=(Enumeration a, Enumeration b) => a.SN <= b.SN;
    /// <summary>
    /// Matches SN only.
    /// </summary>
    public static bool operator >=(Enumeration a, Enumeration b) => a.SN >= b.SN;

    /// <summary>
    /// Matches SN only.
    /// </summary>
    public static implicit operator int(Enumeration a) => a.SN;

    /// <summary>
    /// Matches obj, SN, and Name.
    /// </summary>
    public override bool Equals(object obj) => obj is Enumeration e && SN == e.SN && Name == e.Name;
    public override int GetHashCode() => HashCode.Combine(SN, Name);

    /// <summary>
    /// Return a new instance of the enum matched by obj, SN, and Name.
    /// </summary>
    public static T FindExact<T>(T t) where T : Enumeration, new()
    {
        foreach (var e in All<T>()) if (e.Equals(t)) return e;
        throw new ArgumentOutOfRangeException(t.ToString());
    }

    /// <summary>
    /// Return a new instance of the enum matched by Id.
    /// </summary>
    public static T FindId<T>(int i) where T : Enumeration, new()
    {
        foreach (var e in All<T>()) if (e.Id == i) return e;
        throw new ArgumentOutOfRangeException(i.ToString());
    }

    /// <summary>
    /// Return a new instance of the enum matched by SN.
    /// </summary>
    public static T FindSN<T>(int i) where T : Enumeration, new()
    {
        foreach (var e in All<T>()) if (e.SN == i) return e;
        throw new ArgumentOutOfRangeException(i.ToString());
    }


    /// <summary>
    /// Return a new instance of the enum by it's string value: Name.
    /// </summary>
    public static T FindName<T>(string s) where T : Enumeration, new()
    {
        foreach (var e in All<T>()) if (e.Name == s) return e;
        throw new ArgumentOutOfRangeException(s);
    }

    /// <summary>
    /// Return a new instance of the enum matched by SN.
    /// </summary>
    public static T FindMatch<T>(int i, string s) where T : Enumeration, new()
    {
        foreach (var e in All<T>()) if (e.SN == i && e.Name == s) return e;
        throw new ArgumentOutOfRangeException(i.ToString());
    }

    /// <summary>
    /// Get all enums, in order of declaration (not sorted).
    /// </summary>
    public static T[] All<T>() where T : Enumeration, new() => GetAll<T>().ToArray();

    private static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
    {
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        foreach (FieldInfo info in fields)
        {
            T instance = new();

            if (info.GetValue(instance) is T locatedValue)
            {
                yield return locatedValue;
            }
        }
    }

    public static int Length<T>() where T : Enumeration, new() => GetAll<T>().Count();
}