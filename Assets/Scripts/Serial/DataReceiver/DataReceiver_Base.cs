using UnityEngine;

/// <summary>
/// データ受信基底クラス
/// </summary>
public abstract class DataReceiver_Base:MonoBehaviour
{
    [SerializeField] protected SerialHandler handler;

	//--------------------------------------------------

	/// <summary>
	/// データ受信時の処理
	/// </summary>
	protected abstract void OnReceivedData();

	//--------------------------------------------------

	private void Start()
	{
		// ポートが接続されているときのみ登録する
		if (handler.IsPortEnable) {
			handler.OnDataReceived += OnReceivedData;
		}
	}
}