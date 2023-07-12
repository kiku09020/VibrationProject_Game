using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public abstract class HPManager<T> : ObjectComponentBase<T> where T : ObjectCore
    {
        [Header("Parameters")]
        [SerializeField,Tooltip("�ő�̗�")] protected int maxHP;

        /// <summary> ���݂�HP </summary>
        protected int currentHP;

        /// <summary> �ω��O��HP </summary>
        protected int prevHP;

        /* Properties */
        /// <summary> HP��(���݂�HP/�ő�HP) </summary>
        public float HPRate => currentHP / maxHP;

        /// <summary> ���񂾂� </summary>
        public bool IsDead { get; private set; }

        /// <summary> HP���ύX���ꂽ�� </summary>
        public bool IsChangedHP => (currentHP != prevHP);

        /// <summary> ���݂�HP�ƕύX�O��HP�̍� </summary>
        public int HPDiff => (currentHP - prevHP);

        //--------------------------------------------------
        /* Events */
        /// <summary> �_���[�W���̃C�x���g </summary>
        public event Action OnDamagedEvent;

        /// <summary> ���S���̃C�x���g </summary>
        public event Action OnDeadEvent;

		//--------------------------------------------------
		/// <summary> �_���[�W���̏��� </summary>
		/// <param name="damageAmount">�_���[�W��</param>
		public virtual void Damaged(int damageAmount)
        {
            prevHP = currentHP;             // �ȑO��HP��ۑ�
            currentHP -= damageAmount;      // �̗͌��炷

			// 0�ȉ��̏ꍇ�A���S�C�x���g���s
			if (currentHP <= 0) {            
                Dead();
            }

            // ����ł��Ȃ��ꍇ�A�_���[�W�C�x���g���s
            else {
                OnDamagedEvent?.Invoke();
            }
        }
        
        // HP��0�ȉ��ɂȂ����Ƃ��Ɏ��s�����
        void Dead()
        {
            currentHP = 0;                  // HP0�ɂ���
            IsDead = true;                  // ���S�t���O���Ă�
            OnDeadEvent?.Invoke();          // ���S�C�x���g���s
        }
    }
}
