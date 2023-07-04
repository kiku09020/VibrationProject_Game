using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController.Manager {
    public class GameManager : ObjectCore {
        /* Properties */
        /// <summary> ゲームクリアフラグ </summary>
        public static bool IsGameCleared { get; private set; }
        /// <summary> ゲームオーバーフラグ </summary>
        public static bool IsGameOvered { get; private set; }

        //--------------------------------------------------

        protected override void Start()
        {
            base.Start();

            IsGameCleared = false;
            IsGameOvered = false;
        }

        //--------------------------------------------------
        /* Setters */
        /// <summary> ゲームクリア </summary>
        public void SetGameCleared() { IsGameCleared = true; }
        /// <summary> ゲームオーバー </summary>
        public void SetGameOvered() { IsGameOvered = true; }
    }
}