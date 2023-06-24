using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class BGMManager : AudioManager<BGMManager> 
{
	[SerializeField, Tooltip("��ԍŏ��ɗ���BGM")]
	AudioClip firstBGM;

	protected override void Awake()
	{
		base.Awake();

		SetLoop();

		if(firstBGM != null) {
			PlayBGMWithFade(firstBGM.name, 1, 5);
		}
	}

	protected override void PlayAudio_Derived(AudioClip clip)
	{
		// �N���b�v���w�肵�čĐ�����
		source.clip = clip;
		source.Play();
	}

	/// <summary>
	/// �t�F�[�h�t����BGM���Đ�����B
	/// </summary>
	/// <param name="bgmName">�Đ�����BGM��</param>
	/// <param name="targetVolume">�ڕW����</param>
	/// <param name="duration">�t�F�[�h����</param>
	/// <param name="resetVolume">���ʂ��ŏ���0�ɂ��邩</param>
	/// <param name="isParamReset">�p�����[�^���Z�b�g�t���O</param>
	public async void PlayBGMWithFade(string bgmName, float targetVolume = 1, float duration = 1, bool resetVolume = true, bool isParamReset = false)
	{
		// ���Z�b�g�t���O���L���ł���΁A���ʂ�0�ɂ���
		if(resetVolume) {
			source.volume = 0;
		}

		source.DOFade(targetVolume, duration);			// �t�F�[�h

		await PlayAudio(bgmName, isParamReset);			// BGM�Đ�
	}
}
