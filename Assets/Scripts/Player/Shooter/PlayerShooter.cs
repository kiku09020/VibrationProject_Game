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
		[SerializeField, Tooltip("弾")]
		BulletCore bullet;

		[Header("Parameters")]
		[SerializeField, Tooltip("発射力")]
		float shotPower = 100;

		[SerializeField, Tooltip("発射までのクールタイム")]
		float coolTimeLimit = .5f;
		float coolTimer;
		bool isCoolTime;			// クールタイム中か

		[SerializeField, Tooltip("長押し可能か")]
		bool enableLongPressShot;

		[Header("Reaction")]
		[SerializeField, Tooltip("サイズ")]
		Vector2 reactedSize;
		[SerializeField, Tooltip("時間")]
		float reatedDuration = .5f;
		[SerializeField, Tooltip("イージング")]
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
			if (PlayerController.ActiveController.IsPressed && !isCoolTime ) {
				isCoolTime = true;

				// 弾のインスタンス化
				var bltObj = Instantiate(bullet, transform.position, Quaternion.identity);			// 発射位置：shooter基準
				bltObj.GetCoreComponent<BulletShoted>().Shot(core.transform, shotPower);            // 正面方向：Player基準

				core.transform.DOScale(reactedSize, reatedDuration).SetEase(reactedEasing).SetLoops(2, LoopType.Yoyo);

				manager
					.SetPitchWithTween(1, 1.5f, .25f)
				   .PlayRandomSE();
			}

			else if(isCoolTime) {
				coolTimer += Time.deltaTime;

				// クールタイムを超えていているか
				if(coolTimer >= coolTimeLimit) {

					// 1. 長押し有効で押されている
					// 2. 長押し無効で押されていないとき
					// 　クールタイムリセット
					if (!PlayerController.ActiveController.IsPressed ^ enableLongPressShot) {
						isCoolTime= false;
						coolTimer = 0;
					}
				}
			}
		}
	}
}
