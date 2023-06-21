using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour,IObjectCore
{
    public event Action OnStartEvent;
    public event Action OnUpdateEvent;

    //--------------------------------------------------

    void Start()
    {
        OnStartEvent();
    }

    void FixedUpdate()
    {
        OnUpdateEvent();
    }
}
