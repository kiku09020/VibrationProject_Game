using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class SEManager : AudioManager<SEManager>
{
	protected override void PlayAudio_Derived(AudioClip clip)
	{
		// ループが有効な場合、Play
		if (source.loop) {
			source.clip = clip;
			source.Play();
		}

		// そうでなければ、PlayOneShot
		else {
			source.PlayOneShot(clip);
		}
	}

	/// <summary>
	/// 再生時間を指定して、その時間中効果音ループ再生する
	/// </summary>
	public async void PlayLoopedSE(string audioName, float duration, bool isParamReset = false)
	{
		source.loop = true;

		await PlayAudio(audioName, isParamReset);

		// 待機後にループ終了
		await UniTask.Delay(System.TimeSpan.FromSeconds(duration));
		source.loop = false;
		source.clip = null;
	}

	/* Randomness */

	/// <summary>
	/// ランダムな効果音を再生する
	/// </summary>
	/// <param name="isParamReset">ランダム要素に含める音声名</param>
	public async void PlayRandomSE(bool isParamReset = false)
	{
		await PlayAudio(GetRandomClip(audioDataList.DataList.Count), isParamReset);
	}

	/// <summary>
	/// 音声名指定で音声をランダムで再生する
	/// </summary>
	/// <param name="SENames">ランダム要素に含める音声名</param>
	public async void PlayRandomSE(bool isParamReset = false, params string[] SENames)
	{
		List<AudioClip> clipList = new();

		foreach(var data in audioDataList.DataList) {

			// 指定された名前と同じclipを登録する
			foreach(var name in SENames) {
				if (data.audioName == name) {
					clipList.Add(data.AudioClip); break;
				}
			}
		}

		int randomIndex = Random.Range(0, clipList.Count);

		await PlayAudio(clipList[randomIndex], isParamReset);
	}

	// ランダムなclipを取得
	AudioClip GetRandomClip(int dataCount)
	{
		int randomIndex = Random.Range(0, dataCount);
		AudioClip randomClip = audioDataList.DataList[randomIndex].AudioClip;
		return randomClip;
	}

	//--------------------------------------------------
	/* Pitch */

	/// <summary>
	/// ピッチを範囲指定でランダムにする
	/// </summary>
	public SEManager SetRandomPitch(float min = 0.5f, float max = 1)
	{
		source.pitch = Random.Range(min, max);
		return this;
	}

	/// <summary>
	/// ピッチを徐々に変更
	/// </summary>
	/// <param name="targetPitch">目標ピッチ</param>
	/// <param name="duration">ピッチ変更時間</param>
	public SEManager SetPitchWithTween(float targetPitch, float duration)
	{
		source.DOPitch(targetPitch, duration);              // ピッチ変更

		return this;
	}

	/// <summary>
	/// ピッチを徐々に変更(ランダム)
	/// </summary>
	/// <param name="min">ランダム最小値</param>
	/// <param name="max">ランダム最大値</param>
	/// <param name="duration">ピッチ変更時間</param>
	public SEManager SetPitchWithTween( float min, float max, float duration)
	{
		float randomizedPitch = Random.Range(min, max);     // ランダム値取得

		SetPitchWithTween(randomizedPitch, duration);
		return this;
	}
}
