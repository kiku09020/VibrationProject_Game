using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController.State {
    public class GameStateMachine : StateMatchineBase<GameStateBase,GameManager> {}

    public abstract class GameStateBase : StateBase {}
}