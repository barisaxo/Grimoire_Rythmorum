using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoHelper : MonoBehaviour
{
    #region INSTANCE
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void AutoInit()
    {
        DontDestroyOnLoad(Io);
    }

    public static MonoHelper Io => Instance.Io;

    private class Instance
    {
        static Instance() { }
        static MonoHelper _io;
        internal static MonoHelper Io => _io != null ? _io :
            _io = new GameObject(nameof(MonoHelper)).AddComponent<MonoHelper>();
    }

    private void Start()
    {
        if (this != Io) { Destroy(gameObject); }
    }
    #endregion INSTANCE

    [SerializeField] private List<string> actions = new();

    private static event Action ToUpdate;
    private void Update() => ToUpdate?.Invoke();
    public static event Action OnUpdate
    {
        add { ToUpdate += value; Io.actions.Add(value.Method.ToString()); }
        remove { ToUpdate -= value; foreach (var a in Io.actions) { if (a == value.Method.ToString()) { Io.actions.Remove(a); return; } } }
    }

    public static event Action ToLateUpdate;
    private void LateUpdate() => ToLateUpdate?.Invoke();
    public static event Action OnLateUpdate
    {
        add { ToLateUpdate += value; Io.actions.Add(value.Method.ToString() + " Late"); }
        remove { ToLateUpdate -= value; foreach (var a in Io.actions) { if (a == value.Method.ToString() + " Late") { Io.actions.Remove(a); return; } } }
    }
}

public static class MonoSystems
{
    public static void StartCoroutine(this IEnumerator ie) => MonoHelper.Io.StartCoroutine(ie);
}