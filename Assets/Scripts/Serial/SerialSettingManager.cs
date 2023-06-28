using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialSettingManager : MonoBehaviour
{

    //--------------------------------------------------

    void Awake()
    {
        // ÉCÉxÉìÉgí«â¡
        SerialSelector.OnShouldConnected += OnConnectedEvent;
        SerialSelector.OnShouldSelected += OnSelectedEvent;

        SerialSelector.Init();
    }

    bool OnConnectedEvent()
    {
        Debug.LogWarning("ê⁄ë±ÇµÇÎÇ‚");

        if (SerialSelector.SetNewPortName()) {
            Debug.Log("ê⁄ë±äÆóπ");
            return true;
        }   

        return false;
    }

    bool OnSelectedEvent()
    {
        return false;
    }
}
