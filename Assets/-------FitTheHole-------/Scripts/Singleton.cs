using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : class
{
    public static T Instance { get; protected set; }
    public bool IsInitialized { get => isInitialized; protected set => isInitialized = value; }

    protected bool isInitialized = false;

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            isInitialized = false;
            Instance = this as T;
        }
    }



    protected virtual void OnDestroy()
    {
        isInitialized = false;
        if (Instance != null && Instance.Equals(this))
        {
            Instance = null;
        }
    }
}
