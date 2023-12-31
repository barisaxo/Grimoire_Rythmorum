using System;
using System.Linq;
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

    public string State;
    [SerializeField] private List<string> updateSubscribers = new();
    [SerializeField] private List<string> fixedUpdateSubscribers = new();
    [SerializeField] private List<string> lateUpdateSubscribers = new();
    [SerializeField] private List<string> activeCoroutines = new();

    private void Update() => ToUpdate?.Invoke();
    private static event Action ToUpdate;
    public static Delegate[] UpdateSubscribers => ToUpdate.GetInvocationList();
    public static event Action OnUpdate
    {
        add
        {
            if (ToUpdate != null &&
                ToUpdate.GetInvocationList().Contains(value)) return;
            ToUpdate += value;
            Io.updateSubscribers.Add(value.Method.Name);
        }
        remove
        {
            ToUpdate -= value;
            Io.updateSubscribers.Remove(value.Method.Name);
            Io.updateSubscribers.Remove(value.Method.Name);
        }
    }

    private void FixedUpdate() => ToFixedUpdate?.Invoke();
    public static event Action ToFixedUpdate;
    public static Delegate[] FixedUpdateSubscribers => ToFixedUpdate.GetInvocationList();
    public static event Action OnFixedUpdate
    {
        add
        {
            if (ToFixedUpdate != null &&
                ToFixedUpdate.GetInvocationList().Contains(value)) return;
            ToFixedUpdate += value;
            Io.fixedUpdateSubscribers.Add(value.Method.Name);
        }
        remove
        {
            ToFixedUpdate -= value;
            Io.fixedUpdateSubscribers.Remove(value.Method.Name);
        }
    }

    private void LateUpdate() => ToLateUpdate?.Invoke();
    public static event Action ToLateUpdate;
    public static Delegate[] LateUpdateSubscribers => ToLateUpdate.GetInvocationList();
    public static event Action OnLateUpdate
    {
        add
        {
            if (ToLateUpdate != null &&
                ToLateUpdate.GetInvocationList().Contains(value)) return;
            ToLateUpdate += value;
            Io.lateUpdateSubscribers.Add(value.Method.Name);
        }
        remove
        {
            ToLateUpdate -= value;
            Io.lateUpdateSubscribers.Remove(value.Method.Name);
        }
    }

    public void RunCoroutine(IEnumerator ie)
    {
        StartCoroutine(ManageCoroutine());
        IEnumerator ManageCoroutine()
        {
            Io.activeCoroutines.Add(ie.ToString());
            yield return ie;
            Io.activeCoroutines.Remove(ie.ToString());
        }
    }

}

public static class MonoSystems
{
    public static void StartCoroutine(this IEnumerator ie) => MonoHelper.Io.RunCoroutine(ie);
}