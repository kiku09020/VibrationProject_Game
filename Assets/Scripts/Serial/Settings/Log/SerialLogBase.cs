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
    /// イベント登録
    /// </summary>
    /// <param name="condition">待機終了条件</param>
    public void RegisterEvent(bool condition,CancellationToken token)
    {
        eventUnit.RegisterEvents(() => DispLog(), () => UndispLog(condition),token);
    }

	//--------------------------------------------------

	/// <summary> ログ表示 </summary>
	protected virtual async void DispLog()
    {
        await UniTask.WaitUntil(() => transform.parent.gameObject.activeSelf);

		// 親経由で自身のゲームオブジェクトをtrueにする
		transform.parent.GetChild(transform.GetSiblingIndex()).gameObject.SetActive(true);
        Scaling(true);
    }

    /// <summary>
    /// ログ非表示
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

	// 拡大縮小
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
            tween.OnComplete(() => gameObject.SetActive(false));      // 終了時に画像無効化
        }
    }

	//--------------------------------------------------
}
