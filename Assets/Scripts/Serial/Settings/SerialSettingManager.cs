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

    public bool IsConnected { get; private set; }
    bool enableBack;

    static bool checkPortFlag = true;       // ポート設定するかどうか

	CancellationToken token;

    //--------------------------------------------------

    void Awake()
    {
        token = this.GetCancellationTokenOnDestroy();

        if (checkPortFlag) {
            // ログイベント待機終了条件登録
            connectingLog.RegisterEvent(IsConnected, token);
            selectingPortLog.RegisterEvent(IsConnected, token);

            // 初期化
            SerialSelector.InitActiveSerialPort(() => connectingLog.EventUnit.RunEvent(),
                                () => selectingPortLog.EventUnit.RunEvent(),
                                () => disconnectingLog.EventUnit.RunEvent());

            InitSelectLogUI();

            checkPortFlag = false;      // これ以降は設定しない
        }
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
        if (!connectingLog.EventUnit.EventFlag && !selectingPortLog.EventUnit.EventFlag) {
            if(enableBack) {
                backImage.DOFade(0, .5f)
                         .OnComplete(() => backImage.gameObject.SetActive(false));
                enableBack = false;
            }
        }

        // 有効化
        else {
			if (!enableBack) {
                backImage.gameObject.SetActive(true);
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

        IsConnected = true;

		selectingPortLog.RegisterEvent(IsConnected, token);
	}
}
