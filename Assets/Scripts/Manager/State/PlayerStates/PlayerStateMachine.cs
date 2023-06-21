using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameController.State;

namespace Game.Player.State {
	public class PlayerStateMachine : StateMatchineBase<PlayerStateBase,PlayerCore> {

	}

	public abstract class PlayerStateBase : StateBase { }
}