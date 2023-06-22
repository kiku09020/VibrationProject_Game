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

		[Header("Objects")]
		[SerializeField] ParticleSystem shotParticle;

		[Header("Parameters")]
		[SerializeField,Tooltip("�폜�����܂ł̎���")] 
		float destoiedTime = 3;

		//--------------------------------------------------

		protected override void OnStart()
		{
			Destroy(gameObject,destoiedTime);
		}

		public void Shot(Transform playerTransform,float power)
		{
			// ����
			rb.AddForce(playerTransform.forward * power, ForceMode.Impulse);

			// �p�[�e�B�N������
			var partObj= Instantiate(shotParticle, transform.position, Quaternion.identity);
			var shape = partObj.shape;
			shape.rotation = playerTransform.eulerAngles;

		}
	}
}
