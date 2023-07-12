using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary> シリアル接続イベントクラス </summary>
public class SerialSettingEvent
{
	/// <summary> イベント実行中(待機中)かどうか </summary>
	bool isRunning;

	/* Events */
	/// <summary> 初期化時イベント </summary>
	public event StartDelegate StartEvent;
	public delegate void StartDelegate();

	/// <summary> 更新処理イベント </summary>
	public event UpdateDelegate UpdateEvent;
	public delegate void UpdateDelegate();

	/// <summary> 終了時イベント </summary>
	public event EndDelegate EndEvent;
	public delegate void EndDelegate();

	//--------------------------------------------------
	/// <summary> イベントを登録する </summary>
	public void RegisterEvents(StartDelegate startEvent, UpdateDelegate updateEvent, EndDelegate endDelegate, bool isResetEvents = false)
	{
		// イベントリセット
		if (isResetEvents) {
			StartEvent	 = null;
			updateEvent	 = null;
			EndEvent	 = null;
		}

		// イベント登録
		StartEvent  += startEvent;
		UpdateEvent += updateEvent;
		EndEvent	+= endDelegate;
	}

	/// <summary> イベントを実行する </summary>
	public void RunEvent(bool condition)
	{
		// 更新処理
		UpdateEvent?.Invoke();

		// 条件
		if (condition) {
			// 開始処理
			if (!isRunning) {
				StartEvent?.Invoke();
				isRunning = true;
			}
		}

		// 終了処理
		else if (isRunning) {
			EndEvent?.Invoke();
			isRunning = false;
		}
	}
}
