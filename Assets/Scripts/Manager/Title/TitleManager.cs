using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Title {
    public class TitleManager : MonoBehaviour {

		[SerializeField] SerialSettingManager serSettingManager;

		//--------------------------------------------------

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space) && serSettingManager.IsConnected) {
				SceneManager.LoadScene("Main");
			}
		}
	}
}