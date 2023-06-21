using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GameController.State {
	public abstract class StateMatchineBase<T,U> : ObjectComponentBase<U> 
		where T : StateBase where U: ObjectCore{

		[Header("State")]
		[SerializeField] T initState;
		[SerializeField] protected List<T> states = new List<T>();

		/// <summary>
		/// 現在の状態
		/// </summary>
		public T NowState { get; protected set; }

		//--------------------------------------------------

		/// <summary>
		/// 初期状態セットアップ
		/// </summary>
		protected override void OnStart()
		{
			NowState = initState;
			NowState.OnEnter();
		}

		//--------------------------------------------------

		/// <summary>
		/// 現在の状態の更新処理
		/// </summary>
		protected override void OnUpdate()
		{
			NowState.OnUpdate();
		}

		//--------------------------------------------------
		/// <summary>
		/// 状態遷移
		/// </summary>
		/// <param name="state">次の状態</param>
		public void StateTransition(T state)
		{
			NowState.OnExit();
			NowState = state;
			NowState.OnEnter();
		}

		/// <summary>
		/// 状態遷移
		/// </summary>
		public void StateTransition<State>() where State : T
		{
			StateTransition(GetState<State>());
		}

		/// <summary>
		/// 状態遷移
		/// </summary>
		/// <param name="stateName">遷移先の状態の名前</param>
		public void StateTransition(string stateName)
		{
			StateTransition(GetState(stateName));
		}

		//--------------------------------------------------

		// State判定
		void CheckState<State>(Action<State> action) where State : T
		{
			foreach (var state in states) {
				if (state is State targetState) {
					action?.Invoke(targetState);
				}
			}
		}

		/// <summary>
		/// 指定した<typeparamref name="State"/>があれば、それを返す
		/// </summary>
		/// <typeparam name="State">目的の状態</typeparam>
		/// <returns>指定された<typeparamref name="State"/></returns>
		public State GetState<State>() where State : T
		{
			State state = default(State);

			CheckState<State>((targetState) => {
				state = targetState;
			});

			if (state != null) {
				return state;
			}

			throw new Exception("指定されたStateは存在しません");
		}

		/// <summary>
		/// 指定した名前のStateがあれば、それを返す
		/// </summary>
		/// <param name="stateName">状態の名前</param>
		/// <returns>指定された名前の状態</returns>
		public T GetState(string stateName)
		{
			foreach (var state in states) {

				if (state.Name == stateName) {
					return state;
				}
			}

			throw new Exception("指定された名前のStateは存在しません");
		}
	}
}