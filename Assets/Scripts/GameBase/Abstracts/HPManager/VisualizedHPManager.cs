using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase
{
	/// <summary>
	/// HPバーなどで可視化されたHPManager
	/// </summary>
	/// <typeparam name="T">ComponentのCore</typeparam>
	/// <typeparam name="U">HPManager</typeparam>
	public class VisualizedHPManager<T,U> : ObjectComponentBase<T> 
        where T:ObjectCore		where U:HPManager<T>
    {
		[Header("Components")]
		[SerializeField] U hpManager;

		[Header("Images")]
		[SerializeField,Tooltip("現在のHP")]	HPImage currentHPImg;
		[SerializeField,Tooltip("ダメージ量")]	HPImage damagedHPImg;

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

		// 画像のAmount変更
		void SetImageFillAumounts()
		{
			
			currentHPImg.SetFillAmount(hpManager.HPRate);
		}
	}
}
