using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// シリアル接続時のログ。タイトルで表示する
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
		// ログメッセージの指定
		eventUnit = new SerialSettingEvent();
        logMessageText.text = logMessage;

        gameObject.SetActive(false);								// ゲームオブジェクト無効化
		token = this.GetCancellationTokenOnDestroy();

		eventUnit.RegisterEvents(() => DispLog(), () => SettingCondition.CheckCondition(), () => UndispLog());
	}

	private void OnDestroy()
	{
		image.rectTransform.localScale = Vector3.one;
	}

	//--------------------------------------------------

	/// <summary> イベント実行 </summary>
	public void RunEvent()
	{
		eventUnit.RunEvent(SettingCondition.Condition);
	}

	//--------------------------------------------------

	/// <summary> ログ表示 </summary>
	protected async void DispLog()
    {
		await UniTask.WaitUntil(() => transform.parent.gameObject.activeSelf, cancellationToken: token);

		// 親経由で自身のゲームオブジェクトをtrueにする
		transform.parent.GetChild(transform.GetSiblingIndex()).gameObject.SetActive(true);
		image.rectTransform.localScale = Vector3.one;
		Scaling(true);
    }

    /// <summary> ログ非表示 </summary>
    protected void UndispLog()
    {
        Scaling();
    }

	//--------------------------------------------------

	// 拡大縮小
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
            tween.OnComplete(() => gameObject.SetActive(false));      // 終了時に画像無効化
        }
    }

	//--------------------------------------------------
}
