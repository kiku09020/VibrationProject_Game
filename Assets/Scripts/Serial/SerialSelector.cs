using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

public static class SerialSelector
{
    static List<string> portNames = new List<string>();      // ポート名リスト

	/// <summary> 目的のポート名 </summary>
	public static string TargetPortName { get; private set; }

	//--------------------------------------------------
	/// <summary> 未接続かどうか </summary>
	public static bool IsDisconnect_First => (portNames.Count <= 0);

    /// <summary> 切断されたか </summary>
    public static bool IsDisconnected { get; private set; }

    /// <summary> 複数デバイスがあるかどうか </summary>
    public static bool IsMultiple => (TargetPortName == null);

    //--------------------------------------------------

    /// <summary>
    /// ポート名の更新
    /// </summary>
    public static List<string> CheckPortNames()
    {
        var names = new List<string>(SerialPort.GetPortNames());

        portNames.Clear();
        portNames.AddRange(names);

        return names;
    }

    /// <summary>
    /// 切断判定
    /// </summary>
    public static async void CheckDisconnected(CancellationToken token)
    {
        while (true) {
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);       // 待機

			CheckPortNames();       // ポート名確認

            // 使用ポート名がリストに含まれているか確認
			if (TargetPortName != null) {
				if (!portNames.Contains(TargetPortName)) {
					IsDisconnected = true;          // 含まれていなければ、切断判定
				}

				else {
					IsDisconnected = false;
				}
			}
		}
	}

    // 新しく接続されたシリアルポート名を取得する
    static string GetNewPortName()
    {
        var addedPortNames = SerialPort.GetPortNames().Except(portNames).ToArray();           // 差分

        // 0以下だったらnull
        if (addedPortNames.Length <= 0) {
            return null;
        }

        return addedPortNames[0];               // 追加されたシリアルポート名を返す
    }

    /// <summary>
    /// 新しく接続されたシリアルポート名を使用するポート名にセットする
    /// </summary>
    public static bool SetNewPortName()
    {
        return SetNewPortName( GetNewPortName());
    }

    // 使用するシリアルポート名をセットする
    public static bool SetNewPortName(string portName)
    {
        if (portName != null) {
            // リストに含まれているか判定
            if (portNames.Contains(portName)) {
                // 使用デバイス名指定
                TargetPortName = portName;
                Debug.Log($"使用するシリアルポートを {portName} に設定しました");
                return true;
            }
        }

        return false;
    }

	//--------------------------------------------------


}
