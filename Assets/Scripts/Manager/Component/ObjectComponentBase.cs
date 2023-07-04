using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectComponentBase<T> : MonoBehaviour where T : ObjectCore
{
    [Header("Core")]
    [SerializeField] protected T core;

    //--------------------------------------------------

    protected void Awake()
    {
        // ƒCƒxƒ“ƒg’Ç‰Á
        core.OnStartEvent += OnStart;
        core.OnFixedUpdateEvent += OnFixedUpdate;
        core.OnUpdateEvent += OnUpdate;
    }

    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }
    protected virtual void OnFixedUpdate() { }
}
