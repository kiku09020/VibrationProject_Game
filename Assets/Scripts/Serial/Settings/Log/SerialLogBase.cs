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
public class SerialLogBase:MonoBehaviour 
{
	/* Fields */
	[Header("Parameters")]
	[SerializeField,Multiline] string logMessage; 

	[Header("Components")]
	[SerializeField] TextMeshProUGUI logMessageText;
	[SerializeField] Image image;

	[Header("TweenParameters")]
	[SerializeField] float duration;
	[SerializeField] Ease ease;

	SerialEventUnitBase eventUnit;

	CancellationToken token;
	//--------------------------------------------------

	private void Awake()
	{
		// ログメッセージの指定
		eventUnit = new SerialEventUnitBase(logMessage);
        logMessageText.text = logMessage;

        gameObject.SetActive(false);								// ゲームオブジェクト無効化
		token = this.GetCancellationTokenOnDestroy();
	}

	private void OnDestroy()
	{
		image.rectTransform.localScale = Vector3.one;
	}

	//--------------------------------------------------
	/// <summary> イベント登録 </summary>
	public void RegisterEvent()
    {
		eventUnit.RegisterEvents(() => DispLog(), () => UndispLog(), token);
    }

	/// <summary> イベント実行 </summary>
	public async UniTask RunEvent()
	{
		await eventUnit.RunEvent();
	}

	/// <summary> イベントが実行中かを取得する </summary>
	public bool GetEventIsRunning()
	{
		return eventUnit.IsRunning;
	}

	/// <summary> イベントの終了条件をセットする </summary>
	public void SetEventEndCondition(bool endCondition)
	{
		eventUnit.SetEndCondition(endCondition);
	}

	//--------------------------------------------------

	/// <summary> ログ表示 </summary>
	protected async void DispLog()
    {
		await UniTask.WaitUntil(() => transform.parent.gameObject.activeSelf, cancellationToken: token);

		// 親経由で自身のゲームオブジェクトをtrueにする
		transform.parent.GetChild(transform.GetSiblingIndex()).gameObject.SetActive(true);
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
