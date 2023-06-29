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

        // ���O�C�x���g�ҋ@�I�������o�^
        connectingLog.RegisterEvent(SerialSelector.SetNewPortName() || isConnected,token);
        selectingPortLog.RegisterEvent(isConnected, token);

        // ������
        SerialSelector.Init(() => connectingLog.EventUnit.RunEvent(),
                            () => selectingPortLog.EventUnit.RunEvent(),
                            () => disconnectingLog.EventUnit.RunEvent());

        InitSelectLogUI();
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
        if (!connectingLog.IsLogged && !selectingPortLog.IsLogged) {
            if(enableBack) {
                backImage.DOFade(0, .5f);
                enableBack = false;
            }
        }

        // �L����
        else {
			if (!enableBack) {
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

        isConnected = true;
    }
}
