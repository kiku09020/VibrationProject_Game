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

		//--------------------------------------------------

		protected override void OnUpdate()
		{
			Shoot();
		}

		void Shoot()
		{
			if (core.DataReceiver.IsPressed &&!isCoolTime ) {
				isCoolTime = true;

				// �e�̃C���X�^���X��
				var bltObj = Instantiate(bullet, transform.position, Quaternion.identity);			// ���ˈʒu�Fshooter�
				bltObj.GetCoreComponent<BulletShoted>().Shot(core.transform, shotPower);			// ���ʕ����FPlayer�
			}

			else if(isCoolTime) {
				coolTimer += Time.deltaTime;

				// �N�[���^�C���𒴂��Ă��Ă��邩
				if(coolTimer >= coolTimeLimit) {

					// 1. �������L���ŉ�����Ă���
					// 2. �����������ŉ�����Ă��Ȃ��Ƃ�
					// �@�N�[���^�C�����Z�b�g
					if (!core.DataReceiver.IsPressed ^ enableLongPressShot) {
						isCoolTime= false;
						coolTimer = 0;
					}
				}
			}
		}
	}
}
