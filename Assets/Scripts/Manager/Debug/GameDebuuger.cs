using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDebuuger : Singleton<GameDebuuger>
{

	//--------------------------------------------------

	private void Update()
	{
		// �V�[�������[�h
		if (Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
		}
	}
}
