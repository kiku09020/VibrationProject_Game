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

        /// <summary>
        /// �v���C���[�̃R���|�[�l���g���擾
        /// </summary>
        /// <typeparam name="T">�R���|�[�l���g</typeparam>
        /// <param name="checkChildren">�q�̃R���|�[�l���g���܂߂邩</param>
        /// <returns>�R���|�[�l���g</returns>
        /// <exception cref="Exception"></exception>
        public T GetPlayerComponent<T>(bool checkChildren = false) where T : PlayerBaseComponent
        {
            // �Q�[���I�u�W�F�N�g�̃R���|�[�l���g���擾����
            if (GetComponent<T>() is T comp) {
                return comp;
            }

            // �q�I�u�W�F�N�g�̃R���|�[�l���g���擾����
            if(checkChildren) {
                T childComp;

                for (int i = 0; i < transform.childCount; i++) {
                    childComp = transform.GetChild(i).GetComponent<T>();        // �擾

                    if (childComp != null) {        // null����Ȃ���ΕԂ�
                        return childComp;
                    }
                }
            }

            // ��O
            throw new Exception("�R���|�[�l���g��������܂���ł���");
        }
    }
}