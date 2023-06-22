using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
    public class PlayerCore : ObjectCore {

        [SerializeField] PlayerDataReceiver dataReceiver;

        /* Properties */
        /// <summary>
        /// �f�o�C�X�̓��͏��
        /// </summary>
        public PlayerDataReceiver DataReceiver { get => dataReceiver; }

        /* Events */
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

        //--------------------------------------------------
    }
}