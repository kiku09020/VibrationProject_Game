using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
    public class PlayerCore : MonoBehaviour {

        [SerializeField] PlayerDataReceiver dataReceiver;

        /* Properties */
        /// <summary>
        /// �f�o�C�X�̓��͏��
        /// </summary>
        public PlayerDataReceiver DataReceiver { get => dataReceiver; }

        /* Events */
        /// <summary>
        /// Start�Ŏ��s�����C�x���g
        /// </summary>
        public event Action OnStartEvent;

        /// <summary>
        /// Update�Ŏ��s�����C�x���g
        /// </summary>
        public event Action OnUpdateEvent;

        //--------------------------------------------------

        void Start()
        {
            OnStartEvent();
        }

        void Update()
        {
            OnUpdateEvent();
        }
    }
}