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
    [SerializeField] SerialLogBase disconnectingLog;
    [SerializeField] SerialLogBase connectingLog;
    [SerializeField] SerialLogBase selectingPortLog;

	[Header("UI")]
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] Button button;
    [SerializeField] Image backImage;

    public bool IsConnected { get; private set; }
    bool enableBack;

    static bool checkPortFlag = true;       // ポート設定するかどうか

    //--------------------------------------------------

    void Start()
    {
        if (checkPortFlag) {
            // ログイベント待機終了条件登録
            connectingLog.RegisterEvent();
            selectingPortLog.RegisterEvent();

            // 初期化
            SerialSelector.InitActiveSerialPort(async () => await connectingLog.RunEvent(),
                                                async () => await selectingPortLog.RunEvent(),
                                                async () => await disconnectingLog.RunEvent());

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

	//--------------------------------------------------

	private void FixedUpdate()
	{
		SetConditions();

		// ログ背景 有効化
		if (connectingLog.GetEventIsRunning() || selectingPortLog.GetEventIsRunning()) {
			if (!enableBack) {
				backImage.gameObject.SetActive(true);
				backImage.DOFade(.5f, .5f);
				enableBack = true;
			}
		}

        // ログ背景 無効化
        else {
			if (enableBack) {
				backImage.DOFade(0, .5f)
						 .OnComplete(() => backImage.gameObject.SetActive(false));
				enableBack = false;
			}
		}
	}

    // イベント条件指定
    void SetConditions()
    {
        connectingLog.SetEventEndCondition(SerialSelector.SetNewPortName());
        selectingPortLog.SetEventEndCondition(IsConnected);
    }

	//--------------------------------------------------

	/// <summary>
	/// ポートを登録する
	/// </summary>
	public void RegisterTargetPort()
    {
        SerialSelector.SetNewPortName(dropdown.captionText.text);

        IsConnected = true;
	}
}
