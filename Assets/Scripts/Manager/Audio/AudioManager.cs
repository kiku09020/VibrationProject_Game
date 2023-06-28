using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public abstract class AudioManager<T> : MonoBehaviour where T:AudioManager<T>
{
    [Header("Components")]
    [SerializeField] protected AudioSource source;

    [Header("Data")]
    [SerializeField] protected AudioDataList audioDataList;

    static List<AudioManager<T>> managerList = new(); 

    float delay = 0;        // 再生遅延

    [Serializable]
    protected class AudioData
    {
        public string audioName;
        [SerializeField] AudioClip audioClip;

        public AudioClip AudioClip { get => audioClip; }
	}

	//--------------------------------------------------

	protected virtual void Awake()
	{
        managerList.Add(this);      // 登録

        source.playOnAwake = false;
	}

	// AudioDataのaudioNameをAudioClipのファイル名に変更
	private void OnValidate()
	{
		foreach(var data in audioDataList.DataList) {
            if(data.AudioClip != null) {
                data.audioName = data.AudioClip.name;
            }
        }
	}

	private void OnDestroy()
	{
		managerList.Clear();
	}

	//--------------------------------------------------
	/// <summary>
	/// 音声の再生処理用抽象メソッド
	/// </summary>
	protected abstract void PlayAudio_Derived(AudioClip clip);

    /// <summary>
    /// Clipを指定して、音声を再生する
    /// </summary>
    /// <param name="clip">Clip</param>
    /// <param name="isParamReset">パラメータを規定値にリセットするかどうか</param>
    public async UniTask PlayAudio(AudioClip clip, bool isParamReset = false)
    {
        // パラメータ再生
        if (isParamReset) {
            ResetSourceParameters();
        }

        await UniTask.Delay(TimeSpan.FromSeconds(delay));       // 待機
        PlayAudio_Derived(clip);
    }

    /// <summary>
    /// 音声名を指定して、音声を再生する
    /// </summary>
    /// <param name="audioName">音声の名前</param>
    public async UniTask PlayAudio(string audioName, bool isParamReset = false)
    {
        foreach (var data in audioDataList.DataList) {
            // 指定した名前と同じ名前のデータがあれば、再生
            if (data.audioName == audioName) {

                await PlayAudio(data.AudioClip, isParamReset);
                return;
            }
        }
                throw new Exception("指定された名前の音声のデータがありません");
    }

	//--------------------------------------------------
    /* Audio Settings */

	/* パラメータ調整用(メソッドチェーン) */
	/// <summary> ループ指定 </summary>
	public T SetLoop(bool loop = true)  { source.loop = loop;           return (T)this; }

    /// <summary> 音量を指定する </summary>
    public T SetVolume(float volume)    { source.volume = volume;       return (T)this; }

    /// <summary> ピッチを指定する </summary>
    public T SetPitch(float pitch)      { source.pitch = pitch;         return (T)this; }

    /// <summary> 再生遅延を指定する </summary>
    public T SetDelay(float delay)      { this.delay = delay;           return (T)this; }

    /// <summary> 優先度を指定する </summary>
    public T SetPriority(int priority)  { source.priority = priority;   return (T)this; }

    // パラメータをリセットする    
    void ResetSourceParameters()
    {
        source.loop = false;
        source.priority = 128;
        source.volume = 1;
        source.pitch = 1;
        delay = 0;
    }

	//--------------------------------------------------
    /* AudioSource Settings */

	/// <summary> 音声を一時停止する </summary>
	public void Pause()     { source.Pause(); }

    /// <summary> 音声の一時停止を解除する </summary>
	public void UnPause()   { source.UnPause(); }

    /// <summary> ミュートにする </summary>
    public void Mute()      { source.mute = true; }

    /// <summary> ミュート解除 </summary>
	public void Unmute()    { source.mute = false; }
	//--------------------------------------------------
    /* Static Methods */

    /// <summary>
    /// 全ての音声を一時停止する
    /// </summary>
    public static void PauseAllAudio()
    {
        foreach(var audManager in managerList) {
            audManager.Pause();
        }
    }

    /// <summary>
    /// 全ての音声の一時停止を解除する
    /// </summary>
    public static void UnpauseAllAudio()
    {
        foreach(var audManager in managerList) {
            audManager.UnPause();
        }
    }

    /// <summary>
    /// 全ての音声をミュートにする
    /// </summary>
    public static void MuteAllAudio()
    {
        foreach(var audManager in managerList) {
            audManager.Mute();
        }
    }

    /// <summary>
    /// 全ての音声のミュートを解除する
    /// </summary>
    public static void UnmuteAllAudio()
    {
        foreach(var audManager in managerList) {
            audManager.Unmute();
        }
    }
}
