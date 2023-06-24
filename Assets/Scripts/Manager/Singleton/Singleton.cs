using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
	static T instance;

	public static T Instance {
		get {
			if (!instance) {
				// �����̃C���X�^���X����
				instance = FindObjectOfType<T>();

				// ������ΐV�K�쐬
				if (!instance) {
					SetupInstance();
				}
			}

			return instance;
		}
	}

	protected virtual void Awake()
	{
		RemoveDuplicates();
	}

	// �V�K�쐬
	static void SetupInstance()
	{
		var obj = new GameObject();
		obj.name = typeof(T).Name;

		instance = obj.AddComponent<T>();
		DontDestroyOnLoad(obj);
	}

	// �C���X�^���X�̏d���폜
	void RemoveDuplicates()
	{
		// �V�[����ɖ�����ΐV�K�쐬
		if (!instance) {
			instance = this as T;
			DontDestroyOnLoad(gameObject);
		}

		// ���ɂ���Ύ��g���폜
		else {
			Debug.LogError($"*{gameObject} was destroyed.");
			Destroy(gameObject);
		}
	}
}
