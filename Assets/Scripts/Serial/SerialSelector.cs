using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

public static class SerialSelector
{
    static List<string> portNames = new List<string>();      // �|�[�g�����X�g

	/// <summary> �ړI�̃|�[�g�� </summary>
	public static string TargetPortName { get; private set; }

	//--------------------------------------------------
	/// <summary> ���ڑ����ǂ��� </summary>
	public static bool IsDisconnect_First => (portNames.Count <= 0);

    /// <summary> �ؒf���ꂽ�� </summary>
    public static bool IsDisconnected { get; private set; }

    /// <summary> �����f�o�C�X�����邩�ǂ��� </summary>
    public static bool IsMultiple => (TargetPortName == null);

    //--------------------------------------------------

    /// <summary>
    /// �|�[�g���̍X�V
    /// </summary>
    public static List<string> CheckPortNames()
    {
        var names = new List<string>(SerialPort.GetPortNames());

        portNames.Clear();
        portNames.AddRange(names);

        return names;
    }

    /// <summary>
    /// �ؒf����
    /// </summary>
    public static async void CheckDisconnected(CancellationToken token)
    {
        while (true) {
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);       // �ҋ@

			CheckPortNames();       // �|�[�g���m�F

            // �g�p�|�[�g�������X�g�Ɋ܂܂�Ă��邩�m�F
			if (TargetPortName != null) {
				if (!portNames.Contains(TargetPortName)) {
					IsDisconnected = true;          // �܂܂�Ă��Ȃ���΁A�ؒf����
				}

				else {
					IsDisconnected = false;
				}
			}
		}
	}

    // �V�����ڑ����ꂽ�V���A���|�[�g�����擾����
    static string GetNewPortName()
    {
        var addedPortNames = SerialPort.GetPortNames().Except(portNames).ToArray();           // ����

        // 0�ȉ���������null
        if (addedPortNames.Length <= 0) {
            return null;
        }

        return addedPortNames[0];               // �ǉ����ꂽ�V���A���|�[�g����Ԃ�
    }

    /// <summary>
    /// �V�����ڑ����ꂽ�V���A���|�[�g�����g�p����|�[�g���ɃZ�b�g����
    /// </summary>
    public static bool SetNewPortName()
    {
        return SetNewPortName( GetNewPortName());
    }

    // �g�p����V���A���|�[�g�����Z�b�g����
    public static bool SetNewPortName(string portName)
    {
        if (portName != null) {
            // ���X�g�Ɋ܂܂�Ă��邩����
            if (portNames.Contains(portName)) {
                // �g�p�f�o�C�X���w��
                TargetPortName = portName;
                Debug.Log($"�g�p����V���A���|�[�g�� {portName} �ɐݒ肵�܂���");
                return true;
            }
        }

        return false;
    }

	//--------------------------------------------------


}
