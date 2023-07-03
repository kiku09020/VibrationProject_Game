using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary> シリアル接続イベントクラス </summary>
public class SerialEventUnitBase
{
	string logMessage;		// ログメッセージ

	/// <summary> イベント実行中(待機中)かどうか </summary>
	public bool IsRunning { get; private set; }

	/// <summary> 終了条件 </summary>
	public bool EventEndCondition { get; private set; }

	/* Events */
	/// <summary> 初期化時イベント </summary>
	public event StartDelegate StartEvent;
	public delegate void StartDelegate();

	/// <summary> 更新処理イベント </summary>
	public event UpdateDelegate EndEvent;
	public delegate void UpdateDelegate();

	CancellationToken token;

	//--------------------------------------------------

	public SerialEventUnitBase(string logMessage)
	{
		this.logMessage = logMessage;
	}

	/// <summary> イベントを登録する </summary>
	public void RegisterEvents(StartDelegate startEvent, UpdateDelegate updateEvent,CancellationToken token)
	{
		StartEvent = null;
		EndEvent = null;

		StartEvent += startEvent;
		EndEvent += updateEvent;

		this.token = token;
	}

	/// <summary>
	/// 待機条件を取得する
	/// </summary>
	public void SetEndCondition(bool condition)
	{
		EventEndCondition = condition;
	}

	/// <summary> イベントを実行する </summary>
	public async UniTask RunEvent()
	{
		// 開始処理
		StartEvent?.Invoke();
		IsRunning = true;

		// 条件を満たすまで待機
		await UniTask.WaitUntil(() => {				
			Debug.LogWarning(logMessage);
			return EventEndCondition;

		}, cancellationToken: token);

		// 終了処理
		EndEvent?.Invoke();
		IsRunning = false;
	}
}
