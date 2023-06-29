using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary> �V���A���ڑ��C�x���g�N���X </summary>
public abstract class SerialEventUnitBase
{
	/// <summary> ���O���b�Z�[�W </summary>
	public abstract string LogMessage { get; }

	/// <summary> �C�x���g�t���O </summary>
	public bool EventFlag { get; private set; }

	/// <summary> ���������C�x���g </summary>
	public event StartDelegate StartEvent;
	public delegate void StartDelegate();

	/// <summary> �X�V�����C�x���g </summary>
	public event UpdateDelegate UpdateEvent;
	public delegate bool UpdateDelegate();

	CancellationToken token;

	/// <summary> �C�x���g��o�^���� </summary>
	public void RegisterEvents(StartDelegate startEvent, UpdateDelegate updateEvent,CancellationToken token)
	{
		ResetEvent();

		StartEvent += startEvent;
		UpdateEvent += updateEvent;

		this.token = token;
	}

	/// <summary> �C�x���g�����Z�b�g���� </summary>
	void ResetEvent()
	{
		StartEvent = null;
		UpdateEvent = null;
	}

	/// <summary> �C�x���g�����s���� </summary>
	public async void RunEvent()
	{
		// �����𖞂����܂őҋ@
		await UniTask.WaitUntil(() => {

			// �ŏ��̏���
			if (!EventFlag) {
				StartEvent?.Invoke();
				EventFlag = true;
			}
				
			Debug.LogWarning(LogMessage);
			return UpdateEvent.Invoke();
		}, cancellationToken: token);
	}
}

public class OnDisconnectedEventUnit : SerialEventUnitBase {
	public override string LogMessage => "�ڑ����ؒf����܂����B�Đڑ����Ă�������";
}

public class OnShouldConnectedEventUnit : SerialEventUnitBase {
	public override string LogMessage => "�V���A���|�[�g��1���ڑ�����Ă��܂���B";
}

public class OnShouldSelectedEventUnit : SerialEventUnitBase {
	public override string LogMessage => "�g�p����f�o�C�X���ڑ�����Ă���V���A���|�[�g��I�����Ă��������B";
}
