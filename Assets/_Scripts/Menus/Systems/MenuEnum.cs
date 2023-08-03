using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public abstract class Enumeration
{
    protected Enumeration(int id, string name) => (Id, Name) = (id, name);

    public string Name { get; private set; }
    public int Id { get; private set; }

    public static int operator +(Enumeration a, int b) => a.Id + b;
    public static int operator -(Enumeration a, int b) => a.Id - b;
    public static int operator +(Enumeration a, Enumeration b) => a.Id + b.Id;
    public static int operator -(Enumeration a, Enumeration b) => a.Id - b.Id;

    public static bool operator ==(Enumeration a, Enumeration b) => a.Id == b.Id;
    public static bool operator !=(Enumeration a, Enumeration b) => a.Id != b.Id;
    public static bool operator ==(Enumeration a, int b) => a.Id == b;
    public static bool operator !=(Enumeration a, int b) => a.Id != b;

    public static bool operator <=(Enumeration a, int b) => a.Id <= b;
    public static bool operator >=(Enumeration a, int b) => a.Id >= b;
    public static bool operator <=(Enumeration a, Enumeration b) => a.Id <= b.Id;
    public static bool operator >=(Enumeration a, Enumeration b) => a.Id >= b.Id;

    public static implicit operator int(Enumeration a) => a.Id;

    public override bool Equals(object obj) => obj is Enumeration e && Id == e.Id;
    public override int GetHashCode() => HashCode.Combine(Id);

    public static List<T> List<T>() where T : Enumeration, new() => GetAll<T>().ToList();

    public static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
    {
        var type = typeof(T);
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        foreach (var info in fields)
        {
            var instance = new T();

            if (info.GetValue(instance) is T locatedValue)
            {
                yield return locatedValue;
            }
        }
    }
}