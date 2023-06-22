using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Bullet
{
    // 弾がインスタンス化されたときの処理をまとめたクラス
    public class BulletShoted : ObjectComponentBase<BulletCore>
    {
		[Header("Components")]
		[SerializeField] Rigidbody rb;

		[Header("Objects")]
		[SerializeField] ParticleSystem shotParticle;

		[Header("Parameters")]
		[SerializeField,Tooltip("削除されるまでの時間")] 
		float destoiedTime = 3;

		//--------------------------------------------------

		protected override void OnStart()
		{
			Destroy(gameObject,destoiedTime);
		}

		public void Shot(Transform playerTransform,float power)
		{
			// 発射
			rb.AddForce(playerTransform.forward * power, ForceMode.Impulse);

			// パーティクル生成
			var partObj= Instantiate(shotParticle, transform.position, Quaternion.identity);
			var shape = partObj.shape;
			shape.rotation = playerTransform.eulerAngles;

		}
	}
}
