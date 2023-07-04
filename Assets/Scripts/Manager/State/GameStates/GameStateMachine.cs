using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController.State {
    using GameController.Manager;

    public class GameStateMachine : StateMatchineBase<GameStateBase,GameManager> {}

    public abstract class GameStateBase : StateBase {}
}