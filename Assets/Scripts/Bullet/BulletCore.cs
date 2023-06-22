using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Bullet {
    public class BulletCore : ObjectCore {

		//--------------------------------------------------

		private void Start()
		{
			OnStartEvent();
		}

		private void FixedUpdate()
		{
			OnUpdateEvent();
		}

		public override event Action OnStartEvent;
		public override event Action OnUpdateEvent;
	}
}