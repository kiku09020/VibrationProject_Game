using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AudioList",menuName ="Scriptable/AudioList")]
public class AudioDataList : ScriptableObject
{
	[SerializeField] List<AudioData> dataList;

	[Serializable]
	public class AudioData {
		public string audioName;
		[SerializeField] AudioClip audioClip;

		public AudioClip AudioClip { get => audioClip; }
	}

	/// <summary>
	/// 音声データリスト
	/// </summary>
	public List<AudioData> DataList { get => dataList; }

	private void OnValidate()
	{
		foreach (var data in dataList) {
			if (data.AudioClip != null) {
				data.audioName = data.AudioClip.name;
			}
		}
	}
}
