using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �V���A���ݒ胍�O�̏����`�F�b�N�Ȃ�
public abstract class SerialSettingCondition : MonoBehaviour
{
    /// <summary> ���� </summary>
    public bool Condition { get; protected set; }

    //--------------------------------------------------
    /// <summary> �����`�F�b�N </summary>
    public abstract void CheckCondition();
}






