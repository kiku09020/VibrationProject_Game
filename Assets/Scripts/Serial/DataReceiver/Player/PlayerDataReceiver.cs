using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataReceiver : DataReceiver_Base {

	[SerializeField] int dataLength = 3;

	/// <summary>
	/// デバイスの傾き
	/// </summary>
	public Vector2 Gyro { get; private set; }

	/// <summary>
	/// ボタンが押されたかどうか
	/// </summary>
	public bool IsPressed { get; private set; }

	protected override void OnReceivedData()
	{
		var data = handler.GetSplitedData();     // データ取得

		try {
			if (data.Length >= dataLength) {
				// データ文字列からfloat型に変換して、ベクトルに適用
				float x = float.Parse(data[0]);
				float y = float.Parse(data[1]);

				Gyro = new Vector2(x, y);

				// 押されたかどうかをbool型に変換
				IsPressed = bool.Parse(data[2]);
			}
		}

		// 例外
		catch (System.Exception exc) {
			Debug.LogWarning(exc.Message);
		}
	}
}
