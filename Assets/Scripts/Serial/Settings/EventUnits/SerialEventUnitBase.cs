using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary> シリアル接続イベントクラス </summary>
public abstract class SerialEventUnitBase
{
	/// <summary> ログメッセージ </summary>
	public abstract string LogMessage { get; }

	/// <summary> イベントフラグ </summary>
	public bool EventFlag { get; private set; }

	/// <summary> 初期化時イベント </summary>
	public event StartDelegate StartEvent;
	public delegate void StartDelegate();

	/// <summary> 更新処理イベント </summary>
	public event UpdateDelegate UpdateEvent;
	public delegate bool UpdateDelegate();

	CancellationToken token;

	/// <summary> イベントを登録する </summary>
	public void RegisterEvents(StartDelegate startEvent, UpdateDelegate updateEvent,CancellationToken token)
	{
		ResetEvent();

		StartEvent += startEvent;
		UpdateEvent += updateEvent;

		this.token = token;
	}

	/// <summary> イベントをリセットする </summary>
	void ResetEvent()
	{
		StartEvent = null;
		UpdateEvent = null;
	}

	/// <summary> イベントを実行する </summary>
	public async void RunEvent()
	{
		// 条件を満たすまで待機
		await UniTask.WaitUntil(() => {

			// 最初の処理
			if (!EventFlag) {
				StartEvent?.Invoke();
				EventFlag = true;
			}
				
			Debug.LogWarning(LogMessage);
			return UpdateEvent.Invoke();
		}, cancellationToken: token);
	}
}

public class OnDisconnectedEventUnit : SerialEventUnitBase {
	public override string LogMessage => "接続が切断されました。再接続してください";
}

public class OnShouldConnectedEventUnit : SerialEventUnitBase {
	public override string LogMessage => "シリアルポートに1つも接続されていません。";
}

public class OnShouldSelectedEventUnit : SerialEventUnitBase {
	public override string LogMessage => "使用するデバイスが接続されているシリアルポートを選択してください。";
}
