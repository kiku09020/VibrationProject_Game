using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Title
{
    public class TitlePressChecker : MonoBehaviour
    {
		[Header("UI")]
		[SerializeField] Image pressBar;

		[Header("Components")]
		[SerializeField] TitleManager titleManager;

		public float StartCounter { get; private set; }

		//--------------------------------------------------
		private void FixedUpdate()
		{
			SetPressedGauge();
		}

		//--------------------------------------------------
		// ゲージセット
		void SetPressedGauge()
		{
			// 押していたら増加
			if (PlayerController.ActiveController.IsPressed) {
				if (StartCounter < 1) {
					StartCounter += Time.deltaTime;
				}

				else {
					titleManager.LoadMainScene();
				}
			}

			else {
				// 押していなければ減らす
				if (StartCounter > 0) {
					StartCounter -= Time.deltaTime * 2;
				}
			}

			pressBar.fillAmount = StartCounter;
		}
	}
}
