using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
    public abstract class PlayerBaseComponent : MonoBehaviour {

        [SerializeField] PlayerCore core;

		//--------------------------------------------------

		private void Awake()
		{
			// Core�̃C�x���g�ɒǉ�����
			core.OnStartEvent += OnStart;
			core.OnUpdateEvent += OnUpdate;
		}

		/// <summary>
		/// ���������̏���
		/// </summary>
		protected virtual void OnStart() { }

        /// <summary>
        /// ���t���[�����s����鏈��
        /// </summary>
        protected abstract void OnUpdate();
    }
}