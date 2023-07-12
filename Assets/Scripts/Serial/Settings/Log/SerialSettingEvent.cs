using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary> �V���A���ڑ��C�x���g�N���X </summary>
public class SerialSettingEvent
{
	/// <summary> �C�x���g���s��(�ҋ@��)���ǂ��� </summary>
	bool isRunning;

	/* Events */
	/// <summary> ���������C�x���g </summary>
	public event StartDelegate StartEvent;
	public delegate void StartDelegate();

	/// <summary> �X�V�����C�x���g </summary>
	public event UpdateDelegate UpdateEvent;
	public delegate void UpdateDelegate();

	/// <summary> �I�����C�x���g </summary>
	public event EndDelegate EndEvent;
	public delegate void EndDelegate();

	//--------------------------------------------------
	/// <summary> �C�x���g��o�^���� </summary>
	public void RegisterEvents(StartDelegate startEvent, UpdateDelegate updateEvent, EndDelegate endDelegate, bool isResetEvents = false)
	{
		// �C�x���g���Z�b�g
		if (isResetEvents) {
			StartEvent	 = null;
			updateEvent	 = null;
			EndEvent	 = null;
		}

		// �C�x���g�o�^
		StartEvent  += startEvent;
		UpdateEvent += updateEvent;
		EndEvent	+= endDelegate;
	}

	/// <summary> �C�x���g�����s���� </summary>
	public void RunEvent(bool condition)
	{
		// �X�V����
		UpdateEvent?.Invoke();

		// ����
		if (condition) {
			// �J�n����
			if (!isRunning) {
				StartEvent?.Invoke();
				isRunning = true;
			}
		}

		// �I������
		else if (isRunning) {
			EndEvent?.Invoke();
			isRunning = false;
		}
	}
}
