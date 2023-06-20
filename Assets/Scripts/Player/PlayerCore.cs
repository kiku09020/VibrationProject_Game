using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
    public class PlayerCore : MonoBehaviour {

        [SerializeField] PlayerDataReceiver dataReceiver;

        /* Properties */
        /// <summary>
        /// デバイスの入力情報
        /// </summary>
        public PlayerDataReceiver DataReceiver { get => dataReceiver; }

        /* Events */
        /// <summary>
        /// Startで実行されるイベント
        /// </summary>
        public event Action OnStartEvent;

        /// <summary>
        /// Updateで実行されるイベント
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