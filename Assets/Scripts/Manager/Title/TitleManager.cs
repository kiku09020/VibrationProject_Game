using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Title {
    public class TitleManager : MonoBehaviour {

		[SerializeField] SerialSettingManager serSettingManager;
		[SerializeField] DeviceDataReceiver dataReceiver;

		//--------------------------------------------------

		private void Update()
		{
			// 接続されていて、ボタン押したときにシーン読み込み
			if (serSettingManager.IsConnected && SerialSelector.TargetPortName != null) {
				if(Input.GetKeyDown(KeyCode.Space) || dataReceiver.IsPressed) {
					SceneManager.LoadScene("Main");
				}
			}
		}
	}
}