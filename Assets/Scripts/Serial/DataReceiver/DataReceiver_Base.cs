using Cysharp.Threading.Tasks;
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

	private async void Start()
	{
		// 接続可能になるまで待機
		await UniTask.WaitUntil(() => handler.IsPortEnable, cancellationToken: this.GetCancellationTokenOnDestroy());

		// ポートが接続されているときのみ登録する
		if (handler.IsPortEnable) {
			handler.OnDataReceived += OnReceivedData;
		}
	}
}