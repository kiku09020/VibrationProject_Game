using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using Cysharp.Threading.Tasks;
using System.Linq;

public static class SerialSelector
{
    static List<string> portNames = new List<string>();      // �|�[�g�����X�g

    /// <summary>
    /// �ړI�̃|�[�g��
    /// </summary>
    public static string TargetPortName { get; private set; }

    /// <summary>
    /// �ŏ��ɃV���A���|�[�g��I������K�v�����邩�ǂ���
    /// </summary>
    public static bool ShouldSelect { get; private set; }

    /// <summary>
    /// �V���A���|�[�g���ڑ�����Ă��Ȃ��̂ŁA�ڑ�����K�v�����邩�ǂ���
    /// </summary>
    public static bool ShouldConnect { get; private set; }

    /// <summary>
    /// �ڑ����̃C�x���g
    /// </summary>
    public static event OnConnectedEvent OnShouldConnected;
    public delegate bool OnConnectedEvent();

    /// <summary>
    /// �I�����̃C�x���g
    /// </summary>
    public static event OnSelectedEvent OnShouldSelected;
    public delegate bool OnSelectedEvent();

    //--------------------------------------------------
    // �Q�[���J�n�O�ɁA�I���\�ȃV���A���|�[�g��\��

    public static async void Init()
    {
        portNames.AddRange(SerialPort.GetPortNames());      // �|�[�g���ǉ�

        // 0�ȉ��������ꍇ�A�ڑ�����K�v������
        if(portNames.Count <= 0 ) {
            Debug.LogWarning("�V���A���|�[�g��1���ڑ�����Ă��܂���B");

            await UniTask.WaitUntil(() => OnShouldConnected());       // �ڑ������܂őҋ@

            ShouldConnect = true;
            return;
        }

        // �|�[�g����1�����̏ꍇ�A�����ړI�̃|�[�g���ɂ���
        else if (portNames.Count == 1) {
            Debug.Log("�V���A���|�[�g�������őI�����܂����B");

            ShouldSelect = false;
            TargetPortName = portNames[0];      
        }

        // 2�ȏ�̏ꍇ�A�I������K�v������
        else {
            Debug.LogWarning("�V���A���|�[�g���������݂��邽�߁A�I������K�v������܂�");

			await UniTask.WaitUntil(() => OnShouldSelected());         // �I�������܂őҋ@

			ShouldSelect = true;
        }

        ShouldConnect = false;
    }

	// �Q�[�����ɁA�V���A���|�[�g�̐ڑ����ؒf���ꂽ�ꍇ�ɒ�~����


	// �V�����ڑ����ꂽ�V���A���|�[�g�����擾����
	static string GetNewPortName()
    {
		List<string> prevPortNames = new List<string>(portNames);                       // �ȑO�̃|�[�g�����X�g��ۑ�
		List<string> currentPortNames = new List<string>(SerialPort.GetPortNames());    // ���݂̃|�[�g�����X�g���擾

        var addedPortNames = currentPortNames.Except(prevPortNames).ToList();           // ����

        // ���Z�b�g
        portNames.Clear();
        portNames.AddRange(currentPortNames);

        if(addedPortNames.Count <= 0 ) {
            return null;
        }

        else {
            TargetPortName = addedPortNames[0];     // �ǉ�
            return addedPortNames[0];               // �ǉ����ꂽ�V���A���|�[�g����Ԃ�
        }
	}

	/// <summary>
	/// �V�����ڑ����ꂽ�V���A���|�[�g����ړI�̃|�[�g���ɃZ�b�g����
	/// </summary>
	public static bool SetNewPortName()
    {
        string newPortName = GetNewPortName();

        if( newPortName != null ) {
            TargetPortName = newPortName;
            return true;
        }

        return false;
    }
}
