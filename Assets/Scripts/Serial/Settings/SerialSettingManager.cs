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

    static bool checkPortFlag = true;       // �|�[�g�ݒ肷�邩�ǂ���

    //--------------------------------------------------

    void Start()
    {
        if (checkPortFlag) {
            checkPortFlag = false;      // ����ȍ~�͐ݒ肵�Ȃ�
        }

        SetSelectLogUI();

        // �ؒf����
        SerialSelector.CheckDisconnected(this.GetCancellationTokenOnDestroy());
    }

	//--------------------------------------------------

	private void FixedUpdate()
	{
        RunLogEvents();

		// ���O�w�i �L����
		if (CheckLogConditions()) {
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

    // �h���b�v�_�E���̃I�v�V�����̍X�V
    void SetSelectLogUI()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(SerialSelector.CheckPortNames());       // �|�[�g�����擾���Ēǉ�
    }

	//--------------------------------------------------

	// ���O�C�x���g�̎��s
	void RunLogEvents()
    {
        for(int i = 0; i < logList.Count; i++) {
            var current = logList[i];

            // ���݂̃C���X�^���X���O�̗v�f�̏�����false��
            bool enable = logList.GetRange(0, i).TrueForAll(x => !x.SettingCondition.Condition);

            // �L���ł���΁A���s
            if(enable) {
                current.RunEvent();
            }
        }
	}

    // �����������Ă��邩
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

    // �h���b�v�_�E���̃I�v�V�����̍X�V
    public void ReloadDropDownOptions()
    {
        SetSelectLogUI();
    }

    public void SetDeviceName()
    {
        SerialSelector.SetNewPortName(dropdown.options[dropdown.value].text);
    }
}
