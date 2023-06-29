using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �V���A���ڑ����̃��O�B�^�C�g���ŕ\������
/// </summary>
public class SerialLogBase<T>:MonoBehaviour where T:SerialEventUnitBase,new()
{
    [SerializeField] SerialLogCore core;

    T eventUnit = new T();
    public T EventUnit => eventUnit;
	//--------------------------------------------------

	private void Awake()
	{
        core.LogMessageText.text = eventUnit.LogMessage;
        gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		core.Image.rectTransform.localScale = Vector3.one;
	}

    /// <summary>
    /// �C�x���g�o�^
    /// </summary>
    /// <param name="condition">�ҋ@�I������</param>
    public void RegisterEvent(bool condition,CancellationToken token)
    {
        eventUnit.RegisterEvents(() => DispLog(), () => UndispLog(condition),token);
    }

	//--------------------------------------------------

	/// <summary> ���O�\�� </summary>
	protected virtual async void DispLog()
    {
        await UniTask.WaitUntil(() => transform.parent.gameObject.activeSelf);

		// �e�o�R�Ŏ��g�̃Q�[���I�u�W�F�N�g��true�ɂ���
		transform.parent.GetChild(transform.GetSiblingIndex()).gameObject.SetActive(true);
        Scaling(true);
    }

    /// <summary>
    /// ���O��\��
    /// </summary>
    protected virtual bool UndispLog(bool condition)
    {
        if(condition) {
            Scaling();

            return true;
        }
        return false;
    }
	//--------------------------------------------------

	// �g��k��
	void Scaling(bool doFrom = false)
    {
        var tween = core.Image.rectTransform.DOScale(0, core.Duration)
            .SetEase(core.Ease)
            .SetLink(core.Image.gameObject);

        // 0 -> 1
        if(doFrom) {
            tween.From();
        }

        // 1-> 0
        else {
            tween.OnComplete(() => gameObject.SetActive(false));      // �I�����ɉ摜������
        }
    }

	//--------------------------------------------------
}
