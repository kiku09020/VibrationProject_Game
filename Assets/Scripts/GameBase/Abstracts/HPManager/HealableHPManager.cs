using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    /// <summary> �񕜉\�ȃI�u�W�F�N�g��HP�Ǘ��N���X </summary>
    public class HealableHPManager<T> : HPManager<T> where T : ObjectCore
    {
        public event Action OnHealedEvent;
		//--------------------------------------------------
		public void Healed(int healAmount)
        {
            if(IsDead) return;              // ����ł���return

            prevHP = currentHP;             // �ύX�O��HP��ۑ�
            currentHP += healAmount;        // ��

            // �ő�HP�����傫���Ȃ�����A�߂�
            if (currentHP > maxHP) {
                currentHP = maxHP;
            }

            OnHealedEvent?.Invoke();        // �C�x���g���s
        }
    }
}
