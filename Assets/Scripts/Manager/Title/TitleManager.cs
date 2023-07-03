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
			// �ڑ�����Ă��āA�{�^���������Ƃ��ɃV�[���ǂݍ���
			if (serSettingManager.IsConnected && SerialSelector.TargetPortName != null) {
				if(Input.GetKeyDown(KeyCode.Space) || dataReceiver.IsPressed) {
					SceneManager.LoadScene("Main");
				}
			}
		}
	}
}