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

    static bool checkPortFlag = true;       // �|�[�g�ݒ肷�邩�ǂ���

	CancellationToken token;

    //--------------------------------------------------

    void Awake()
    {
        token = this.GetCancellationTokenOnDestroy();

        if (checkPortFlag) {
            // ���O�C�x���g�ҋ@�I�������o�^
            connectingLog.RegisterEvent(IsConnected, token);
            selectingPortLog.RegisterEvent(IsConnected, token);

            // ������
            SerialSelector.InitActiveSerialPort(() => connectingLog.EventUnit.RunEvent(),
                                () => selectingPortLog.EventUnit.RunEvent(),
                                () => disconnectingLog.EventUnit.RunEvent());

            InitSelectLogUI();

            checkPortFlag = false;      // ����ȍ~�͐ݒ肵�Ȃ�
        }
    }

    // �I�����O�̏�����
    void InitSelectLogUI()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(SerialSelector.PortNames);
    }

	private void FixedUpdate()
	{
        // ������
        if (!connectingLog.EventUnit.EventFlag && !selectingPortLog.EventUnit.EventFlag) {
            if(enableBack) {
                backImage.DOFade(0, .5f)
                         .OnComplete(() => backImage.gameObject.SetActive(false));
                enableBack = false;
            }
        }

        // �L����
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
	/// �|�[�g��o�^����
	/// </summary>
	public void RegisterTargetPort()
    {
        SerialSelector.SetNewPortName(dropdown.captionText.text);

        IsConnected = true;

		selectingPortLog.RegisterEvent(IsConnected, token);
	}
}
