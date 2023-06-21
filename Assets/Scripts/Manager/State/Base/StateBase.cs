using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameController.State {
	public abstract class StateBase : MonoBehaviour {
		[SerializeField] string stateName;
		public string Name => stateName;

		public abstract void OnEnter();
		public abstract void OnUpdate();
		public abstract void OnExit();
	}
}