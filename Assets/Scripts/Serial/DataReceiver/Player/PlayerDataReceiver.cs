using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataReceiver : DataReceiver_Base {

	[SerializeField] int dataLength = 3;

	/// <summary>
	/// �f�o�C�X�̌X��
	/// </summary>
	public Vector2 Gyro { get; private set; }

	/// <summary>
	/// �{�^���������ꂽ���ǂ���
	/// </summary>
	public bool IsPressed { get; private set; }

	protected override void OnReceivedData()
	{
		var data = handler.GetSplitedData();     // �f�[�^�擾

		try {
			if (data.Length >= dataLength) {
				// �f�[�^�����񂩂�float�^�ɕϊ����āA�x�N�g���ɓK�p
				float x = float.Parse(data[0]);
				float y = float.Parse(data[1]);

				Gyro = new Vector2(x, y);

				// �����ꂽ���ǂ�����bool�^�ɕϊ�
				IsPressed = bool.Parse(data[2]);
			}
		}

		// ��O
		catch (System.Exception exc) {
			Debug.LogWarning(exc.Message);
		}
	}
}
