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

		[SerializeField,Tooltip("削除されるまでの時間")] 
		float destoiedTime = 3;

		//--------------------------------------------------

		protected override void OnStart()
		{
			Destroy(gameObject,destoiedTime);
		}

		public void Shot(Transform playerTransform,float power)
		{
			Vector3 forward = playerTransform.forward;

			rb.AddForce(forward * power, ForceMode.Impulse);
		}
	}
}
