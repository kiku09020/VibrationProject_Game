using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Linq;

public static class SerialSelector
{
    static List<string> portNames = new List<string>();      // �|�[�g�����X�g

    public static List<string> PortNames => portNames;

    static Action OnDisconnected;

    /// <summary> �ړI�̃|�[�g�� </summary>
    public static string TargetPortName { get; private set; }

    //--------------------------------------------------

    /// <summary> �g�p����V���A���|�[�g�̏����� </summary>
    /// <param name="onConnected">      �V�����ڑ����ꂽ�Ƃ��̏��� </param>
    /// <param name="onSelected">       �|�[�g�I������   </param>
    /// <param name="onDisconnected">   �ڑ��ؒf���̏��� </param>
    public static void InitActiveSerialPort(Action onConnected, Action onSelected, Action onDisconnected)
    {
        OnDisconnected = onDisconnected;

        // 0�������ꍇ�A�V�����|�[�g��ڑ�����K�v������
        if (portNames.Count == 0) {
            onConnected?.Invoke();
        }

        else {
            // �g�p����V���A���|�[�g��I������
            onSelected?.Invoke();
        }
    }

    public static void SetConnectedPortNames()
    {
		portNames.Clear();
		portNames.AddRange(SerialPort.GetPortNames());      // �|�[�g���ǉ�
	}

    /// <summary>
    /// �V���A���|�[�g�̐ؒf�`�F�b�N
    /// </summary>
    public static void CheckDisconnected(SerialPort serialPort)
    {
        if (TargetPortName != null) {
            if (!serialPort.IsOpen) {
                OnDisconnected?.Invoke();
            }
        }
    }

    //--------------------------------------------------
    // �V�����ڑ����ꂽ�V���A���|�[�g�����擾����
    static string GetNewPortName()
    {
        List<string> prevPortNames = new List<string>(portNames);                       // �ȑO�̃|�[�g�����X�g��ۑ�
        List<string> currentPortNames = new List<string>(SerialPort.GetPortNames());    // ���݂̃|�[�g�����X�g���擾

        var addedPortNames = currentPortNames.Except(prevPortNames).ToList();           // ����

        // ���Z�b�g
        portNames.Clear();
        portNames.AddRange(currentPortNames);

        if (addedPortNames.Count <= 0) {
            return null;
        }

        TargetPortName = addedPortNames[0];     // �ǉ�
        return addedPortNames[0];               // �ǉ����ꂽ�V���A���|�[�g����Ԃ�
    }

	/// <summary>
	/// �V�����ڑ����ꂽ�V���A���|�[�g�����g�p����|�[�g���ɃZ�b�g����
	/// </summary>
	public static bool SetNewPortName()
    {
        return SetNewPortName(GetNewPortName());
    }

    /// <summary>
    /// �g�p����V���A���|�[�g�����Z�b�g����
    /// </summary>
    /// <param name="portName"></param>
    /// <returns></returns>
    public static bool SetNewPortName(string portName)
    {
        if (portName != null) {
            TargetPortName = portName;
            Debug.Log($"�g�p����V���A���|�[�g�� {portName} �ɐݒ肵�܂���");
            return true;
        }

        return false;
    }

	//--------------------------------------------------


}
