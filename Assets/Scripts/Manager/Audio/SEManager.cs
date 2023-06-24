using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class SEManager : AudioManager<SEManager>
{
	protected override void PlayAudio_Derived(AudioClip clip)
	{
		// ���[�v���L���ȏꍇ�APlay
		if (source.loop) {
			source.clip = clip;
			source.Play();
		}

		// �����łȂ���΁APlayOneShot
		else {
			source.PlayOneShot(clip);
		}
	}

	/// <summary>
	/// �Đ����Ԃ��w�肵�āA���̎��Ԓ����ʉ����[�v�Đ�����
	/// </summary>
	public async void PlayLoopedSE(string audioName, float duration, bool isParamReset = false)
	{
		source.loop = true;

		await PlayAudio(audioName, isParamReset);

		// �ҋ@��Ƀ��[�v�I��
		await UniTask.Delay(System.TimeSpan.FromSeconds(duration));
		source.loop = false;
		source.clip = null;
	}

	/* Randomness */

	/// <summary>
	/// �����_���Ȍ��ʉ����Đ�����
	/// </summary>
	/// <param name="isParamReset">�����_���v�f�Ɋ܂߂鉹����</param>
	public async void PlayRandomSE(bool isParamReset = false)
	{
		await PlayAudio(GetRandomClip(dataList.Count), isParamReset);
	}

	/// <summary>
	/// �������w��ŉ����������_���ōĐ�����
	/// </summary>
	/// <param name="SENames">�����_���v�f�Ɋ܂߂鉹����</param>
	public async void PlayRandomSE(bool isParamReset = false, params string[] SENames)
	{
		List<AudioClip> clipList = new();

		foreach(var data in dataList) {

			// �w�肳�ꂽ���O�Ɠ���clip��o�^����
			foreach(var name in SENames) {
				if (data.audioName == name) {
					clipList.Add(data.AudioClip); break;
				}
			}
		}

		int randomIndex = Random.Range(0, clipList.Count);

		await PlayAudio(clipList[randomIndex], isParamReset);
	}

	// �����_����clip���擾
	AudioClip GetRandomClip(int dataCount)
	{
		int randomIndex = Random.Range(0, dataCount);
		AudioClip randomClip = dataList[randomIndex].AudioClip;
		return randomClip;
	}

	//--------------------------------------------------
	/* Pitch */

	/// <summary>
	/// �s�b�`��͈͎w��Ń����_���ɂ���
	/// </summary>
	public SEManager SetRandomPitch(float min = 0.5f, float max = 1)
	{
		source.pitch = Random.Range(min, max);
		return this;
	}

	/// <summary>
	/// �s�b�`�����X�ɕύX
	/// </summary>
	/// <param name="targetPitch">�ڕW�s�b�`</param>
	/// <param name="duration">�s�b�`�ύX����</param>
	public SEManager SetPitchWithTween(float targetPitch, float duration)
	{
		source.DOPitch(targetPitch, duration);              // �s�b�`�ύX

		return this;
	}

	/// <summary>
	/// �s�b�`�����X�ɕύX(�����_��)
	/// </summary>
	/// <param name="min">�����_���ŏ��l</param>
	/// <param name="max">�����_���ő�l</param>
	/// <param name="duration">�s�b�`�ύX����</param>
	public SEManager SetPitchWithTween( float min, float max, float duration)
	{
		float randomizedPitch = Random.Range(min, max);     // �����_���l�擾

		SetPitchWithTween(randomizedPitch, duration);
		return this;
	}
}
