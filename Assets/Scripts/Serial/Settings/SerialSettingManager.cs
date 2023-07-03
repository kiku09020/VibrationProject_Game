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

    static bool checkPortFlag = true;       // �|�[�g�ݒ肷�邩�ǂ���

    //--------------------------------------------------

    void Start()
    {
        if (checkPortFlag) {
            // ���O�C�x���g�ҋ@�I�������o�^
            connectingLog.RegisterEvent();
            selectingPortLog.RegisterEvent();

            // ������
            SerialSelector.InitActiveSerialPort(async () => await connectingLog.RunEvent(),
                                                async () => await selectingPortLog.RunEvent(),
                                                async () => await disconnectingLog.RunEvent());

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

	//--------------------------------------------------

	private void FixedUpdate()
	{
		SetConditions();

		// ���O�w�i �L����
		if (connectingLog.GetEventIsRunning() || selectingPortLog.GetEventIsRunning()) {
			if (!enableBack) {
				backImage.gameObject.SetActive(true);
				backImage.DOFade(.5f, .5f);
				enableBack = true;
			}
		}

        // ���O�w�i ������
        else {
			if (enableBack) {
				backImage.DOFade(0, .5f)
						 .OnComplete(() => backImage.gameObject.SetActive(false));
				enableBack = false;
			}
		}
	}

    // �C�x���g�����w��
    void SetConditions()
    {
        connectingLog.SetEventEndCondition(SerialSelector.SetNewPortName());
        selectingPortLog.SetEventEndCondition(IsConnected);
    }

	//--------------------------------------------------

	/// <summary>
	/// �|�[�g��o�^����
	/// </summary>
	public void RegisterTargetPort()
    {
        SerialSelector.SetNewPortName(dropdown.captionText.text);

        IsConnected = true;
	}
}
