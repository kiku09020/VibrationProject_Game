using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController.Manager {
    public class GameManager : ObjectCore {
        /* Properties */
        /// <summary> �Q�[���N���A�t���O </summary>
        public static bool IsGameCleared { get; private set; }
        /// <summary> �Q�[���I�[�o�[�t���O </summary>
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
        /// <summary> �Q�[���N���A </summary>
        public void SetGameCleared() { IsGameCleared = true; }
        /// <summary> �Q�[���I�[�o�[ </summary>
        public void SetGameOvered() { IsGameOvered = true; }
    }
}