using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public abstract class HPManager<T> : ObjectComponentBase<T> where T : ObjectCore
    {
        [Header("Parameters")]
        [SerializeField,Tooltip("最大体力")] protected int maxHP;

        /// <summary> 現在のHP </summary>
        protected int currentHP;

        /// <summary> 変化前のHP </summary>
        protected int prevHP;

        /* Properties */
        /// <summary> HP率(現在のHP/最大HP) </summary>
        public float HPRate => currentHP / maxHP;

        /// <summary> 死んだか </summary>
        public bool IsDead { get; private set; }

        /// <summary> HPが変更されたか </summary>
        public bool IsChangedHP => (currentHP != prevHP);

        /// <summary> 現在のHPと変更前のHPの差 </summary>
        public int HPDiff => (currentHP - prevHP);

        //--------------------------------------------------
        /* Events */
        /// <summary> ダメージ時のイベント </summary>
        public event Action OnDamagedEvent;

        /// <summary> 死亡時のイベント </summary>
        public event Action OnDeadEvent;

		//--------------------------------------------------
		/// <summary> ダメージ時の処理 </summary>
		/// <param name="damageAmount">ダメージ量</param>
		public virtual void Damaged(int damageAmount)
        {
            prevHP = currentHP;             // 以前のHPを保存
            currentHP -= damageAmount;      // 体力減らす

			// 0以下の場合、死亡イベント実行
			if (currentHP <= 0) {            
                Dead();
            }

            // 死んでいない場合、ダメージイベント実行
            else {
                OnDamagedEvent?.Invoke();
            }
        }
        
        // HPが0以下になったときに実行される
        void Dead()
        {
            currentHP = 0;                  // HP0にする
            IsDead = true;                  // 死亡フラグ立てる
            OnDeadEvent?.Invoke();          // 死亡イベント実行
        }
    }
}
