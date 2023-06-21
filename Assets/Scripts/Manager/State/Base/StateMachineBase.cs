using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GameController.State {
	public abstract class StateMatchineBase<T,U> : ObjectComponentBase<U> 
		where T : StateBase where U: IObjectCore{

		[Header("State")]
		[SerializeField] T initState;
		[SerializeField] protected List<T> states = new List<T>();

		/// <summary>
		/// ���݂̏��
		/// </summary>
		public T NowState { get; protected set; }

		//--------------------------------------------------

		/// <summary>
		/// ������ԃZ�b�g�A�b�v
		/// </summary>
		protected override void OnStart()
		{
			NowState = initState;
			NowState.OnEnter();
		}

		//--------------------------------------------------

		/// <summary>
		/// ���݂̏�Ԃ̍X�V����
		/// </summary>
		protected override void OnUpdate()
		{
			NowState.OnUpdate();
		}

		//--------------------------------------------------
		/// <summary>
		/// ��ԑJ��
		/// </summary>
		/// <param name="state">���̏��</param>
		public void StateTransition(T state)
		{
			NowState.OnExit();
			NowState = state;
			NowState.OnEnter();
		}

		/// <summary>
		/// ��ԑJ��
		/// </summary>
		public void StateTransition<State>() where State : T
		{
			StateTransition(GetState<State>());
		}

		/// <summary>
		/// ��ԑJ��
		/// </summary>
		/// <param name="stateName">�J�ڐ�̏�Ԃ̖��O</param>
		public void StateTransition(string stateName)
		{
			StateTransition(GetState(stateName));
		}

		//--------------------------------------------------

		// State����
		void CheckState<State>(Action<State> action) where State : T
		{
			foreach (var state in states) {
				if (state is State targetState) {
					action?.Invoke(targetState);
				}
			}
		}

		/// <summary>
		/// �w�肵��<typeparamref name="State"/>������΁A�����Ԃ�
		/// </summary>
		/// <typeparam name="State">�ړI�̏��</typeparam>
		/// <returns>�w�肳�ꂽ<typeparamref name="State"/></returns>
		public State GetState<State>() where State : T
		{
			State state = default(State);

			CheckState<State>((targetState) => {
				state = targetState;
			});

			if (state != null) {
				return state;
			}

			throw new Exception("�w�肳�ꂽState�͑��݂��܂���");
		}

		/// <summary>
		/// �w�肵�����O��State������΁A�����Ԃ�
		/// </summary>
		/// <param name="stateName">��Ԃ̖��O</param>
		/// <returns>�w�肳�ꂽ���O�̏��</returns>
		public T GetState(string stateName)
		{
			foreach (var state in states) {

				if (state.Name == stateName) {
					return state;
				}
			}

			throw new Exception("�w�肳�ꂽ���O��State�͑��݂��܂���");
		}
	}
}