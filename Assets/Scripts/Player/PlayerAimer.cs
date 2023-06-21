using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
	/// <summary>
	/// �v���C���[�̏Ə��֌W�̃N���X
	/// </summary>
    public class PlayerAimer : PlayerBaseComponent {

		[SerializeField, Tooltip("�Ə��ړ�臒l"), Range(0, 1)]
		float aimThreshold = .6f;
		[SerializeField, Tooltip("�Ə��ړ�臒l(�߂�)"), Range(-1, 1)]
		float aimReturnThreshold = .6f;

		[SerializeField, Tooltip("�Ə��̊p�x"), Range(0, 90)]
		float aimAngle = 30;

		[SerializeField, Tooltip("�Ə��ړ�����"), Range(0, 1)]
		float aimDuration = .5f;


		[SerializeField, Tooltip("�Ə��C�[�W���O")]
		Ease aimEasing;

		[SerializeField, Tooltip("���݂̕���")]
		Direction currentDirection = Direction.down;

		// ����
		enum Direction
		{
			up,
			down,
		}

		//--------------------------------------------------

		protected override void OnUpdate()
		{
			Aiming();
		}

		void Aiming()
		{
			if (core.DataReceiver.Gyro.y >= aimThreshold && currentDirection == Direction.down) {
				core.transform.DORotate(new Vector3(-aimAngle, 0, 0), aimDuration)
					.SetEase(aimEasing);

				currentDirection = Direction.up;
			}

			else if (core.DataReceiver.Gyro.y <= aimReturnThreshold && currentDirection == Direction.up){
				core.transform.DORotate(new Vector3(0, 0, 0), aimDuration)
					.SetEase(aimEasing);

				currentDirection = Direction.down;
			}
		}
	}
}