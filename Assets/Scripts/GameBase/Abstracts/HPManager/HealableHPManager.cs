using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    /// <summary> 回復可能なオブジェクトのHP管理クラス </summary>
    public class HealableHPManager<T> : HPManager<T> where T : ObjectCore
    {
        public event Action OnHealedEvent;
		//--------------------------------------------------
		public void Healed(int healAmount)
        {
            if(IsDead) return;              // 死んでたらreturn

            prevHP = currentHP;             // 変更前のHPを保存
            currentHP += healAmount;        // 回復

            // 最大HPよりも大きくなったら、戻す
            if (currentHP > maxHP) {
                currentHP = maxHP;
            }

            OnHealedEvent?.Invoke();        // イベント実行
        }
    }
}
