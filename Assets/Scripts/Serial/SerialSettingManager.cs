using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialSettingManager : MonoBehaviour
{

    //--------------------------------------------------

    void Awake()
    {
        // �C�x���g�ǉ�
        SerialSelector.OnShouldConnected += OnConnectedEvent;
        SerialSelector.OnShouldSelected += OnSelectedEvent;

        SerialSelector.Init();
    }

    bool OnConnectedEvent()
    {
        Debug.LogWarning("�ڑ������");

        if (SerialSelector.SetNewPortName()) {
            Debug.Log("�ڑ�����");
            return true;
        }   

        return false;
    }

    bool OnSelectedEvent()
    {
        return false;
    }
}
