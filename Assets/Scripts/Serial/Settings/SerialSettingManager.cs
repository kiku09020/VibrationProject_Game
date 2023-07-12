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
    [SerializeField] List<SerialSettingLog> logList = new List<SerialSettingLog>();

	[Header("UI")]
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] Button button;
    [SerializeField] Image backImage;

    [Header("Parameters")]
    [SerializeField] float waitDuration = 3;

    public bool IsConnected { get; private set; }
    bool enableBack;

    static bool checkPortFlag = true;       // ポート設定するかどうか

    //--------------------------------------------------

    void Start()
    {
        if (checkPortFlag) {
            checkPortFlag = false;      // これ以降は設定しない
        }

        SetSelectLogUI();

        // 切断判定
        SerialSelector.CheckDisconnected(this.GetCancellationTokenOnDestroy());
    }

	//--------------------------------------------------

	private void FixedUpdate()
	{
        RunLogEvents();

		// ログ背景 有効化
		if (CheckLogConditions()) {
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

    // ドロップダウンのオプションの更新
    void SetSelectLogUI()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(SerialSelector.CheckPortNames());       // ポート名を取得して追加
    }

	//--------------------------------------------------

	// ログイベントの実行
	void RunLogEvents()
    {
        for(int i = 0; i < logList.Count; i++) {
            var current = logList[i];

            // 現在のインスタンスより前の要素の条件がfalseか
            bool enable = logList.GetRange(0, i).TrueForAll(x => !x.SettingCondition.Condition);

            // 有効であれば、実行
            if(enable) {
                current.RunEvent();
            }
        }
	}

    // 条件満たしているか
    bool CheckLogConditions()
    {
        foreach(var log in  logList) {
            if (log.SettingCondition.Condition) {
                return true;
            }
        }

        return false;
    }

	//--------------------------------------------------
    /* UI Event Methods */

    // ドロップダウンのオプションの更新
    public void ReloadDropDownOptions()
    {
        SetSelectLogUI();
    }

    public void SetDeviceName()
    {
        SerialSelector.SetNewPortName(dropdown.options[dropdown.value].text);
    }
}
