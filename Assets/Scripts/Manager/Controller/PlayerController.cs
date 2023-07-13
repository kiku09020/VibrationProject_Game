using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameController.Manager;

public class PlayerController : MonoBehaviour {

	[Header("Components")]
	[SerializeField] DeviceDataReceiver deviceDataReceiver;

	[SerializeField] DeviceThreasholds threasholds;

	/* Properties */
	KeyController key;
	DeviceController device;

	/// <summary>  �L���ȃR���g���[���[ </summary>
	public static ControllerBase ActiveController { get; private set; }

	/// <summary> �R���g���[���[���f�o�C�X���ǂ��� </summary>
	public static bool ActiveCtrlIsDevice => (ActiveController is DeviceController);

	//--------------------------------------------------

	[Serializable]
	public class DeviceThreasholds {
		[Header("�f�o�C�X")]
		[SerializeField, Tooltip("��X��")] float upThreashold = .7f;
		[SerializeField, Tooltip("���X��")] float downThreashold = .1f;
		[SerializeField, Tooltip("���X��")] float sideThreashold = .5f;

		public float Up => upThreashold;
		public float Down => downThreashold;
		public float Side => sideThreashold;
	}

	//--------------------------------------------------

	public abstract class ControllerBase {
		public abstract bool IsUp { get; }
		public abstract bool IsDown { get; }
		public abstract bool IsLeft { get; }
		public abstract bool IsRight { get; }

		public abstract bool IsPressed { get; }
		//public abstract bool IsReleased { get; }

		public abstract bool IsPressedPauseButton { get; }

		public abstract bool IsAxisX { get; }

		public bool IsInputAnyKey()
		{
			if (IsUp || IsDown || IsLeft || IsRight || IsPressed || IsPressedPauseButton) {
				return true;
			}

			return false;
		}
	}

	/// <summary> �L�[���� </summary>
	public class KeyController : ControllerBase {
		public override bool IsUp => Input.GetKeyDown(KeyCode.UpArrow);
		public override bool IsDown => Input.GetKeyDown(KeyCode.DownArrow);
		public override bool IsLeft => Input.GetKeyDown(KeyCode.LeftArrow);
		public override bool IsRight => Input.GetKeyDown(KeyCode.RightArrow);

		public override bool IsPressed => Input.GetKeyDown(KeyCode.Space);
		//public override bool IsReleased => Input.GetKeyUp  (KeyCode.Space);

		public override bool IsPressedPauseButton => Input.GetKeyDown(KeyCode.Escape);

		public override bool IsAxisX => (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1);
	}

	/// <summary> �f�o�C�X���� </summary>
	public class DeviceController : ControllerBase {
		DeviceDataReceiver receiver;
		DeviceThreasholds threasholds;

		public DeviceController(DeviceDataReceiver receiver, DeviceThreasholds threasholds)
		{
			this.receiver = receiver; this.threasholds = threasholds;
		}

		public override bool IsUp => (receiver.Gyro.y >= threasholds.Up);
		public override bool IsDown => (receiver.Gyro.y <= threasholds.Down && receiver.Gyro.y != 0);
		public override bool IsLeft => (receiver.Gyro.x >= threasholds.Side);
		public override bool IsRight => (receiver.Gyro.x <= -threasholds.Side);

		public override bool IsPressed => (receiver.IsPressedA);
		//public override bool IsReleased => (receiver.IsPressed);

		public override bool IsPressedPauseButton => (receiver.IsPressedB);

		public override bool IsAxisX => (Mathf.Abs(receiver.Gyro.x) >= threasholds.Side);
	}

	//--------------------------------------------------

	private void Start()
	{
		key = new KeyController();
		device = new DeviceController(deviceDataReceiver, threasholds);

		ActiveController = key;
	}

	private void Update()
	{
		ChangeActiveDevice();
		CheckPauseButton();
	}

	//--------------------------------------------------
	// �f�o�C�X�����͂��ꂽ��A���̃f�o�C�X�ɐ؂�ւ�
	void ChangeActiveDevice()
	{
		if (key.IsInputAnyKey()) {
			ActiveController = key;
		}

		else if (device.IsInputAnyKey()) {
			ActiveController = device;
		}
	}

	// �|�[�Y���͔���
	void CheckPauseButton()
	{
		// �|�[�Y������Ȃ����Ƀ|�[�Y�{�^�������͂��ꂽ��A�|�[�Y
		if (!PauseManager.IsPausing && ActiveController.IsPressedPauseButton) {
			PauseManager.Pause();
		}

		// �|�[�Y���Ƀ|�[�Y�{�^������͂�����|�[�Y����
		else if (PauseManager.IsPausing && ActiveController.IsPressedPauseButton) {
			PauseManager.UnPause();
		}
	}
}