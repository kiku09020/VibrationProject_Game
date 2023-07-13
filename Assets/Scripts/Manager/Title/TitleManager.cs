using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Title {
    public class TitleManager : MonoBehaviour {

		/// <summary> ƒV[ƒ““Ç‚İ‚İ </summary>
		public void LoadMainScene()
		{
			SceneManager.LoadScene("Main");
		}
	}
}