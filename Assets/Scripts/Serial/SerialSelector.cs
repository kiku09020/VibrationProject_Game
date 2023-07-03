using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Linq;

public static class SerialSelector
{
    static List<string> portNames = new List<string>();      // ポート名リスト

    public static List<string> PortNames => portNames;

    static Action OnDisconnected;

    /// <summary> 目的のポート名 </summary>
    public static string TargetPortName { get; private set; }

    //--------------------------------------------------

    /// <summary> 使用するシリアルポートの初期化 </summary>
    /// <param name="onConnected">      新しく接続されたときの処理 </param>
    /// <param name="onSelected">       ポート選択処理   </param>
    /// <param name="onDisconnected">   接続切断時の処理 </param>
    public static void InitActiveSerialPort(Action onConnected, Action onSelected, Action onDisconnected)
    {
        OnDisconnected = onDisconnected;

        // 0だった場合、新しくポートを接続する必要がある
        if (portNames.Count == 0) {
            onConnected?.Invoke();
        }

        else {
            // 使用するシリアルポートを選択する
            onSelected?.Invoke();
        }
    }

    public static void SetConnectedPortNames()
    {
		portNames.Clear();
		portNames.AddRange(SerialPort.GetPortNames());      // ポート名追加
	}

    /// <summary>
    /// シリアルポートの切断チェック
    /// </summary>
    public static void CheckDisconnected(SerialPort serialPort)
    {
        if (TargetPortName != null) {
            if (!serialPort.IsOpen) {
                OnDisconnected?.Invoke();
            }
        }
    }

    //--------------------------------------------------
    // 新しく接続されたシリアルポート名を取得する
    static string GetNewPortName()
    {
        List<string> prevPortNames = new List<string>(portNames);                       // 以前のポート名リストを保存
        List<string> currentPortNames = new List<string>(SerialPort.GetPortNames());    // 現在のポート名リストを取得

        var addedPortNames = currentPortNames.Except(prevPortNames).ToList();           // 差分

        // リセット
        portNames.Clear();
        portNames.AddRange(currentPortNames);

        if (addedPortNames.Count <= 0) {
            return null;
        }

        TargetPortName = addedPortNames[0];     // 追加
        return addedPortNames[0];               // 追加されたシリアルポート名を返す
    }

	/// <summary>
	/// 新しく接続されたシリアルポート名を使用するポート名にセットする
	/// </summary>
	public static bool SetNewPortName()
    {
        return SetNewPortName(GetNewPortName());
    }

    /// <summary>
    /// 使用するシリアルポート名をセットする
    /// </summary>
    /// <param name="portName"></param>
    /// <returns></returns>
    public static bool SetNewPortName(string portName)
    {
        if (portName != null) {
            TargetPortName = portName;
            Debug.Log($"使用するシリアルポートを {portName} に設定しました");
            return true;
        }

        return false;
    }

	//--------------------------------------------------


}
