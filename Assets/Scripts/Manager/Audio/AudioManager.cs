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

    float delay = 0;        // �Đ��x��

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
        managerList.Add(this);      // �o�^

        source.playOnAwake = false;
	}

	// AudioData��audioName��AudioClip�̃t�@�C�����ɕύX
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
	/// �����̍Đ������p���ۃ��\�b�h
	/// </summary>
	protected abstract void PlayAudio_Derived(AudioClip clip);

    /// <summary>
    /// Clip���w�肵�āA�������Đ�����
    /// </summary>
    /// <param name="clip">Clip</param>
    /// <param name="isParamReset">�p�����[�^���K��l�Ƀ��Z�b�g���邩�ǂ���</param>
    public async UniTask PlayAudio(AudioClip clip, bool isParamReset = false)
    {
        // �p�����[�^�Đ�
        if (isParamReset) {
            ResetSourceParameters();
        }

        await UniTask.Delay(TimeSpan.FromSeconds(delay));       // �ҋ@
        PlayAudio_Derived(clip);
    }

    /// <summary>
    /// ���������w�肵�āA�������Đ�����
    /// </summary>
    /// <param name="audioName">�����̖��O</param>
    public async UniTask PlayAudio(string audioName, bool isParamReset = false)
    {
        foreach (var data in audioDataList.DataList) {
            // �w�肵�����O�Ɠ������O�̃f�[�^������΁A�Đ�
            if (data.audioName == audioName) {

                await PlayAudio(data.AudioClip, isParamReset);
                return;
            }
        }
                throw new Exception("�w�肳�ꂽ���O�̉����̃f�[�^������܂���");
    }

	//--------------------------------------------------
    /* Audio Settings */

	/* �p�����[�^�����p(���\�b�h�`�F�[��) */
	/// <summary> ���[�v�w�� </summary>
	public T SetLoop(bool loop = true)  { source.loop = loop;           return (T)this; }

    /// <summary> ���ʂ��w�肷�� </summary>
    public T SetVolume(float volume)    { source.volume = volume;       return (T)this; }

    /// <summary> �s�b�`���w�肷�� </summary>
    public T SetPitch(float pitch)      { source.pitch = pitch;         return (T)this; }

    /// <summary> �Đ��x�����w�肷�� </summary>
    public T SetDelay(float delay)      { this.delay = delay;           return (T)this; }

    /// <summary> �D��x���w�肷�� </summary>
    public T SetPriority(int priority)  { source.priority = priority;   return (T)this; }

    // �p�����[�^�����Z�b�g����    
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

	/// <summary> �������ꎞ��~���� </summary>
	public void Pause()     { source.Pause(); }

    /// <summary> �����̈ꎞ��~���������� </summary>
	public void UnPause()   { source.UnPause(); }

    /// <summary> �~���[�g�ɂ��� </summary>
    public void Mute()      { source.mute = true; }

    /// <summary> �~���[�g���� </summary>
	public void Unmute()    { source.mute = false; }
	//--------------------------------------------------
    /* Static Methods */

    /// <summary>
    /// �S�Ẳ������ꎞ��~����
    /// </summary>
    public static void PauseAllAudio()
    {
        foreach(var audManager in managerList) {
            audManager.Pause();
        }
    }

    /// <summary>
    /// �S�Ẳ����̈ꎞ��~����������
    /// </summary>
    public static void UnpauseAllAudio()
    {
        foreach(var audManager in managerList) {
            audManager.UnPause();
        }
    }

    /// <summary>
    /// �S�Ẳ������~���[�g�ɂ���
    /// </summary>
    public static void MuteAllAudio()
    {
        foreach(var audManager in managerList) {
            audManager.Mute();
        }
    }

    /// <summary>
    /// �S�Ẳ����̃~���[�g����������
    /// </summary>
    public static void UnmuteAllAudio()
    {
        foreach(var audManager in managerList) {
            audManager.Unmute();
        }
    }
}
