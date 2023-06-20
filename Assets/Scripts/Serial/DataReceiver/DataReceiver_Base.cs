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

	private void Awake()
	{
		handler.OnDataReceived += OnReceivedData;
	}
}