using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SerialSettingManager : MonoBehaviour
{
    [Header("Logs")]
    [SerializeField] SerialDisconnectedLog disconnectingLog;
    [SerializeField] SerialConnectedLog connectingLog;
    [SerializeField] SerialSelectLog selectingPortLog;

	[Header("UI")]
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] Button button;
    [SerializeField] Image backImage;

    bool isConnected;
    bool enableBack;

    CancellationToken token;

    //--------------------------------------------------

    void Awake()
    {
        token = this.GetCancellationTokenOnDestroy();

        // ログイベント待機終了条件登録
        connectingLog.RegisterEvent(SerialSelector.SetNewPortName() || isConnected,token);
        selectingPortLog.RegisterEvent(isConnected, token);

        // 初期化
        SerialSelector.Init(() => connectingLog.EventUnit.RunEvent(),
                            () => selectingPortLog.EventUnit.RunEvent(),
                            () => disconnectingLog.EventUnit.RunEvent());

        InitSelectLogUI();
    }

    // 選択ログの初期化
    void InitSelectLogUI()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(SerialSelector.PortNames);
    }

	private void FixedUpdate()
	{
        // 無効化
        if (!connectingLog.IsLogged && !selectingPortLog.IsLogged) {
            if(enableBack) {
                backImage.DOFade(0, .5f);
                enableBack = false;
            }
        }

        // 有効化
        else {
			if (!enableBack) {
				backImage.DOFade(.5f, .5f);
				enableBack = true;
			}
		}
	}

	//--------------------------------------------------

	/// <summary>
	/// ポートを登録する
	/// </summary>
	public void RegisterTargetPort()
    {
        SerialSelector.SetNewPortName(dropdown.captionText.text);

        isConnected = true;
    }
}
