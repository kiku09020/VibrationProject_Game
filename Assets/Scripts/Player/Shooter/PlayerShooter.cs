using DG.Tweening;
using Game.Bullet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerShooter : PlayerBaseComponent
    {
		[Header("Objects")]
		[SerializeField, Tooltip("�e")]
		BulletCore bullet;

		[Header("Parameters")]
		[SerializeField, Tooltip("���˗�")]
		float shotPower = 100;

		[SerializeField, Tooltip("���˂܂ł̃N�[���^�C��")]
		float coolTimeLimit = .5f;
		float coolTimer;
		bool isCoolTime;			// �N�[���^�C������

		[SerializeField, Tooltip("�������\��")]
		bool enableLongPressShot;

		[Header("Reaction")]
		[SerializeField, Tooltip("�T�C�Y")]
		Vector2 reactedSize;
		[SerializeField, Tooltip("����")]
		float reatedDuration = .5f;
		[SerializeField, Tooltip("�C�[�W���O")]
		Ease reactedEasing;

		[Header("Componets")]
		[SerializeField] SEManager manager;

		//--------------------------------------------------

		protected override void OnUpdate()
		{
			Shoot();
		}

		void Shoot()
		{
			if (core.Controller.ActiveController.IsPressed && !isCoolTime ) {
				isCoolTime = true;

				// �e�̃C���X�^���X��
				var bltObj = Instantiate(bullet, transform.position, Quaternion.identity);			// ���ˈʒu�Fshooter�
				bltObj.GetCoreComponent<BulletShoted>().Shot(core.transform, shotPower);            // ���ʕ����FPlayer�

				core.transform.DOScale(reactedSize, reatedDuration).SetEase(reactedEasing).SetLoops(2, LoopType.Yoyo);

				manager
					.SetPitchWithTween(1, 1.5f, .25f)
				   .PlayRandomSE();
			}

			else if(isCoolTime) {
				coolTimer += Time.deltaTime;

				// �N�[���^�C���𒴂��Ă��Ă��邩
				if(coolTimer >= coolTimeLimit) {

					// 1. �������L���ŉ�����Ă���
					// 2. �����������ŉ�����Ă��Ȃ��Ƃ�
					// �@�N�[���^�C�����Z�b�g
					if (!core.Controller.ActiveController.IsPressed ^ enableLongPressShot) {
						isCoolTime= false;
						coolTimer = 0;
					}
				}
			}
		}
	}
}
