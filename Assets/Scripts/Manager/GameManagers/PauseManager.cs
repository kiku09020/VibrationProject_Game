using GameController.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController.Manager
{
    public class PauseManager : ObjectComponentBase<GameManager>
    {
        /// <summary> ƒ|[ƒY’†‚© </summary>
        public static bool IsPausing { get; private set; }

		//--------------------------------------------------

		protected override void OnStart()
		{
            UnPause();
		}

        public static void Pause()
        {
            IsPausing = true;
            Time.timeScale = 0;     // ’â~

            // ‰¹ºˆê’â~
            AudioManager<BGMManager>.PauseAllAudio();
            AudioManager<SEManager>.PauseAllAudio();

            // PauseUI•\¦
            UIManager.ShowUIGroup<PauseUIGroup>();
        }

        public static void UnPause()
        {
            IsPausing = false;
            Time.timeScale = 1;     // ’â~‰ğœ

            // ‰¹º‚Ìˆê’â~‰ğœ
            AudioManager<BGMManager>.UnpauseAllAudio();
            AudioManager<SEManager>.UnpauseAllAudio();

            // ƒQ[ƒ€UI‚É–ß‚·
            UIManager.ShowLastUIGroup();
        }
    }
}
