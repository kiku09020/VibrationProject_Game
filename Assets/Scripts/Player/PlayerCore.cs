using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
    public class PlayerCore : ObjectCore {

        [SerializeField] PlayerDataReceiver dataReceiver;

        /* Properties */
        /// <summary>
        /// デバイスの入力情報
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
        /// プレイヤーのコンポーネントを取得
        /// </summary>
        /// <typeparam name="T">コンポーネント</typeparam>
        /// <param name="checkChildren">子のコンポーネントを含めるか</param>
        /// <returns>コンポーネント</returns>
        /// <exception cref="Exception"></exception>
        public T GetPlayerComponent<T>(bool checkChildren = false) where T : PlayerBaseComponent
        {
            // ゲームオブジェクトのコンポーネントを取得する
            if (GetComponent<T>() is T comp) {
                return comp;
            }

            // 子オブジェクトのコンポーネントを取得する
            if(checkChildren) {
                T childComp;

                for (int i = 0; i < transform.childCount; i++) {
                    childComp = transform.GetChild(i).GetComponent<T>();        // 取得

                    if (childComp != null) {        // nullじゃなければ返す
                        return childComp;
                    }
                }
            }

            // 例外
            throw new Exception("コンポーネントが見つかりませんでした");
        }
    }
}