using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
	/// <summary>
	/// �v���C���[�̏Ə��֌W�̃N���X
	/// </summary>
    public class PlayerAimer : PlayerBaseComponent {

		[Header("CheckX")]
		[SerializeField, Tooltip("���E�ړ���臒l�`�F�b�N���邩�B" )]
		bool checkAimThresholdX = true;

		[SerializeField, Tooltip("�ړ�X臒l�B���̒l�ȉ��̂Ƃ��̂݁A�㉺�Ə��ړ�����"), Range(0, 1)]
		float movingThresholdX = .5f;

		[Header("Aiming")]
		[SerializeField, Tooltip("�Ə��̊p�x"), Range(0, 90)]
		float aimAngle = 30;
		[SerializeField, Tooltip("�Ə��ړ�����"), Range(0, 1)]
		float aimDuration = .5f;

		[SerializeField, Tooltip("�Ə��C�[�W���O")]
		Ease aimEasing;

		[Header("Direction")]
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
			if (CheckAimMovable(Direction.up)) {
				core.transform.DORotate(new Vector3(-aimAngle, 0, 0), aimDuration)
					.SetEase(aimEasing);

				currentDirection = Direction.up;
			}

			else if (CheckAimMovable(Direction.down)) {
				core.transform.DORotate(new Vector3(0, 0, 0), aimDuration)
					.SetEase(aimEasing);

				currentDirection = Direction.down;
			}
		}

		// �Ə��ړ��\�����m�F����
		bool CheckAimMovable(Direction checkDirection)
		{
			switch(checkDirection) {
				case Direction.up:
					if (PlayerController.ActiveController.IsUp) {
						break;
					}
					return false;

					case Direction.down:
					if (PlayerController.ActiveController.IsDown) {
						break;
					}

					return false;
			}

			// �X����X���ړ�臒l�ȉ��� ���� ���������݂̕����ł͂Ȃ��Ƃ�
			// �����E�ɌX���Ȃ���㉺�ɏƏ��ړ����Ȃ��悤�ɂ��邽�߂̔���ł�
			if ((Mathf.Abs(core.DataReceiver.Gyro.x) <= movingThresholdX) || !checkAimThresholdX) {
				if (currentDirection != checkDirection) {
					return true;
				}
			}
			
			return false;
		}
	}
}