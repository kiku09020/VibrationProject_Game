using GameController.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController.Manager
{
    public class PauseManager : ObjectComponentBase<GameManager>
    {
        /// <summary> �|�[�Y���� </summary>
        public static bool IsPausing { get; private set; }

		//--------------------------------------------------

		protected override void OnStart()
		{
            UnPause();
		}

        public static void Pause()
        {
            IsPausing = true;
            Time.timeScale = 0;     // ��~

            // �����ꎞ��~
            AudioManager<BGMManager>.PauseAllAudio();
            AudioManager<SEManager>.PauseAllAudio();

            // PauseUI�\��
            UIManager.ShowUIGroup<PauseUIGroup>();
        }

        public static void UnPause()
        {
            IsPausing = false;
            Time.timeScale = 1;     // ��~����

            // �����̈ꎞ��~����
            AudioManager<BGMManager>.UnpauseAllAudio();
            AudioManager<SEManager>.UnpauseAllAudio();

            // �Q�[��UI�ɖ߂�
            UIManager.ShowLastUIGroup();
        }
    }
}
