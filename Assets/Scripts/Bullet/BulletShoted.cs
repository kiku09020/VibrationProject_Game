using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Bullet
{
    // �e���C���X�^���X�����ꂽ�Ƃ��̏������܂Ƃ߂��N���X
    public class BulletShoted : ObjectComponentBase<BulletCore>
    {
		[Header("Components")]
		[SerializeField] Rigidbody rb;

		[SerializeField,Tooltip("�폜�����܂ł̎���")] 
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
