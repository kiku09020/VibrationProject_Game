using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ObjectCore
{
	public override event Action OnStartEvent;
	public override event Action OnUpdateEvent;

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
