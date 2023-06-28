using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using Cysharp.Threading.Tasks;
using System.Linq;

public static class SerialSelector
{
    static List<string> portNames = new List<string>();      // ポート名リスト

    /// <summary>
    /// 目的のポート名
    /// </summary>
    public static string TargetPortName { get; private set; }

    /// <summary>
    /// 最初にシリアルポートを選択する必要があるかどうか
    /// </summary>
    public static bool ShouldSelect { get; private set; }

    /// <summary>
    /// シリアルポートが接続されていないので、接続する必要があるかどうか
    /// </summary>
    public static bool ShouldConnect { get; private set; }

    /// <summary>
    /// 接続時のイベント
    /// </summary>
    public static event OnConnectedEvent OnShouldConnected;
    public delegate bool OnConnectedEvent();

    /// <summary>
    /// 選択時のイベント
    /// </summary>
    public static event OnSelectedEvent OnShouldSelected;
    public delegate bool OnSelectedEvent();

    //--------------------------------------------------
    // ゲーム開始前に、選択可能なシリアルポートを表示

    public static async void Init()
    {
        portNames.AddRange(SerialPort.GetPortNames());      // ポート名追加

        // 0以下だった場合、接続する必要がある
        if(portNames.Count <= 0 ) {
            Debug.LogWarning("シリアルポートに1つも接続されていません。");

            await UniTask.WaitUntil(() => OnShouldConnected());       // 接続されるまで待機

            ShouldConnect = true;
            return;
        }

        // ポート名が1つだけの場合、それを目的のポート名にする
        else if (portNames.Count == 1) {
            Debug.Log("シリアルポートを自動で選択しました。");

            ShouldSelect = false;
            TargetPortName = portNames[0];      
        }

        // 2つ以上の場合、選択する必要がある
        else {
            Debug.LogWarning("シリアルポートが複数存在するため、選択する必要があります");

			await UniTask.WaitUntil(() => OnShouldSelected());         // 選択されるまで待機

			ShouldSelect = true;
        }

        ShouldConnect = false;
    }

	// ゲーム中に、シリアルポートの接続が切断された場合に停止する


	// 新しく接続されたシリアルポート名を取得する
	static string GetNewPortName()
    {
		List<string> prevPortNames = new List<string>(portNames);                       // 以前のポート名リストを保存
		List<string> currentPortNames = new List<string>(SerialPort.GetPortNames());    // 現在のポート名リストを取得

        var addedPortNames = currentPortNames.Except(prevPortNames).ToList();           // 差分

        // リセット
        portNames.Clear();
        portNames.AddRange(currentPortNames);

        if(addedPortNames.Count <= 0 ) {
            return null;
        }

        else {
            TargetPortName = addedPortNames[0];     // 追加
            return addedPortNames[0];               // 追加されたシリアルポート名を返す
        }
	}

	/// <summary>
	/// 新しく接続されたシリアルポート名を目的のポート名にセットする
	/// </summary>
	public static bool SetNewPortName()
    {
        string newPortName = GetNewPortName();

        if( newPortName != null ) {
            TargetPortName = newPortName;
            return true;
        }

        return false;
    }
}
