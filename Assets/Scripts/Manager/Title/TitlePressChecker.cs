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
		// �Q�[�W�Z�b�g
		void SetPressedGauge()
		{
			// �����Ă����瑝��
			if (PlayerController.ActiveController.IsPressed) {
				if (StartCounter < 1) {
					StartCounter += Time.deltaTime;
				}

				else {
					titleManager.LoadMainScene();
				}
			}

			else {
				// �����Ă��Ȃ���Ό��炷
				if (StartCounter > 0) {
					StartCounter -= Time.deltaTime * 2;
				}
			}

			pressBar.fillAmount = StartCounter;
		}
	}
}
