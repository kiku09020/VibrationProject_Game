using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game.Player {
    public class PlayerMover : PlayerBaseComponent {

		/* Fields */
		[Header("Controll")]
		[SerializeField, Tooltip("�ړ�臒l"), Range(0, 1)]
		float movingThreshold = .6f;

		[SerializeField, Tooltip("�ړ��N�[���^�C��"), Range(0, 1)]
		float movingCoolTimeLimit = .5f;

		float movingCoolTimer;      // �N�[���^�C���p�^�C�}�[

		[SerializeField, Tooltip("�����ƌX���Ă����Ԃł���������")]
		bool continueTilted;

		bool isTilted;				// �f�o�C�X���X�����Ă��邩

		[Header("SideMoving")]
		[SerializeField,Tooltip("���݃v���C���[�����铹")]
		Roads currentRoad = Roads.middle;

		[SerializeField, Tooltip("���E�ړ�����"), Range(0, 10)]
		float movingSideDistance = .5f;

		[SerializeField, Tooltip("�ړ�����"), Range(0, 3)]
		float movingDuration = .25f;

		[SerializeField, Tooltip("���E�ړ��C�[�W���O")]
		Ease movingSideEase;

		[Header("Forward")]
		[SerializeField, Tooltip("�O���ړ�����"), Range(0, 1)] 
		float movingForwardDistance = .1f;


		/* Properties */
		/// <summary>
		/// �ړ��\��
		/// </summary>
		public bool IsSideMovable { get; private set; }

		/* Other */
		// ���̗�
		enum Roads {
			left,
			middle,
			right,
		}

		//--------------------------------------------------

		protected override void OnUpdate()
		{
			SetSideMovable();

			MoveForward();
			MoveSide();
		}

		// �O�i
		void MoveForward()
		{
			transform.position += new Vector3(0, 0, movingForwardDistance);
		}

		// ���ړ�
		void MoveSide()
		{
			if(IsSideMovable) {
				// ������
				if (core.Controller.ActiveController.IsLeft) {

					// ���̓��ȊO��������A���݂̓���ύX����
					if (currentRoad != Roads.left) {
						core.transform.DOLocalMoveX(-movingSideDistance, movingDuration)
							.SetEase(movingSideEase)
							.SetRelative();
						currentRoad--;
					}
				}

				// �E����
				else if (core.Controller.ActiveController.IsRight) {

					// �E�̓��ȊO��������A���݂̓���ύX����
					if(currentRoad != Roads.right) {
						core.transform.DOLocalMoveX(movingSideDistance, movingDuration)
							.SetEase(movingSideEase)
							.SetRelative();

						currentRoad++;
					}
				}
			}
		}

		// ���E�ړ��ł��邩�ǂ������m�F����
		void SetSideMovable()
		{
			movingCoolTimer += Time.deltaTime;     // �^�C�}�[���Z

			IsSideMovable = false;

			// �N�[���^�C����������
			if (movingCoolTimer >= movingCoolTimeLimit) {
				// 臒l�ȏ�
				if (core.Controller.ActiveController.IsAxisX) {
					if (!isTilted || continueTilted) {
						IsSideMovable = true;                   // ������悤�ɂ���
						isTilted = true;

						movingCoolTimer = 0;            // �^�C�}�[���Z�b�g
					}
				}

				else {
					isTilted = false;
				}
			}
		}
	}
}