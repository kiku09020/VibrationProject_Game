using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
    public abstract class PlayerBaseComponent : MonoBehaviour {

        [SerializeField] PlayerCore core;

		//--------------------------------------------------

		private void Awake()
		{
			// Coreのイベントに追加する
			core.OnStartEvent += OnStart;
			core.OnUpdateEvent += OnUpdate;
		}

		/// <summary>
		/// 初期化時の処理
		/// </summary>
		protected virtual void OnStart() { }

        /// <summary>
        /// 毎フレーム実行される処理
        /// </summary>
        protected abstract void OnUpdate();
    }
}