using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Title {
    public class TitleManager : MonoBehaviour {

		/// <summary> �V�[���ǂݍ��� </summary>
		public void LoadMainScene()
		{
			SceneManager.LoadScene("Main");
		}
	}
}