using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// �f�[�^��M���N���X
/// </summary>
public abstract class DataReceiver_Base:MonoBehaviour
{
    [SerializeField] protected SerialHandler handler;

	//--------------------------------------------------

	/// <summary>
	/// �f�[�^��M���̏���
	/// </summary>
	protected abstract void OnReceivedData();

	//--------------------------------------------------

	private async void Start()
	{
		// �ڑ��\�ɂȂ�܂őҋ@
		await UniTask.WaitUntil(() => handler.IsPortEnable, cancellationToken: this.GetCancellationTokenOnDestroy());

		// �|�[�g���ڑ�����Ă���Ƃ��̂ݓo�^����
		if (handler.IsPortEnable) {
			handler.OnDataReceived += OnReceivedData;
		}
	}
}