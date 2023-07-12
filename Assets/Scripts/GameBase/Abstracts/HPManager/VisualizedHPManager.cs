using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase
{
	/// <summary>
	/// HP�o�[�Ȃǂŉ������ꂽHPManager
	/// </summary>
	/// <typeparam name="T">Component��Core</typeparam>
	/// <typeparam name="U">HPManager</typeparam>
	public class VisualizedHPManager<T,U> : ObjectComponentBase<T> 
        where T:ObjectCore		where U:HPManager<T>
    {
		[Header("Components")]
		[SerializeField] U hpManager;

		[Header("Images")]
		[SerializeField,Tooltip("���݂�HP")]	HPImage currentHPImg;
		[SerializeField,Tooltip("�_���[�W��")]	HPImage damagedHPImg;

		public class HPImage
		{
			[SerializeField] Image image;
			[SerializeField] float setDuration;
			[SerializeField] Ease ease;

			public void SetFillAmount(float targetAmount)
			{
				image.DOFillAmount(targetAmount, setDuration)
					.SetEase(ease);
			}
		}

		protected override void OnFixedUpdate()
		{
			SetImageFillAumounts();
		}

		// �摜��Amount�ύX
		void SetImageFillAumounts()
		{
			
			currentHPImg.SetFillAmount(hpManager.HPRate);
		}
	}
}
