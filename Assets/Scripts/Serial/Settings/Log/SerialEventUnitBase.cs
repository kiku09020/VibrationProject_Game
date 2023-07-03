using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary> �V���A���ڑ��C�x���g�N���X </summary>
public class SerialEventUnitBase
{
	string logMessage;		// ���O���b�Z�[�W

	/// <summary> �C�x���g���s��(�ҋ@��)���ǂ��� </summary>
	public bool IsRunning { get; private set; }

	/// <summary> �I������ </summary>
	public bool EventEndCondition { get; private set; }

	/* Events */
	/// <summary> ���������C�x���g </summary>
	public event StartDelegate StartEvent;
	public delegate void StartDelegate();

	/// <summary> �X�V�����C�x���g </summary>
	public event UpdateDelegate EndEvent;
	public delegate void UpdateDelegate();

	CancellationToken token;

	//--------------------------------------------------

	public SerialEventUnitBase(string logMessage)
	{
		this.logMessage = logMessage;
	}

	/// <summary> �C�x���g��o�^���� </summary>
	public void RegisterEvents(StartDelegate startEvent, UpdateDelegate updateEvent,CancellationToken token)
	{
		StartEvent = null;
		EndEvent = null;

		StartEvent += startEvent;
		EndEvent += updateEvent;

		this.token = token;
	}

	/// <summary>
	/// �ҋ@�������擾����
	/// </summary>
	public void SetEndCondition(bool condition)
	{
		EventEndCondition = condition;
	}

	/// <summary> �C�x���g�����s���� </summary>
	public async UniTask RunEvent()
	{
		// �J�n����
		StartEvent?.Invoke();
		IsRunning = true;

		// �����𖞂����܂őҋ@
		await UniTask.WaitUntil(() => {				
			Debug.LogWarning(logMessage);
			return EventEndCondition;

		}, cancellationToken: token);

		// �I������
		EndEvent?.Invoke();
		IsRunning = false;
	}
}
