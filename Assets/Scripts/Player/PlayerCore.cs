using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
    public class PlayerCore : ObjectCore {

        [SerializeField] DeviceDataReceiver dataReceiver;
        [SerializeField] PlayerController playerController;

        /* Properties */
        /// <summary>
        /// デバイスの入力情報
        /// </summary>
        public DeviceDataReceiver DataReceiver => dataReceiver; 
        public PlayerController Controller => playerController;

        //--------------------------------------------------

        protected override void Start()
        {
            base.Start();
        }

		protected override void Update()
		{
			base.Update();
		}

		protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        //--------------------------------------------------
    }
}