using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectComponentBase<T> : MonoBehaviour where T : ObjectCore
{
    [Header("Core")]
    [SerializeField] protected T core;
    [SerializeField] bool isFixedUpdate = true;

    //--------------------------------------------------

    protected void Awake()
    {
        // ƒCƒxƒ“ƒg’Ç‰Á
        core.OnStartEvent += OnStart;

        if (isFixedUpdate) {
            core.OnFixedUpdateEvent += OnUpdate;
        }

        else {
            core.OnUpdateEvent += OnUpdate;
        }
    }

    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }
}
