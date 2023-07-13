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
public class SerialSettingLog:MonoBehaviour 
{
	/* Fields */
	[Header("Parameters")]
	[SerializeField,Multiline] string logMessage; 

	[Header("Components")]
	[SerializeField] TextMeshProUGUI logMessageText;
	[SerializeField] Image image;

	[SerializeField] SerialSettingCondition condition;

	[Header("TweenParameters")]
	[SerializeField] float duration;
	[SerializeField] Ease ease;

	SerialSettingEvent eventUnit;
	public SerialSettingCondition SettingCondition => condition;

	CancellationToken token;
	//--------------------------------------------------

	private void Awake()
	{
		// ���O���b�Z�[�W�̎w��
		eventUnit = new SerialSettingEvent();
        logMessageText.text = logMessage;

        gameObject.SetActive(false);								// �Q�[���I�u�W�F�N�g������
		token = this.GetCancellationTokenOnDestroy();

		eventUnit.RegisterEvents(() => DispLog(), () => SettingCondition.CheckCondition(), () => UndispLog());
	}

	private void OnDestroy()
	{
		image.rectTransform.localScale = Vector3.one;
	}

	//--------------------------------------------------

	/// <summary> �C�x���g���s </summary>
	public void RunEvent()
	{
		eventUnit.RunEvent(SettingCondition.Condition);
	}

	//--------------------------------------------------

	/// <summary> ���O�\�� </summary>
	protected async void DispLog()
    {
		await UniTask.WaitUntil(() => transform.parent.gameObject.activeSelf, cancellationToken: token);

		// �e�o�R�Ŏ��g�̃Q�[���I�u�W�F�N�g��true�ɂ���
		transform.parent.GetChild(transform.GetSiblingIndex()).gameObject.SetActive(true);
		image.rectTransform.localScale = Vector3.one;
		Scaling(true);
    }

    /// <summary> ���O��\�� </summary>
    protected void UndispLog()
    {
        Scaling();
    }

	//--------------------------------------------------

	// �g��k��
	void Scaling(bool doFrom = false)
    {
        var tween = image.rectTransform.DOScale(0, duration)
            .SetEase(ease)
            .SetLink(image.gameObject);

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
