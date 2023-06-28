using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialSettingManager : MonoBehaviour
{

    //--------------------------------------------------

    void Awake()
    {
        // イベント追加
        SerialSelector.OnShouldConnected += OnConnectedEvent;
        SerialSelector.OnShouldSelected += OnSelectedEvent;

        SerialSelector.Init();
    }

    bool OnConnectedEvent()
    {
        Debug.LogWarning("接続しろや");

        if (SerialSelector.SetNewPortName()) {
            Debug.Log("接続完了");
            return true;
        }   

        return false;
    }

    bool OnSelectedEvent()
    {
        return false;
    }
}
