using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectComponentBase<T> : MonoBehaviour where T : IObjectCore
{
    [Header("Core")]
    [SerializeField] T core;

    //--------------------------------------------------

    protected void Awake()
    {
        // �C�x���g�ǉ�
        core.OnStartEvent += OnStart;
        core.OnUpdateEvent += OnUpdate;
    }

    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }
}
